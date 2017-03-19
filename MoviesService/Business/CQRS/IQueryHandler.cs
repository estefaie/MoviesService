using System;
using System.Collections.Generic;
using MoviesService.Business.Enums;
using MoviesService.Models;

namespace MoviesService.Business.CQRS
{
    /// <summary>
    /// The interface class for QueryHandler
    /// </summary>
    public interface IQueryHandler : IDisposable
    {
        /// <summary>
        /// Handles the GetAllMoviesQuery
        /// </summary>
        /// <returns>the list of all movies</returns>
        IEnumerable<Movie> HandleGetAllMoviesQuery();

        /// <summary>
        /// Handles the GetMoviesSortedByQuery
        /// </summary>
        /// <param name="sortBy">sorted by param</param>
        /// <returns>sorted list of movies</returns>
        IEnumerable<Movie> HandleGetMoviesSortedByQuery(SortByEnum sortBy);

        /// <summary>
        /// Handles the SearchMoviesQuery
        /// </summary>
        /// <param name="textToSearch">text param to be searched</param>
        /// <returns>list of movies with containing the text in one of their fields</returns>
        IEnumerable<Movie> HandleSearchMoviesQuery(string textToSearch);
    }
}
