using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using MoviesLibrary;
using MoviesService.Business.Enum;
using MoviesService.Business.Helpers;
using MoviesService.Models;

namespace MoviesService.Business.Repository
{
    /// <summary>
    /// The repository pattern which contains the in Db memory to retrieve the information from 
    /// the in memory db. The static private fields are being used as in memory tables and some
    /// of them are being used for improving performance. The assumption is the count of movies
    /// is small enough to be stored in the memory of a proper server.
    /// 
    /// This class could have been singleton, but for making it singleton I am relying on the 
    /// dependency injection (Simple Injector)
    /// </summary>
    public class ReadRepository : IReadRepository
    {
        /// <summary>
        /// The list of movies. ConcurrentBag type is a type safe List to avoid concurrenty 
        /// problems on the list. Beacause the maintenance of this list is being done
        /// internally by the object itself, it doesn't have a set property.
        /// </summary>
        public ConcurrentBag<Movie> Movies { get; }

        /// <summary>
        /// A dictionary of string and Movie pairs which makes the high performance in memory
        /// test easier. Since, the Movie objecrs already exist in memory, there is only the 
        /// cost of creating this dictionary and mainting it while adding new Movies. However,
        /// the performance of searching is going to be great. Beacause the maintenance of this
        /// dictionary is being done internally by the object itself, it doesn't have a set 
        /// property.
        /// </summary>
        public ConcurrentDictionary<string, List<Movie>> SearchMoviesDictionary { get; }

        /// <summary>
        /// A dictionary of each sortedBy item and its sorted list. Beacause the maintenance of 
        /// this dictionary is being done internally by the object itself, it doesn't have a 
        /// set property.
        /// </summary>
        public ConcurrentDictionary<SortByEnum, SortedList<object, Movie>> OrderByDictionary { get; }

        /// <summary>
        /// A dictionary of Db generated MovieId to Movie for updating.
        /// </summary>
        public ConcurrentDictionary<int, Movie> MovieIdToMovieDictionary { get; }

        /// <summary>
        /// A dictionary of gui generated TempMovieId to Movie for updating. We need to
        /// maintain this, because if user needs to update a movied staright away after
        /// generating it, they're going to send the TempMovieId. 
        /// </summary>
        public ConcurrentDictionary<Guid, Movie> TempMovieIdToMovieDictionary { get; }

        /// <summary>
        /// Constructor the in memory fields are being initiated here
        /// </summary>
        public ReadRepository()
        {
            var dataSource = new MovieDataSource();
            Movies = new ConcurrentBag<Movie>();
            SearchMoviesDictionary = new ConcurrentDictionary<string, List<Movie>>();
            OrderByDictionary = new ConcurrentDictionary<SortByEnum, SortedList<object, Movie>>();
            MovieIdToMovieDictionary = new ConcurrentDictionary<int, Movie>();
            TempMovieIdToMovieDictionary = new ConcurrentDictionary<Guid, Movie>();

            CreateInitialOrderByDictinary();
            var movies = dataSource.GetAllData();
            foreach (var movieData in movies)
                InsertMovie(movieData.ToMovie());
        }


        /// <summary>
        /// Adds a movie to the seach dictionary. This method looks into the string perties of 
        /// the movie, looks them up in the dictionary, and if don't exist add them to the
        /// keys of the dictionary and the movie to the new list of movies of that string, 
        /// otherwise (if exist), add the movie to the existing list of the key. 
        /// </summary>
        /// <param name="movie">the movie instance to add</param>
        private void AddMovieToSearchDictionary(Movie movie)
        {
            var listOfWords = new List<string>();
            listOfWords.AddRange(movie.Cast);
            listOfWords.Add(movie.Classification);
            listOfWords.Add(movie.Genre);
            listOfWords.Add(movie.Title);
            listOfWords.Add(movie.MovieId.ToString());
            listOfWords.Add(movie.Rating.ToString());
            listOfWords.Add(movie.ReleaseDate.ToString());

            foreach (var word in listOfWords)
            {
                List<Movie> list;
                if (SearchMoviesDictionary.TryGetValue(word, out list))
                    list.Add(movie);
                else
                    SearchMoviesDictionary.TryAdd(word, new List<Movie> {movie});
            }
        }

        /// <summary>
        /// Generates the initial ordered lists for each sorting criteria.
        /// </summary>
        private void CreateInitialOrderByDictinary()
        {
            OrderByDictionary.TryAdd(SortByEnum.ReleaseDate, new SortedList<object, Movie>());
            OrderByDictionary.TryAdd(SortByEnum.Classification, new SortedList<object, Movie>());
            OrderByDictionary.TryAdd(SortByEnum.Genre, new SortedList<object, Movie>());
            OrderByDictionary.TryAdd(SortByEnum.MovieId, new SortedList<object, Movie>());
            OrderByDictionary.TryAdd(SortByEnum.Rating, new SortedList<object, Movie>());
            OrderByDictionary.TryAdd(SortByEnum.Title, new SortedList<object, Movie>());
        }

        /// <summary>
        /// Add a new movie to sorted lists.
        /// </summary>
        /// <param name="movie"></param>
        private void AddMovieToOrderedLists(Movie movie)
        {
            //Prefered to do this hardcoded rather than using a foreach for more performance.
            OrderByDictionary[SortByEnum.Classification].Add(movie.Classification, movie);
            OrderByDictionary[SortByEnum.Title].Add(movie.Title, movie);
            OrderByDictionary[SortByEnum.Genre].Add(movie.Genre, movie);
            OrderByDictionary[SortByEnum.MovieId].Add(movie.MovieId, movie);
            OrderByDictionary[SortByEnum.Rating].Add(movie.Rating, movie);
            OrderByDictionary[SortByEnum.Rating].Add(movie.ReleaseDate, movie);
        }

        /// <summary>
        /// Retrieves the list of movies
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Movie> GetAllMovies()
        {
            return Movies;
        }

        /// <summary>
        /// Retrieves the list of movies as a result of a search.
        /// </summary>
        /// <param name="textToSearch">the text to search for</param>
        /// <returns>the list of movies</returns>
        public IEnumerable<Movie> SearchMovies(string textToSearch)
        {
            List<Movie> movies;
            SearchMoviesDictionary.TryGetValue(textToSearch, out movies);
            return movies;
        }

        /// <summary>
        /// Retrieves the list of movies sorted by a particular property
        /// </summary>
        /// <param name="sortBy">the property to sort the list by</param>
        /// <returns>sorted list of movies</returns>
        public IEnumerable<Movie> GetMoviesSortedBy(SortByEnum sortBy)
        {
            SortedList<object, Movie> movies;
            OrderByDictionary.TryGetValue(sortBy, out movies);
            return movies.Values;
        }

        /// <summary>
        /// Inserts a movie object into MovieLibrary Db
        /// </summary>
        /// <param name="movie">MovieData object to insert into Db</param>
        /// <returns>Movie Id generated by Movie Library</returns>
        public void InsertMovie(Movie movie)
        {
            Movies.Add(movie);
            AddMovieToSearchDictionary(movie);
            AddMovieToOrderedLists(movie);
            //update id indexing dictionaries
            if (movie.MovieId != 0)
                MovieIdToMovieDictionary.TryAdd(movie.MovieId, movie);
            if (movie.TempMovieId != Guid.Empty)
                TempMovieIdToMovieDictionary.TryAdd(movie.TempMovieId, movie);
        }

        /// <summary>
        /// Updates the movie in MovieLibrary Db
        /// </summary>
        /// <param name="movie">Movie object to be updated</param>
        public void UpdateMovie(Movie movie)
        {
            
            //Since we have introduced in memory caching mechanism, the update is going to be
            //the trickiest part. For update, we need to whaich field(s) has been updated and
            //update sorted and search dictionaries for them.

        }
    }
}