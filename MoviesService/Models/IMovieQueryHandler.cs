using System.Collections.Generic;
using MoviesLibrary;
using MoviesService.Business.Enum;

namespace MoviesService.Models
{
    public interface IMovieQueryHandler
    {
        List<Movie> GetAllMovies();

        List<Movie> GetAllMoviewSortedBy(SortedByEnum sortedBy);

        List<Movie> SearchAll(string searchText);
    }
}
