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
  }
}
