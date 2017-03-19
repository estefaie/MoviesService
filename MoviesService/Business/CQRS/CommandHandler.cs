using MoviesService.Business.Repository;
using MoviesService.Models;

namespace MoviesService.Business.CQRS
{
    /// <summary>
    /// Handles all of the commands 
    /// </summary>
    public class CommandHandler : ICommandHandler
    {
        /// <summary>
        /// the instance of IWriteRepository for doing the write to db operations
        /// </summary>
        private readonly IWriteRepository _writeRepository;

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
            _writeRepository = writeRepository;
            _readRepository = readRepository;
        }

        /// <summary>
        /// Handles create movie command
        /// </summary>
        /// <param name="movie">movie to be created</param>
        public void HandleCreateMovieCommand(Movie movie)
        {
            _writeRepository.InsertMovie(movie);
            _readRepository.InsertMovie(movie);
        }

        /// <summary>
        /// Handles update movie command
        /// </summary>
        /// <param name="movie">movie to be updated</param>
        public void HandleUpdateMovieCommand(Movie movie)
        {
            _writeRepository.UpdateMovie(movie);
            _readRepository.UpdateMovie(movie);
        }

        public void Dispose()
        {
            _readRepository?.Dispose();
        }
    }
}