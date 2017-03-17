using MoviesService.Models;

namespace MoviesService.Business.CQRS
{
    /// <summary>
    /// The command for creating a new movie
    /// </summary>
    public class CreateMovieCommand : ICommand
    {
        public Movie Movie { get; set; }
    }
}