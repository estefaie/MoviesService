using System.Collections.Generic;
using MoviesService.Models;

namespace MoviesService.Business.Repository
{
    /// <summary>
    /// The interface for Db class
    /// </summary>
    public interface IDb
    {
        /// <summary>
        /// Gets all of movies from the db
        /// </summary>
        /// <returns></returns>
        IEnumerable<Movie> GetAllData();

        /// <summary>
        /// Creates a new movie in db
        /// </summary>
        /// <param name="movie">movie object to be created</param>
        /// <returns>primary key of created row in the db</returns>
        int Create(Movie movie);

        /// <summary>
        /// Updates the movie in db.
        /// </summary>
        /// <param name="movie">movie to be updated</param>
        void Update(Movie movie);
    }
}
