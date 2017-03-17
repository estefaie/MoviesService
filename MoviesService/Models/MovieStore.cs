using System.Collections.Concurrent;
using System.Collections.Generic;
using MoviesLibrary;

namespace MoviesService.Models
{
    public sealed class MovieStore
    {
        private static volatile MovieStore _instance;
        private static readonly object SyncRoot = new object();

        private MovieStore() { }

        public static MovieStore Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (_instance == null)
                            _instance = new MovieStore();
                    }
                }

                return _instance;
            }
        }

        public List<Movie> Movies = new List<Movie>();

        private MovieDataSource _movieDataSource = new MovieDataSource();
    }
}