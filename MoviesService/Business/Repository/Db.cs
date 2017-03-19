using System.Collections.Generic;
using System.Linq;
using MoviesLibrary;
using MoviesService.Business.Helpers;
using MoviesService.Models;

namespace MoviesService.Business.Repository
{
    /// <summary>
    /// This class wraps the MovieDataSource and allows mocking of actual MovieDataSource for testing
    /// purpose. Moreover, it hides the conversion of Business objects into Db objects and visa versa.
    /// </summary>
    public class Db : IDb
    {
        private readonly MovieDataSource _movieDataSource = new MovieDataSource();

        /// <summary>
        /// Gets all of movies from the db
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Movie> GetAllData()
        {
            return _movieDataSource.GetAllData().Select(m => m.ToMovie());
        }

        /// <summary>
        /// Creates a new movie in db
        /// </summary>
        /// <param name="movie">movie object to be created</param>
        /// <returns>primary key of created row in the db</returns>
        public int Create(Movie movie)
        {
            return _movieDataSource.Create(movie.ToMovieData());
        }

        /// <summary>
        /// Updates the movie in db.
        /// </summary>
        /// <param name="movie">movie to be updated</param>
        public void Update(Movie movie)
        {
            _movieDataSource.Update(movie.ToMovieData());
        }
    }
}