using MoviesService.Models;

namespace MoviesService.Business.CQRS.Command
{
    /// <summary>
    /// The command for creating a new movie
    /// </summary>
    public class CreateMovieCommand
    {
        public Movie Movie { get; set; }
    }
}