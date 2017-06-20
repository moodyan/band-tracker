using Xunit;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BandTracker.Objects;

namespace BandTracker
{
  [Collection("BandTrackerTest")]
  public class BandTest : IDisposable
  {
    public BandTest()
    {
      DBConfiguration.ConnectionString  = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=band_tracker_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
      int result = Band.GetAll().Count;
      Assert.Equal(0, result);
    }
    [Fact]
    public void Test_Equal_ReturnsTrueIfBandsAreTheSame()
    {
      Band firstBand = new Band("Bon Iver", "Justin Vernon", "Indie Folk", "Information about the band Bon Iver.");
      Band secondBand = new Band("Bon Iver", "Justin Vernon", "Indie Folk", "Information about the band Bon Iver.");
      Assert.Equal(firstBand, secondBand);
    }
    [Fact]
    public void Test_Save_ToBandDatabase()
    {
      Band testBand = new Band("Bon Iver", "Justin Vernon", "Indie Folk", "Information about the band Bon Iver.");
      testBand.Save();

      List<Band> result = Band.GetAll();
      List<Band> testList = new List<Band>{testBand};
      Assert.Equal(testList, result);
    }
    [Fact]
    public void Test_Save_AssignsIdToObject()
    {
      Band testBand = new Band("Bon Iver", "Justin Vernon", "Indie Folk", "Information about the band Bon Iver.");
      testBand.Save();

      int testId = testBand.GetId();
      int savedBandId = Band.GetAll()[0].GetId();
      Assert.Equal(testId, savedBandId);
    }
    [Fact]
    public void Test_Find_FindsBandInDatabase()
    {
      Band testBand = new Band("Bon Iver", "Justin Vernon", "Indie Folk", "Information about the band Bon Iver.");
      testBand.Save();

      Band foundBand = Band.Find(testBand.GetId());
      Assert.Equal(testBand, foundBand);
    }
    [Fact]
    public void TestVenue_AddsVenueToBand_VenueList()
    {
      Band testBand = new Band("Bon Iver", "Justin Vernon", "Indie Folk", "Information about the band Bon Iver.");
      testBand.Save();

      Venue testVenue = new Venue("Crystal Ballroom", "Portland, OR", "Details about the Crystal Ballroom venue.");
      testVenue.Save();

      testBand.AddVenue(testVenue);

      List<Venue> result = testBand.GetVenues();
      List<Venue> testList = new List<Venue>{testVenue};

      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_ReturnsAllBandsVenues_VenueList()
    {
      Band testBand = new Band("Bon Iver", "Justin Vernon", "Indie Folk", "Information about the band Bon Iver.");
      testBand.Save();

      Venue testVenue1 = new Venue("Crystal Ballroom", "Portland, OR", "Details about the Crystal Ballroom venue.");
      testVenue1.Save();

      Venue testVenue2 = new Venue("Doug Fir Lounge", "Portland, OR", "Details about the Doug Fir Lounge venue.");
      testVenue2.Save();

      testBand.AddVenue(testVenue1);
      testBand.AddVenue(testVenue2);
      List<Venue> result = testBand.GetVenues();
      List<Venue> testList = new List<Venue> {testVenue1, testVenue2};

      Assert.Equal(testList, result);
    }
    // [Fact]
    // public void TestShow_AddsShowToBand_ShowList()
    // {
    //   Band testBand = new Band("Bon Iver", "Justin Vernon", "Indie Folk", "Information about the band Bon Iver.");
    //   testBand.Save();
    //
    //   Show testShow = new Show("Portland, OR", new DateTime(2017, 06, 16));
    //   testShow.Save();
    //
    //   testBand.AddShow(testShow);
    //
    //   List<Show> result = testBand.GetShows();
    //   List<Show> testList = new List<Show>{testShow};
    //
    //   Assert.Equal(testList, result);
    // }
    //
    // [Fact]
    // public void Test_ReturnsAllBandsShows_ShowList()
    // {
    //   Band testBand = new Band("Bon Iver", "Justin Vernon", "Indie Folk", "Information about the band Bon Iver.");
    //   testBand.Save();
    //
    //   Show testShow1 = new Show("Portland, OR", new DateTime(2017, 06, 16));
    //   testShow1.Save();
    //
    //   Show testShow2 = new Show("Seattle, WA", new DateTime(2017, 06, 17));
    //   testShow2.Save();
    //
    //   testBand.AddShow(testShow1);
    //   testBand.AddShow(testShow2);
    //   List<Show> result = testBand.GetShows();
    //   List<Show> testList = new List<Show> {testShow1, testShow2};
    //
    //   Assert.Equal(testList, result);
    // }
    [Fact]
    public void Delete_DeletesBandAssociationsFromDataBase_BandList()
    {
      Venue testVenue = new Venue("Doug Fir Lounge", "Portland, OR", "Details about the Doug Fir Lounge venue.");
      testVenue.Save();

      Band testBand = new Band("Bon Iver", "Justin Vernon", "Indie Folk", "Information about the band Bon Iver.");
      testBand.Save();

      testBand.AddVenue(testVenue);
      testBand.Delete();

      List<Band> result = testVenue.GetBands();
      List<Band> test = new List<Band>{};

      Assert.Equal(test, result);
    }
    public void Dispose()
    {
      Band.DeleteAll();
      Venue.DeleteAll();
      Show.DeleteAll();
    }
  }
}
