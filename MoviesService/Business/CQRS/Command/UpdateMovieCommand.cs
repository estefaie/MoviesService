using MoviesService.Models;

namespace MoviesService.Business.CQRS.Command
{
    /// <summary>
    /// The command for updating the information of a movie
    /// </summary>
    public class UpdateMovieCommand
    {
        public Movie Movie { get; set; }
    }
}