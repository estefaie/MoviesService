using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MoviesLibrary;

namespace MoviesService.Models
{
    public class Movie
    {
        public int MovieId { get; set; }

        public string Title { get; set; }

        public string[] Cast { get; set; }

        public string Classification { get; set; }

        public string Genre { get; set; }

        public int Rating { get; set; }

        public int ReleaseDate { get; set; }
    }
}