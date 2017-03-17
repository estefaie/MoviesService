using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
        /// Constructor the in memory fields are being initiated here
        /// </summary>
        public ReadRepository()
        {
            var dataSource = new MovieDataSource();
            _movies = new ConcurrentBag<Movie>();
            var movies = dataSource.GetAllData();
            foreach (var movieData in movies)
            {
                var movie = movieData.ToMovie();
                _movies.Add(movie);
            }
        }

        public IEnumerable<Movie> GetAllMovies()
        {
            return _movies;
        }

        public IEnumerable<Movie> SearchMovies(string textToSearch)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Movie> GetMoviesSortedBy(SortedByEnum sortedBy)
        {
            throw new NotImplementedException();
        }

        public void InsertMovie(Movie movie)
        {
            throw new NotImplementedException();
        }

        public void UpdateMovie(Movie movie)
        {
            throw new NotImplementedException();
        }

        private ConcurrentBag<Movie> _movies;
    }
}