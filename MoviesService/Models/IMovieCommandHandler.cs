using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoviesLibrary;

namespace MoviesService.Models
{
    public interface IMovieCommandHandler
    {
        int AddNewMovie(MovieData movie);

        void UpdateMovie(int id, MovieData movie);
    }
}
