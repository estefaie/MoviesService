using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MoviesService.Business.Repository;

namespace MoviesService.Business.CQRS
{
    /// <summary>
    /// Handles all of the commands 
    /// </summary>
    public class CommandHandler : ICommandHandler
    {
        public CommandHandler(IWriteRepository writeRepository, IReadRepository readRepository)
        {
            
        }

        public async void Handle(CreateMovieCommand command)
        {
            throw new NotImplementedException();
        }

        public async void Handle(UpdateMovieCommand command)
        {
            throw new NotImplementedException();
        }
    }
}