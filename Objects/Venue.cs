using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BandTracker.Objects
{
  public class Venue
  {
    private string _venueName;
    private string _location;
    private string _details;
    private int _id;

    public Venue(string VenueName, string Location, string Details, int Id = 0)
    {
      _venueName = VenueName;
      _location = Location;
      _details = Details;
      _id = Id;
    }

    public int GetId()
    {
      return _id;
    }
    public string GetVenueName()
    {
      return _venueName;
    }
    public string GetLocation()
    {
      return _location;
    }
    public string GetDetails()
    {
      return _details;
    }

    public override bool Equals(System.Object otherVenue)
    {
      if (!(otherVenue is Venue))
      {
        return false;
      }
      else
      {
        Venue newVenue = (Venue) otherVenue;
        bool idEquality = (this.GetId() == newVenue.GetId());
        bool venueNameEquality = (this.GetVenueName() == newVenue.GetVenueName());
        bool locationEquality = (this.GetLocation() == newVenue.GetLocation());
        bool detailsEquality = (this.GetDetails() == newVenue.GetDetails());
        return (idEquality && venueNameEquality && locationEquality && detailsEquality);
      }
    }

    public static void DeleteAll()
   {
     SqlConnection conn = DB.Connection();
     conn.Open();
     SqlCommand cmd = new SqlCommand("DELETE FROM venues;", conn);
     cmd.ExecuteNonQuery();
     conn.Close();
   }

   public static List<Venue> GetAll()
    {
      List<Venue> allVenues = new List<Venue>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM venues ORDER BY venue_name;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int venueId = rdr.GetInt32(0);
        string venueName = rdr.GetString(1);
        string location = rdr.GetString(2);
        string details = rdr.GetString(3);
        Venue newVenue = new Venue(venueName, location, details, venueId);
        allVenues.Add(newVenue);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allVenues;
    }
    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO venues (venue_name, location, details) OUTPUT INSERTED.id VALUES (@VenueName, @VenueLocation, @VenueDetails)", conn);

      SqlParameter venueNameParameter = new SqlParameter();
      venueNameParameter.ParameterName = "@VenueName";
      venueNameParameter.Value = this.GetVenueName();

      SqlParameter venueLocationParameter = new SqlParameter();
      venueLocationParameter.ParameterName = "@VenueLocation";
      venueLocationParameter.Value = this.GetLocation();

      SqlParameter venueDetailsParameter = new SqlParameter();
      venueDetailsParameter.ParameterName = "@VenueDetails";
      venueDetailsParameter.Value = this.GetDetails();

      cmd.Parameters.Add(venueNameParameter);
      cmd.Parameters.Add(venueLocationParameter);
      cmd.Parameters.Add(venueDetailsParameter);
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
    public static Venue Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM venues WHERE id = @VenueId;", conn);

      SqlParameter venueIdParameter = new SqlParameter();
      venueIdParameter.ParameterName = "@VenueId";
      venueIdParameter.Value = id.ToString();
      cmd.Parameters.Add(venueIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundVenueId = 0;
      string foundVenueName = null;
      string foundLocation = null;
      string foundDetails = null;

      while(rdr.Read())
      {
        foundVenueId = rdr.GetInt32(0);
        foundVenueName = rdr.GetString(1);
        foundLocation = rdr.GetString(2);
        foundDetails = rdr.GetString(3);
      }
      Venue foundVenue = new Venue(foundVenueName, foundLocation, foundDetails, foundVenueId);

      if (rdr != null)
     {
       rdr.Close();
     }
     if (conn != null)
     {
       conn.Close();
     }
     return foundVenue;
    }
    public void AddBand(Band newBand)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO bands_venues_shows (venues_id, bands_id) VALUES (@VenueId, @BandId);", conn);
      SqlParameter venueIdParameter = new SqlParameter();
      venueIdParameter.ParameterName = "@VenueId";
      venueIdParameter.Value = this.GetId();
      cmd.Parameters.Add(venueIdParameter);

      SqlParameter bandIdParameter = new SqlParameter();
      bandIdParameter.ParameterName = "@BandId";
      bandIdParameter.Value = newBand.GetId();
      cmd.Parameters.Add(bandIdParameter);

      cmd.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close();
      }
    }
    public List<Band> GetBands()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT bands.* FROM venues JOIN bands_venues_shows ON (venues.id = bands_venues_shows.venues_id) JOIN bands ON (bands_venues_shows.bands_id = bands.id) WHERE venues.id = @VenueId;", conn);
      SqlParameter venueIdParameter = new SqlParameter();
      venueIdParameter.ParameterName = "@VenueId";
      venueIdParameter.Value = this.GetId();

      cmd.Parameters.Add(venueIdParameter);

      SqlDataReader rdr = cmd.ExecuteReader();

      List<Band> bands = new List<Band> {};

      while(rdr.Read())
      {
        int thisBandId = rdr.GetInt32(0);
        string bandName = rdr.GetString(1);
        string members = rdr.GetString(2);
        string genre = rdr.GetString(3);
        string bandInfo = rdr.GetString(4);
        Band foundBand = new Band(bandName, members, genre, bandInfo, thisBandId);
        bands.Add(foundBand);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return bands;
    }
    public void UpdateDetails(string newDetails)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand ("UPDATE venues SET details = @NewDetails OUTPUT INSERTED.details WHERE id = @VenueId;", conn);

      cmd.Parameters.AddWithValue("@NewDetails", newDetails);
      cmd.Parameters.AddWithValue("@VenueId", _id);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._details = rdr.GetString(0);
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

      SqlCommand cmd = new SqlCommand("DELETE FROM venues WHERE id = @VenueId; DELETE FROM bands_venues_shows WHERE venues_id = @VenueId;", conn);
      SqlParameter venueIdParameter = new SqlParameter();
      venueIdParameter.ParameterName = "@VenueId";
      venueIdParameter.Value = this.GetId();

      cmd.Parameters.Add(venueIdParameter);
      cmd.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close();
      }
    }
    public static List<Venue> SearchVenueLocation(string searchVenueLocation)
    {
      List<Venue> allVenues = new List<Venue>{};

      SqlConnection conn = DB.Connection();
      conn.Open();


      SqlCommand cmd = new SqlCommand("SELECT * FROM venues WHERE location LIKE @VenueLocation;", conn);

      SqlParameter venueLocationParam = new SqlParameter();
      venueLocationParam.ParameterName = "@VenueLocation";
      venueLocationParam.Value = "%" + searchVenueLocation + "%";

      cmd.Parameters.Add(venueLocationParam);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int venueId = rdr.GetInt32(0);
        string venueName = rdr.GetString(1);
        string location = rdr.GetString(2);
        string details = rdr.GetString(3);
        Venue newVenue = new Venue(venueName, location, details, venueId);
        allVenues.Add(newVenue);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allVenues;
    }
  }
}
