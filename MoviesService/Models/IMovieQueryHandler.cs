using System.Collections.Generic;
using MoviesLibrary;

namespace MoviesService.Models
{
    public interface IMovieQueryHandler
    {
        List<Movie> GetAllMovies();

        List<Movie> GetAllMoviewSortedBy(SortedByEnum sortedBy);

        List<Movie> SearchAll(string searchText);
    }
}
