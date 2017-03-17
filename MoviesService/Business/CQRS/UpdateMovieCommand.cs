using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MoviesService.Models;

namespace MoviesService.Business.CQRS
{
    /// <summary>
    /// The command for updating the information of a movie
    /// </summary>
    public class UpdateMovieCommand : ICommand
    {
        public Movie Movie { get; set; }
    }
}