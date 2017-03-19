using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MoviesService.Business.CQRS;
using MoviesService.Business.Repository;
using MoviesService.Models;

namespace MoviesService.Tests
{
    [TestClass]
    public class WriteRepositoryTest
    {
        [TestMethod]
        public void InsertMovie_ShouldCallCreateMovieOfDb()
        {
            //Arrange
            var dbMock = new Mock<IDb>();
            var db = dbMock.Object;
            var writeRepository = new WriteRepository(db);
            var movie = new Movie();
            //Act
            writeRepository.InsertMovie(movie);
            //Assert
            dbMock.Verify(m => m.Create(movie), Times.Once);
        }

        [TestMethod]
        public void UpdateMovie_ShouldCallUpdateovieOfDb()
        {
            //Arrange
            var dbMock = new Mock<IDb>();
            var db = dbMock.Object;
            var writeRepository = new WriteRepository(db);
            var movie = new Movie();
            //Act
            writeRepository.UpdateMovie(movie);
            //Assert
            dbMock.Verify(m => m.Update(movie), Times.Once);
        }
    }
}
