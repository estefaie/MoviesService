// Decompiled with JetBrains decompiler
// Type: MoviesLibrary.MovieData
// Assembly: MoviesLibrary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 099914D2-E4B9-4486-9104-2BC3203248E1
// Assembly location: C:\Coding Challenge\CBA\MoviesLibrary.dll

using System;

namespace MoviesLibrary
{
  [Serializable]
  public class MovieData
  {
    private int _movieId;
    private string _title;
    private string _genre;
    private string _classification;
    private int _releaseDate;
    private int _rating;
    private string[] _cast;

    public int MovieId
    {
      get
      {
        return this._movieId;
      }
      set
      {
        this._movieId = value;
      }
    }

    public string Title
    {
      get
      {
        return this._title;
      }
      set
      {
        this._title = value;
      }
    }

    public string Genre
    {
      get
      {
        return this._genre;
      }
      set
      {
        this._genre = value;
      }
    }

    public string Classification
    {
      get
      {
        return this._classification;
      }
      set
      {
        this._classification = value;
      }
    }

    public int ReleaseDate
    {
      get
      {
        return this._releaseDate;
      }
      set
      {
        this._releaseDate = value;
      }
    }

    public int Rating
    {
      get
      {
        return this._rating;
      }
      set
      {
        this._rating = value;
      }
    }

    public string[] Cast
    {
      get
      {
        return this._cast;
      }
      set
      {
        this._cast = value;
      }
    }
  }
}
