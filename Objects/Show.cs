using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BandTracker.Objects
{
  public class Show
  {
    private string _cityState;
    private DateTime _date;
    private int _id;

    public Show(string CityState, DateTime Date, int Id = 0)
    {
      _cityState = CityState;
      _date = Date;
      _id = Id;
    }

    public int GetId()
    {
      return _id;
    }
    public string GetCityState()
    {
      return _cityState;
    }
    public DateTime GetDate()
    {
      return _date;
    }

    public override bool Equals(System.Object otherShow)
    {
      if (!(otherShow is Show))
      {
        return false;
      }
      else
      {
        Show newShow = (Show) otherShow;
        bool idEquality = (this.GetId() == newShow.GetId());
        bool cityStateEquality = (this.GetCityState() == newShow.GetCityState());
        bool dateEquality = (this.GetDate() == newShow.GetDate());
        return (idEquality && cityStateEquality && dateEquality);
      }
    }

    public static void DeleteAll()
   {
     SqlConnection conn = DB.Connection();
     conn.Open();
     SqlCommand cmd = new SqlCommand("DELETE FROM shows;", conn);
     cmd.ExecuteNonQuery();
     conn.Close();
   }

   public static List<Show> GetAll()
    {
      List<Show> allShows = new List<Show>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM shows ORDER BY date;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int showId = rdr.GetInt32(0);
        string cityState = rdr.GetString(1);
        DateTime date = rdr.GetDateTime(2);
        Show newShow = new Show(cityState, date, showId);
        allShows.Add(newShow);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allShows;
    }
    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO shows (city_state, date) OUTPUT INSERTED.id VALUES (@ShowCityState, @ShowDate)", conn);

      SqlParameter showCityStateParameter = new SqlParameter();
      showCityStateParameter.ParameterName = "@ShowCityState";
      showCityStateParameter.Value = this.GetCityState();

      SqlParameter showDateParameter = new SqlParameter();
      showDateParameter.ParameterName = "@ShowDate";
      showDateParameter.Value = this.GetDate();

      cmd.Parameters.Add(showCityStateParameter);
      cmd.Parameters.Add(showDateParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
    }
    public static Show Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM shows WHERE id = @ShowId;", conn);

      SqlParameter showIdParameter = new SqlParameter();
      showIdParameter.ParameterName = "@ShowId";
      showIdParameter.Value = id.ToString();
      cmd.Parameters.Add(showIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundShowId = 0;
      string foundCityState = null;
      DateTime foundDate = default(DateTime);

      while(rdr.Read())
      {
        foundShowId = rdr.GetInt32(0);
        foundCityState = rdr.GetString(1);
        foundDate = rdr.GetDateTime(2);
      }
      Show foundShow = new Show(foundCityState, foundDate, foundShowId);

      if (rdr != null)
     {
       rdr.Close();
     }
     if (conn != null)
     {
       conn.Close();
     }
     return foundShow;
    }
    public void AddVenue(Venue newVenue)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO bands_venues_shows (venues_id, shows_id) OUTPUT INSERTED.venues_id VALUES (@VenueId, @ShowId);", conn);

      SqlParameter showIdParameter = new SqlParameter();
      showIdParameter.ParameterName = "@ShowId";
      showIdParameter.Value = this.GetId();
      cmd.Parameters.Add(showIdParameter);

      SqlParameter venueIdParameter = new SqlParameter();
      venueIdParameter.ParameterName = "@VenueId";
      venueIdParameter.Value = newVenue.GetId();
      cmd.Parameters.Add(venueIdParameter);

      cmd.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close();
      }
    }

    public List<Venue> GetVenue()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT venues.* FROM shows JOIN bands_venues_shows ON (shows.id = bands_venues_shows.shows_id) JOIN venues ON (bands_venues_shows.venues_id = venues.id) WHERE shows.id = @ShowId;", conn);
      SqlParameter showIdParam = new SqlParameter();
      showIdParam.ParameterName = "@ShowId";
      showIdParam.Value = this.GetId().ToString();

      cmd.Parameters.Add(showIdParam);

      SqlDataReader rdr = cmd.ExecuteReader();

      List<Venue> venue = new List<Venue>{};

      while(rdr.Read())
      {
        int venueId = rdr.GetInt32(0);
        string venueName = rdr.GetString(1);
        string venueLocation = rdr.GetString(2);
        string venueDetails = rdr.GetString(3);
        Venue newVenue = new Venue(venueName, venueLocation, venueDetails, venueId);
        venue.Add(newVenue);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return venue;
    }
    public void UpdateDate(string newDate)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand ("UPDATE shows SET date = @NewDate OUTPUT INSERTED.date WHERE id = @ShowId;", conn);

      cmd.Parameters.AddWithValue("@NewDate", newDate);
      cmd.Parameters.AddWithValue("@ShowId", _id);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._date = rdr.GetDateTime(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }
    public void Delete()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM shows WHERE id = @ShowId; DELETE FROM bands_shows WHERE shows_id = @ShowId;", conn);
      SqlParameter showIdParameter = new SqlParameter();
      showIdParameter.ParameterName = "@ShowId";
      showIdParameter.Value = this.GetId();

      cmd.Parameters.Add(showIdParameter);
      cmd.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close();
      }
    }
    public static List<Show> SearchShowCityState(string searchShowCityState)
    {
      List<Show> allShows = new List<Show>{};

      SqlConnection conn = DB.Connection();
      conn.Open();


      SqlCommand cmd = new SqlCommand("SELECT * FROM shows WHERE city_state LIKE @ShowCityState;", conn);

      SqlParameter showCityStateParam = new SqlParameter();
      showCityStateParam.ParameterName = "@ShowCityState";
      showCityStateParam.Value = "%" + searchShowCityState + "%";

      cmd.Parameters.Add(showCityStateParam);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int showId = rdr.GetInt32(0);
        string cityState = rdr.GetString(1);
        DateTime date = rdr.GetDateTime(2);
        Show newShow = new Show(cityState, date, showId);
        allShows.Add(newShow);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allShows;
    }
  }
}
