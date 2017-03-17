using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoviesService.Models
{
    public interface IMoviesStore
    {
        IMoviesStore Instance { get; set; }
    }
}