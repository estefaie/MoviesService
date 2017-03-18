using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MoviesLibrary;
using MoviesService.Models;

namespace MoviesService.Business.Helpers
{
    /// <summary>
    /// Contains the extension methods
    /// </summary>
    public static class ExtensionMethods
    {
        /// <summary>
        /// the extension method for generating a Movie object from a MovieData object
        /// </summary>
        /// <param name="movieData">original object</param>
        /// <returns>generated object</returns>
        public static Movie ToMovie(this MovieData movieData)
        {
            return new Movie
            {
                Cast = movieData.Cast,
                Classification = movieData.Classification,
                Genre = movieData.Genre,
                MovieId = movieData.MovieId,
                Rating = movieData.Rating,
                ReleaseDate = movieData.ReleaseDate,
                TempMovieId = Guid.Empty,
                Title = movieData.Title
            };
        }

        /// <summary>
        /// the extension method for generating a Movie object from a MovieData object
        /// </summary>
        /// <param name="movie">original object</param>
        /// <returns>generated object</returns>
        public static MovieData ToMovieData(this Movie movie)
        {
            return new MovieData
            {
                Cast = movie.Cast,
                Classification = movie.Classification,
                Genre = movie.Genre,
                MovieId = movie.MovieId,
                Rating = movie.Rating,
                ReleaseDate = movie.ReleaseDate,
                Title = movie.Title
            };
        }
    }
}