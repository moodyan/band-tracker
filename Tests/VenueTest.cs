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
    [Fact]
    public void TestBand_AddsBandToVenue_BandList()
    {
      Venue testVenue = new Venue("Crystal Ballroom", "Portland, OR", "Details about the Crystal Ballroom venue.");
      testVenue.Save();

      Band testBand = new Band("Bon Iver", "Justin Vernon", "Indie Folk", "Information about the band Bon Iver.");
      testBand.Save();

      testVenue.AddBand(testBand);

      List<Band> result = testVenue.GetBands();
      List<Band> testList = new List<Band>{testBand};

      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_ReturnsAllVenuesBands_BandList()
    {
      Venue testVenue = new Venue("Crystal Ballroom", "Portland, OR", "Details about the Crystal Ballroom venue.");
      testVenue.Save();

      Band testBand1 = new Band("Bon Iver", "Justin Vernon", "Indie Folk", "Information about the band Bon Iver.");
      testBand1.Save();

      Band testBand2 = new Band("Band of Horses", "Ben Bridwell, Creighton Barrett, Ryan Monroe", "Indie Rock", "Information about the Band of Horses band.");
      testBand2.Save();

      testVenue.AddBand(testBand1);
      testVenue.AddBand(testBand2);
      List<Band> result = testVenue.GetBands();
      List<Band> testList = new List<Band> {testBand1, testBand2};

      Assert.Equal(testList, result);
    }
    public void Dispose()
    {
      Venue.DeleteAll();
      Band.DeleteAll();
    }
  }
}
