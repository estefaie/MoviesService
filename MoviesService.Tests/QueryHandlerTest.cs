using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MoviesService.Business.CQRS;
using MoviesService.Business.Enums;
using MoviesService.Business.Repository;

namespace MoviesService.Tests
{
    [TestClass]
    public class QueryHandlerTest
    {
        [TestMethod]
        public void GetAllMovies_ShouldRunGetAllMoviesOfReadRepo()
        {
            //Arrange
            var mockReadRepo = new Mock<IReadRepository>();
            var readRepo = mockReadRepo.Object;
            var queryHandler = new QueryHandler(readRepo);
            //Act
            queryHandler.HandleGetAllMoviesQuery();
            //Assert
            mockReadRepo.Verify(m => m.GetAllMovies(), Times.Once);
        }

        [TestMethod]
        public void GetMoviesSortedBy_ShouldRunGetMoviesSortedByOfReadRepo()
        {
            //Arrange
            var mockReadRepo = new Mock<IReadRepository>();
            var readRepo = mockReadRepo.Object;
            var queryHandler = new QueryHandler(readRepo);
            var sortedBy = SortByEnum.Classification;
            //Act
            queryHandler.HandleGetMoviesSortedByQuery(sortedBy);
            //Assert
            mockReadRepo.Verify(m => m.GetMoviesSortedBy(sortedBy), Times.Once);
        }

        [TestMethod]
        public void SearchMovies_ShouldRunGetMoviesSearchForOfReadRepo()
        {
            //Arrange
            var mockReadRepo = new Mock<IReadRepository>();
            var readRepo = mockReadRepo.Object;
            var queryHandler = new QueryHandler(readRepo);
            var text = "sample text";
            //Act
            queryHandler.HandleSearchMoviesQuery(text);
            //Assert
            mockReadRepo.Verify(m => m.GetMoviesSearchFor(text), Times.Once);
        }
    }
}
