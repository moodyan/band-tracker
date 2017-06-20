using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BandTracker.Objects
{
  public class Band
  {
    private string _bandName;
    private string _members;
    private string _genre;
    private string _information;
    private int _id;

    public Band(string BandName, string Members, string Genre, string Information, int Id = 0)
    {
      _bandName = BandName;
      _members = Members;
      _genre = Genre;
      _information = Information;
      _id = Id;
    }

    public int GetId()
    {
      return _id;
    }
    public string GetBandName()
    {
      return _bandName;
    }
    public string GetMembers()
    {
      return _members;
    }
    public string GetGenre()
    {
      return _genre;
    }
    public string GetInformation()
    {
      return _information;
    }

    public override bool Equals(System.Object otherBand)
    {
      if (!(otherBand is Band))
      {
        return false;
      }
      else
      {
        Band newBand = (Band) otherBand;
        bool idEquality = (this.GetId() == newBand.GetId());
        bool bandNameEquality = (this.GetBandName() == newBand.GetBandName());
        bool membersEquality = (this.GetMembers() == newBand.GetMembers());
        bool genreEquality = (this.GetGenre() == newBand.GetGenre());
        bool infoEquality = (this.GetInformation() == newBand.GetInformation());
        return (idEquality && bandNameEquality && membersEquality && genreEquality && infoEquality);
      }
    }

    public static void DeleteAll()
   {
     SqlConnection conn = DB.Connection();
     conn.Open();
     SqlCommand cmd = new SqlCommand("DELETE FROM bands;", conn);
     cmd.ExecuteNonQuery();
     conn.Close();
   }

   public static List<Band> GetAll()
    {
      List<Band> allBands = new List<Band>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM bands ORDER BY band_name;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int bandId = rdr.GetInt32(0);
        string bandName = rdr.GetString(1);
        string members = rdr.GetString(2);
        string genre = rdr.GetString(3);
        string information = rdr.GetString(4);
        Band newBand = new Band(bandName, members, genre, information, bandId);
        allBands.Add(newBand);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allBands;
    }
    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO bands (band_name, members, genre, information) OUTPUT INSERTED.id VALUES (@BandName, @BandMembers, @BandGenre, @BandInfo)", conn);

      SqlParameter bandNameParameter = new SqlParameter();
      bandNameParameter.ParameterName = "@BandName";
      bandNameParameter.Value = this.GetBandName();

      SqlParameter bandMembersParameter = new SqlParameter();
      bandMembersParameter.ParameterName = "@BandMembers";
      bandMembersParameter.Value = this.GetMembers();

      SqlParameter bandGenreParameter = new SqlParameter();
      bandGenreParameter.ParameterName = "@BandGenre";
      bandGenreParameter.Value = this.GetGenre();

      SqlParameter bandInfoParameter = new SqlParameter();
      bandInfoParameter.ParameterName = "@BandInfo";
      bandInfoParameter.Value = this.GetInformation();

      cmd.Parameters.Add(bandNameParameter);
      cmd.Parameters.Add(bandMembersParameter);
      cmd.Parameters.Add(bandGenreParameter);
      cmd.Parameters.Add(bandInfoParameter);
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
    public static Band Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM bands WHERE id = @BandId;", conn);

      SqlParameter bandIdParameter = new SqlParameter();
      bandIdParameter.ParameterName = "@BandId";
      bandIdParameter.Value = id.ToString();
      cmd.Parameters.Add(bandIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundBandId = 0;
      string foundBandName = null;
      string foundMembers = null;
      string foundGenre = null;
      string foundBandInfo = null;

      while(rdr.Read())
      {
        foundBandId = rdr.GetInt32(0);
        foundBandName = rdr.GetString(1);
        foundMembers = rdr.GetString(2);
        foundGenre = rdr.GetString(3);
        foundBandInfo = rdr.GetString(4);
      }
      Band foundBand = new Band(foundBandName, foundMembers, foundGenre, foundBandInfo, foundBandId);

      if (rdr != null)
      {
       rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundBand;
    }

    public void AddVenue(Venue newVenue)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO bands_venues (bands_id, venues_id) OUTPUT INSERTED.venues_id VALUES (@BandId, @VenueId);", conn);

      SqlParameter bandIdParameter = new SqlParameter();
      bandIdParameter.ParameterName = "@BandId";
      bandIdParameter.Value = this.GetId();
      cmd.Parameters.Add(bandIdParameter);

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

    public List<Venue> GetVenues()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT venues.* FROM bands JOIN bands_venues ON (bands.id = bands_venues.bands_id) JOIN venues ON (bands_venues.venues_id = venues.id) WHERE bands.id = @BandId;", conn);
      SqlParameter BandIdParam = new SqlParameter();
      BandIdParam.ParameterName = "@BandId";
      BandIdParam.Value = this.GetId().ToString();

      cmd.Parameters.Add(BandIdParam);

      SqlDataReader rdr = cmd.ExecuteReader();

      List<Venue> venues = new List<Venue>{};

      while(rdr.Read())
      {
        int venueId = rdr.GetInt32(0);
        string venueName = rdr.GetString(1);
        string venueLocation = rdr.GetString(2);
        string venueDetails = rdr.GetString(3);
        Venue newVenue = new Venue(venueName, venueLocation, venueDetails, venueId);
        venues.Add(newVenue);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return venues;
    }
    public List<Show> GetShows()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM shows WHERE id = @ShowId;", conn);
      SqlParameter showIdParameter = new SqlParameter();
      showIdParameter.ParameterName = "@ShowId";
      showIdParameter.Value = this.GetId();

      cmd.Parameters.Add(showIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      List<Show> shows = new List<Show> {};
      while(rdr.Read())
      {
        int showId = rdr.GetInt32(0);
        string cityState = rdr.GetString(1);
        DateTime date = rdr.GetDateTime(2);
        int bandId = rdr.GetInt32(3);
        int venueId = rdr.GetInt32(4);
        Show newShow = new Show(cityState, date, bandId, venueId, showId);
        shows.Add(newShow);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return shows;
    }
    public void Delete()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM bands WHERE id = @BandId; DELETE FROM bands_venues WHERE bands_id = @BandId;", conn);
      SqlParameter bandIdParameter = new SqlParameter();
      bandIdParameter.ParameterName = "@BandId";
      bandIdParameter.Value = this.GetId();

      cmd.Parameters.Add(bandIdParameter);
      cmd.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close();
      }
    }
  }
}
