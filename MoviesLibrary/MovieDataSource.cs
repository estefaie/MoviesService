// Decompiled with JetBrains decompiler
// Type: MoviesLibrary.MovieDataSource
// Assembly: MoviesLibrary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 099914D2-E4B9-4486-9104-2BC3203248E1
// Assembly location: C:\Coding Challenge\CBA\MoviesLibrary.dll

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Threading;

namespace MoviesLibrary
{
  public class MovieDataSource
  {
    private static DataSet _dsMovies = new DataSet();
    private static int _pk;

    private static DataSet Movies
    {
      get
      {
        lock (MovieDataSource._dsMovies)
        {
          if (MovieDataSource._dsMovies.Tables.Count > 0)
            return MovieDataSource._dsMovies;
          StreamReader local_1 = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("MoviesLibrary.MoviesDataSource.xml"));
          MovieDataSource._dsMovies = new DataSet();
          int temp_18 = (int) MovieDataSource._dsMovies.ReadXml((TextReader) local_1, XmlReadMode.ReadSchema);
          if (MovieDataSource._dsMovies.Tables["Movie"].PrimaryKey == null || MovieDataSource._dsMovies.Tables["Movie"].PrimaryKey.Length == 0)
            MovieDataSource._dsMovies.Tables["Movie"].PrimaryKey = new DataColumn[1]
            {
              MovieDataSource._dsMovies.Tables["Movie"].Columns["Id"]
            };
          DataView local_2 = new DataView(MovieDataSource._dsMovies.Tables["Movie"], (string) null, "Id DESC", DataViewRowState.CurrentRows);
          MovieDataSource._pk = local_2.Count <= 0 ? 0 : Convert.ToInt32(local_2[0]["Id"]);
          return MovieDataSource._dsMovies;
        }
      }
    }

    public List<MovieData> GetAllData()
    {
      List<MovieData> movieDataList = new List<MovieData>();
      if (MovieDataSource.Movies != null)
      {
        foreach (DataRow row in (InternalDataCollectionBase) MovieDataSource.Movies.Tables["Movie"].Rows)
          movieDataList.Add(this.GetMovieData(row));
        Thread.Sleep(2000);
      }
      return movieDataList;
    }

    public MovieData GetDataById(int id)
    {
      if (MovieDataSource.Movies != null)
      {
        Thread.Sleep(100);
        DataRow[] dataRowArray = MovieDataSource.Movies.Tables["Movie"].Select("Id = " + id.ToString());
        if (dataRowArray.Length > 0)
          return this.GetMovieData(dataRowArray[0]);
      }
      return (MovieData) null;
    }

    public int Create(MovieData movie)
    {
      if (MovieDataSource.Movies == null)
        throw new Exception("Movies datasource is not available");
      lock (MovieDataSource._dsMovies)
      {
        DataRow local_0 = MovieDataSource._dsMovies.Tables["Movie"].NewRow();
        int local_1 = ++MovieDataSource._pk;
        movie.MovieId = local_1;
        local_0["Id"] = (object) local_1;
        if (string.IsNullOrEmpty(movie.Title))
          throw new Exception("Movie Title is mandatory");
        local_0["Title"] = (object) movie.Title.Trim();
        if (!string.IsNullOrEmpty(movie.Genre))
          local_0["Genre"] = (object) movie.Genre.Trim();
        if (!string.IsNullOrEmpty(movie.Classification))
          local_0["Classification"] = (object) movie.Classification.ToString();
        local_0["Rating"] = (object) movie.Rating;
        local_0["ReleaseDate"] = (object) movie.ReleaseDate;
        MovieDataSource._dsMovies.Tables["Movie"].Rows.Add(local_0);
        if (movie.Cast != null && movie.Cast.Length > 0)
          this.AddCast(movie);
        MovieDataSource._dsMovies.AcceptChanges();
        return local_1;
      }
    }

    public void Update(MovieData movie)
    {
      if (MovieDataSource.Movies == null)
        throw new Exception("Movies datasource is not available");
      lock (MovieDataSource._dsMovies)
      {
        DataRow[] local_0 = MovieDataSource._dsMovies.Tables["Movie"].Select("Id = " + (object) movie.MovieId);
        if (local_0.Length == 0)
          throw new Exception("The movie ID " + movie.MovieId.ToString() + " does not exist");
        if (string.IsNullOrEmpty(movie.Title))
          throw new Exception("Movie Title is mandatory");
        local_0[0]["Title"] = (object) movie.Title;
        local_0[0]["Classification"] = string.IsNullOrEmpty(movie.Classification) ? (object) DBNull.Value : (object) movie.Classification.Trim();
        local_0[0]["Genre"] = string.IsNullOrEmpty(movie.Genre) ? (object) DBNull.Value : (object) movie.Genre.Trim();
        local_0[0]["Rating"] = (object) movie.Rating;
        local_0[0]["ReleaseDate"] = (object) movie.ReleaseDate;
        foreach (DataRow item_0 in MovieDataSource._dsMovies.Tables["Cast"].Select("MovieId = " + movie.MovieId.ToString()))
          item_0.Delete();
        if (movie.Cast == null || movie.Cast.Length <= 0)
          return;
        this.AddCast(movie);
      }
    }

    private void AddCast(MovieData movie)
    {
      for (int index = 0; index < movie.Cast.Length; ++index)
      {
        if (movie.Cast[index].Trim().Length > 0)
        {
          DataRow row = MovieDataSource._dsMovies.Tables["Cast"].NewRow();
          row["MovieId"] = (object) movie.MovieId;
          row["ActorName"] = (object) movie.Cast[index].Trim();
          MovieDataSource._dsMovies.Tables["Cast"].Rows.Add(row);
        }
      }
    }

    private MovieData GetMovieData(DataRow r)
    {
      MovieData movieData = new MovieData();
      movieData.MovieId = Convert.ToInt32(r["Id"]);
      movieData.Title = r["Title"].ToString();
      movieData.ReleaseDate = Convert.ToInt32(r["ReleaseDate"]);
      movieData.Rating = Convert.ToInt32(r["Rating"]);
      movieData.Genre = r["Genre"].ToString();
      movieData.Classification = r["Classification"].ToString();
      DataRow[] dataRowArray = MovieDataSource.Movies.Tables["Cast"].Select("MovieId = " + movieData.MovieId.ToString());
      movieData.Cast = new string[dataRowArray.Length];
      for (int index = 0; index < dataRowArray.Length; ++index)
        movieData.Cast[index] = dataRowArray[index]["ActorName"].ToString();
      return movieData;
    }
  }
}
