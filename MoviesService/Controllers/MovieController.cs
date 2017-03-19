using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using MoviesService.Business.CQRS;
using MoviesService.Business.Enums;
using MoviesService.Models;

namespace MoviesService.Controllers
{
    public class MovieController : ApiController
    {
        private readonly ICommandHandler _commandHandler;
        private readonly IQueryHandler _queryHandler;

        public MovieController(ICommandHandler commandHandler, IQueryHandler queryHandler)
        {
            _commandHandler = commandHandler;
            _queryHandler = queryHandler;
        }

        // GET: api/Movie
        public IQueryable<Movie> GetMovies()
        {
            return _queryHandler.HandleGetAllMoviesQuery().AsQueryable();
        }

        [Route("api/Movie/SortBy/{sortBy}")]
        [HttpGet]
        public IQueryable<Movie> GetMoviesSorted(string sortBy)
        {
            if (!string.IsNullOrEmpty(sortBy))
            {
                SortByEnum sortByEnum;
                if (!Enum.TryParse(sortBy.First().ToString().ToUpper() + sortBy.Substring(1), out sortByEnum))
                    return new List<Movie>().AsQueryable();
                return _queryHandler.HandleGetMoviesSortedByQuery(sortByEnum).AsQueryable();
            }
            return new List<Movie>().AsQueryable();
        }

        [Route("api/Movie/SearchFor/{searchFor}")]
        [HttpGet]
        public IQueryable<Movie> GetMoviesSearched(string searchFor)
        {
            if (!string.IsNullOrEmpty(searchFor))
            {
                var result = _queryHandler.HandleSearchMoviesQuery(searchFor.ToLower());
                if (result != null)
                    return _queryHandler.HandleSearchMoviesQuery(searchFor.ToLower()).AsQueryable();
                return new List<Movie>().AsQueryable();
            }
            return new List<Movie>().AsQueryable();
        }

        // PUT: api/Movie/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMovie(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
                
            _commandHandler.ExecuteHandleUpdateMovieCommand(movie);

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Movie
        [ResponseType(typeof(Movie))]
        public IHttpActionResult PostMovie(Movie movie)
        {
            if (!ModelState.IsValid || movie.MovieId == 0 && movie.TempMovieId == Guid.Empty)
            {
                return BadRequest(ModelState);
            }

            _commandHandler.ExecuteCreateMovieCommand(movie);

            return StatusCode(HttpStatusCode.NoContent);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _commandHandler.Dispose();
                _queryHandler.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
