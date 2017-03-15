using System.Collections.Generic;
using MoviesLibrary;

namespace MoviesService.Models
{
    public interface IMovieQueryHandler
    {
        List<MovieData> GetAllMovies();

        List<MovieData> GetAllMoviewSortedBy(SoretedByEnum soretedBy);

        List<MovieData> SearchAll(string searchText);
    }
}
