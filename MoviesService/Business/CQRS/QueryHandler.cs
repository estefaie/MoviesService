using System.Collections.Generic;
using MoviesService.Business.Enums;
using MoviesService.Business.Repository;
using MoviesService.Models;

namespace MoviesService.Business.CQRS
{
    /// <summary>
    /// this is responsible for handling the queries
    /// </summary>
    public class QueryHandler : IQueryHandler
    {
        /// <summary>
        /// IReadRepository to read from in memory db
        /// </summary>
        private readonly IReadRepository _readRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="readRepository">instance of IReadRepository to be injected by DI</param>
        public QueryHandler(IReadRepository readRepository)
        {
            _readRepository = readRepository;
        }

        /// <summary>
        /// Handles the GetAllMoviesQuery
        /// </summary>
        /// <returns>the list of all movies</returns>
        public IEnumerable<Movie> HandleGetAllMoviesQuery()
        {
            return _readRepository.GetAllMovies();
        }

        /// <summary>
        /// Handles the GetMoviesSortedByQuery
        /// </summary>
        /// <param name="sortBy">sorted by param</param>
        /// <returns>sorted list of movies</returns>
        public IEnumerable<Movie> HandleGetMoviesSortedByQuery(SortByEnum sortBy)
        {
            return _readRepository.GetMoviesSortedBy(sortBy);
        }

        /// <summary>
        /// Handles the SearchMoviesQuery
        /// </summary>
        /// <param name="textToSearch">text param to be searched</param>
        /// <returns>list of movies with containing the text in one of their fields</returns>
        public IEnumerable<Movie> HandleSearchMoviesQuery(string textToSearch)
        {
            return _readRepository.GetMoviesSearchFor(textToSearch);
        }

        public void Dispose()
        {
            _readRepository.Dispose();
        }
    }
}