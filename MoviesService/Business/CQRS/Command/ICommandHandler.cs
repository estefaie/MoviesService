namespace MoviesService.Business.CQRS.Command
{
    /// <summary>
    /// The interface of CommandHandler class for moqing purposes
    /// </summary>
    public interface ICommandHandler
    {
        /// <summary>
        /// Handles create movie command
        /// </summary>
        /// <param name="command">command object which contains the movie to create</param>
        void Handle(CreateMovieCommand command);

        /// <summary>
        /// Handles update movie command
        /// </summary>
        /// <param name="command">command object which contains the movie to update</param>
        void Handle(UpdateMovieCommand command);
    }
}
