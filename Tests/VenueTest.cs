using Xunit;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BandTracker.Objects;

namespace BandTracker
{
  [Collection("BandTrackerTest")]
  public class VenueTest : IDisposable
  {
    public VenueTest()
    {
      DBConfiguration.ConnectionString  = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=band_tracker_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
      int result = Venue.GetAll().Count;
      Assert.Equal(0, result);
    }
    [Fact]
    public void Test_Equal_ReturnsTrueIfVenuesAreTheSame()
    {
      Venue firstVenue = new Venue("Crystal Ballroom", "Portland, OR", "Details about the Crystal Ballroom venue.");
      Venue secondVenue = new Venue("Crystal Ballroom", "Portland, OR", "Details about the Crystal Ballroom venue.");
      Assert.Equal(firstVenue, secondVenue);
    }
    [Fact]
    public void Test_Save_ToVenueDatabase()
    {
      Venue testVenue = new Venue("Crystal Ballroom", "Portland, OR", "Details about the Crystal Ballroom venue.");
      testVenue.Save();

      List<Venue> result = Venue.GetAll();
      List<Venue> testList = new List<Venue>{testVenue};
      Assert.Equal(testList, result);
    }
    [Fact]
    public void Test_Save_AssignsIdToObject()
    {
      Venue testVenue = new Venue("Crystal Ballroom", "Portland, OR", "Details about the Crystal Ballroom venue.");
      testVenue.Save();

      int testId = testVenue.GetId();
      int savedVenueId = Venue.GetAll()[0].GetId();
      Assert.Equal(testId, savedVenueId);
    }
    [Fact]
    public void Test_Find_FindsVenueInDatabase()
    {
      Venue testVenue = new Venue("Crystal Ballroom", "Portland, OR", "Details about the Crystal Ballroom venue.");
      testVenue.Save();

      Venue foundVenue = Venue.Find(testVenue.GetId());
      Assert.Equal(testVenue, foundVenue);
    }
    public void Dispose()
    {
      Venue.DeleteAll();
      Band.DeleteAll();
    }
  }
}
