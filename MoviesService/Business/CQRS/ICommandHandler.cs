using System;
using MoviesService.Models;

namespace MoviesService.Business.CQRS
{
    /// <summary>
    /// The interface of CommandHandler class for moqing purposes
    /// </summary>
    public interface ICommandHandler : IDisposable
    {
        /// <summary>
        /// Handles create movie command
        /// </summary>
        /// <param name="movie">movie to be created</param>
        void ExecuteCreateMovieCommand(Movie movie);

        /// <summary>
        /// Handles update movie command
        /// </summary>
        /// <param name="movie">movie to be updated</param>
        void ExecuteHandleUpdateMovieCommand(Movie movie);
    }
}
