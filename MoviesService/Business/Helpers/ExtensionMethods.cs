using System;
using System.Web.UI;
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

        /// <summary>
        /// Checks to see if the values in two movies are equal or not
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dst"></param>
        /// <returns>true if org and dst have equal properties</returns>
        public static bool IsEqualTo(this Movie src, Movie dst)
        {
            if (src.MovieId != dst.MovieId)
                return false;
            if (src.Classification != dst.Classification)
                return false;
            if (src.Genre != dst.Genre)
                return false;
            if (src.Rating != dst.Rating)
                return false;
            if (src.ReleaseDate != dst.ReleaseDate)
                return false;
            if (src.Title != dst.Title)
                return false;
            if (src.Cast.Length != dst.Cast.Length)
                return false;
            if (src.Cast.Length == 0)
                return true;
            for (int i=0; i < src.Cast.Length; i++)
            {
                if (src.Cast[i] != dst.Cast[i])
                    return false;
            }
            return true;
        }
    }
}