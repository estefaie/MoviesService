using MoviesService.Business.Repository;

namespace MoviesService.Business.CQRS.Command
{
    /// <summary>
    /// Handles all of the commands 
    /// </summary>
    public class CommandHandler : ICommandHandler
    {
        /// <summary>
        /// the instance of IWriteRepository for doing the write to db operations
        /// </summary>
        private readonly IWriteRepository _wtireRepository;

        /// <summary>
        /// the instance of IReadRepository for sending the changes to read repo to do the
        /// modification in the cached db in memory.
        /// </summary>
        private readonly IReadRepository _readRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="writeRepository">IWriteRepository instance to be injected by DI framework</param>
        /// <param name="readRepository">IReadRepository instance to be injected by DI framework</param>
        public CommandHandler(IWriteRepository writeRepository, IReadRepository readRepository)
        {
            _wtireRepository = writeRepository;
            _readRepository = readRepository;
        }

        /// <summary>
        /// Handles create movie command
        /// </summary>
        /// <param name="command">command object which contains the movie to create</param>
        public void Handle(CreateMovieCommand command)
        {
            _wtireRepository.InsertMovie(command.Movie);
            _readRepository.InsertMovie(command.Movie);
        }

        /// <summary>
        /// Handles update movie command
        /// </summary>
        /// <param name="command">command object which contains the movie to update</param>
        public void Handle(UpdateMovieCommand command)
        {
            _wtireRepository.UpdateMovie(command.Movie);
            _readRepository.UpdateMovie(command.Movie);
        }
    }
}