using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MoviesService.Business.CQRS;
using MoviesService.Business.Repository;
using MoviesService.Models;

namespace MoviesService.Tests
{
    [TestClass]
    public class CommandHandlerTest
    {
        [TestMethod]
        public void HandleCreateMovieCommand_ShouldCallReadAndWriteReposInsertMovie()
        {
            //Arrange
            var mockReadRepo = new Mock<IReadRepository>();
            var readRepo = mockReadRepo.Object;
            var mockWriteRepo = new Mock<IWriteRepository>();
            var writeRepo = mockWriteRepo.Object;
            var commandHandler = new CommandHandler(writeRepo, readRepo);
            var movie = new Movie();
            //Act
            commandHandler.ExecuteCreateMovieCommand(movie);
            //Assert
            mockReadRepo.Verify(m => m.InsertMovie(movie), Times.Once);
            mockWriteRepo.Verify(m => m.InsertMovie(movie), Times.Once);
        }

        [TestMethod]
        public void HandleUpdateMovieCommand_ShouldCallReadAndWriteReposUpdateMovie()
        {
            //Arrange
            var mockReadRepo = new Mock<IReadRepository>();
            var readRepo = mockReadRepo.Object;
            var mockWriteRepo = new Mock<IWriteRepository>();
            var writeRepo = mockWriteRepo.Object;
            var commandHandler = new CommandHandler(writeRepo, readRepo);
            var movie = new Movie();
            //Act
            commandHandler.ExecuteHandleUpdateMovieCommand(movie);
            //Assert
            mockReadRepo.Verify(m => m.UpdateMovie(movie), Times.Once);
            mockWriteRepo.Verify(m => m.UpdateMovie(movie), Times.Once);
        }
    }
}
