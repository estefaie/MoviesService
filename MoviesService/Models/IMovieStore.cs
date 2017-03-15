using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoviesLibrary;

namespace MoviesService.Models
{
    interface IMovieStore
    {
        List<MovieData> GetAllMovies();

        List<MovieData> GetAllMoviewSortedBy(SortedByEnum sortedBy);

        List<MovieData> SearchAll(string searchText);
    }
}
