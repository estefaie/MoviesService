using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MoviesService.Business.Enums;
using MoviesService.Business.Helpers;
using MoviesService.Business.Repository;
using MoviesService.Models;

namespace MoviesService.Tests
{
    [TestClass]
    public class ReadRepositoryTest
    {
        private List<Movie> _movies = new List<Movie>()
        {
            new Movie
            {
                MovieId = 1,
                Title = "Terminator",
                TempMovieId = Guid.Empty,
                Genre = "Scifi",
                ReleaseDate = 1984,
                Classification = "M15",
                Rating = 8,
                Cast = new []{"one", "two"}
            },
            new Movie
            {
                MovieId = 2,
                Title = "Terminator 3",
                TempMovieId = Guid.Empty,
                Genre = "Action",
                ReleaseDate = 1991,
                Classification = "M15",
                Rating = 9,
                Cast = new []{"three", "four"}
            }
        };

        [TestMethod]
        public void AfterInitialStateWithANonEmptyDb_MoviesListShouldContainAllOfTheMovies()
        {
            //Arrange
            var dbMock = new Mock<IDb>();
            dbMock.Setup(d => d.GetAllData()).Returns(_movies);
            var db = dbMock.Object;
            //Act
            var readRepo = new ReadRepository(db);
            //Assert
            Assert.AreEqual(2, readRepo.Movies.Count);
            Assert.IsTrue(readRepo.Movies.OrderBy(m=>m.MovieId).First().IsEqualTo(_movies.OrderBy(m => m.MovieId).First()));
            Assert.IsTrue(readRepo.Movies.OrderBy(m => m.MovieId).Last().IsEqualTo(_movies.OrderBy(m => m.MovieId).Last()));
        }

        [TestMethod]
        public void AfterInitialStateWithANonEmptyDb_SearchMoviesDictionaryShouldContainAllWords()
        {
            //Arrange
            var dbMock = new Mock<IDb>();
            dbMock.Setup(d => d.GetAllData()).Returns(_movies);
            var db = dbMock.Object;
            //Act
            var readRepo = new ReadRepository(db);
            //Assert
            var dic = readRepo.SearchMoviesDictionary;
            Assert.AreEqual(15, dic.Count);
            foreach (var el in dic)
                Assert.AreEqual(el.Key != "terminator" && el.Key != "m15" ? 1 : 2, el.Value.Count, message: el.Key);
        }

        [TestMethod]
        public void AfterInitialStateWithANonEmptyDb_OrderByDictionaryShouldContainTheOrderedLists()
        {
            //Arrange
            var dbMock = new Mock<IDb>();
            dbMock.Setup(d => d.GetAllData()).Returns(_movies);
            var db = dbMock.Object;
            //Act
            var readRepo = new ReadRepository(db);
            //Assert
            var dic = readRepo.OrderByDictionary;
            Assert.AreEqual(6, dic.Count);
            foreach (var el in dic)
                Assert.AreEqual(2, el.Value.Count);

            Assert.AreEqual(1, dic[SortByEnum.MovieId].First().MovieId);
            Assert.AreEqual(2, dic[SortByEnum.Genre].First().MovieId);
            Assert.AreEqual(1, dic[SortByEnum.Rating].First().MovieId);
            Assert.AreEqual(1, dic[SortByEnum.Title].First().MovieId);
            Assert.AreEqual(1, dic[SortByEnum.Classification].First().MovieId);
            Assert.AreEqual(1, dic[SortByEnum.ReleaseDate].First().MovieId);
        }
    }
}
