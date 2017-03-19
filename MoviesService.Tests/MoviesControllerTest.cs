using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MoviesService.Business.CQRS;
using MoviesService.Controllers;
using MoviesService.Models;

namespace MoviesService.Tests
{
    [TestClass]
    public class MoviesControllerTest
    {
        private List<Movie> _movies = new List<Movie>()
        {
            new Movie
            {
                MovieId = 1,
                Title = "Terminator",
                TempMovieId = Guid.Empty,
                Genre = "Action",
                ReleaseDate = 1984,
                Classification = "M15",
                Rating = 8
            },
            new Movie
            {
                MovieId = 1,
                Title = "Terminator 2",
                TempMovieId = Guid.Empty,
                Genre = "Action",
                ReleaseDate = 1991,
                Classification = "M15",
                Rating = 9
            }
        };
        

        [TestMethod]
        public void GetMovie_ShouldReturnAllOfTheProducts()
        {
            //Arrange
            var commandHandler = Mock.Of<ICommandHandler>();
            var moqQueryHandler = new Mock<IQueryHandler>();
            moqQueryHandler.Setup(mqh => mqh.HandleGetAllMoviesQuery()).Returns(_movies);
            var queryHandler = moqQueryHandler.Object;
            MovieController mc = new MovieController(commandHandler, queryHandler);
            //Act
            var result = mc.GetMovies();
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count(), _movies.Count);
            Assert.AreEqual(result.First().MovieId, _movies.First().MovieId);
        }

        [TestMethod]
        public void GetMoviesSorted_EmptyParam_ShouldReturnEmpty()
        {
            // Arrange
            var commandHandler = Mock.Of<ICommandHandler>();
            var queryHandler = Mock.Of<IQueryHandler>();
            MovieController mc = new MovieController(commandHandler, queryHandler);
            //Act
            var result = mc.GetMoviesSorted("");
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count(), 0);
        }

        [TestMethod]
        public void GetMoviesSorted_NullParam_ShouldReturnEmpty()
        {
            // Arrange
            var commandHandler = Mock.Of<ICommandHandler>();
            var queryHandler = Mock.Of<IQueryHandler>();
            MovieController mc = new MovieController(commandHandler, queryHandler);
            //Act
            var result = mc.GetMoviesSorted(null);
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count(), 0);
        }

        [TestMethod]
        public void GetMoviesSorted_UnrelatedParam_ShouldReturnEmpty()
        {
            // Arrange
            var commandHandler = Mock.Of<ICommandHandler>();
            var queryHandler = Mock.Of<IQueryHandler>();
            MovieController mc = new MovieController(commandHandler, queryHandler);
            //Act
            var result = mc.GetMoviesSorted("unrelated");
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count(), 0);
        }

        [TestMethod]
        public void GetMoviesSearched_EmptyParam_ShouldReturnEmpty()
        {
            // Arrange
            var commandHandler = Mock.Of<ICommandHandler>();
            var queryHandler = Mock.Of<IQueryHandler>();
            MovieController mc = new MovieController(commandHandler, queryHandler);
            //Act
            var result = mc.GetMoviesSearched("");
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count(), 0);
        }

        [TestMethod]
        public void GetMoviesSearched_NullParam_ShouldReturnEmpty()
        {
            // Arrange
            var commandHandler = Mock.Of<ICommandHandler>();
            var queryHandler = Mock.Of<IQueryHandler>();
            MovieController mc = new MovieController(commandHandler, queryHandler);
            //Act
            var result = mc.GetMoviesSearched(null);
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count(), 0);
        }

        //[TestMethod]
        //public void PutMovie_InvalidModel_ShouldReturnBadRequest()
        //{
        //    // Arrange
        //    var commandHandler = Mock.Of<ICommandHandler>();
        //    var queryHandler = Mock.Of<IQueryHandler>();
        //    MovieController mc = new MovieController(commandHandler, queryHandler);
        //    //Act
        //    var result = mc.PutMovie(new Movie { MovieId = 1 });
        //    //Assert
        //    Assert.IsInstanceOfType(result, typeof(InvalidModelStateResult));
        //}

        [TestMethod]
        public void PostMovie_MovieWithoutMovieIdAndTempMovieId_ShouldReturnBadRequest()
        {
            // Arrange
            var commandHandler = Mock.Of<ICommandHandler>();
            var queryHandler = Mock.Of<IQueryHandler>();
            MovieController mc = new MovieController(commandHandler, queryHandler);
            //Act
            var result = mc.PostMovie(new Movie ());
            //Assert
            Assert.IsInstanceOfType(result, typeof(InvalidModelStateResult));
        }
    }
}
