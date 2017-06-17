using Xunit;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BandTracker.Objects;

namespace BandTracker
{
  [Collection("BandTrackerTest")]
  public class ShowTest : IDisposable
  {
    public ShowTest()
    {
      DBConfiguration.ConnectionString  = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=band_tracker_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
      int result = Show.GetAll().Count;
      Assert.Equal(0, result);
    }
    [Fact]
    public void Test_Equal_ReturnsTrueIfShowsAreTheSame()
    {
      Show firstShow = new Show("Portland, OR", new DateTime(2017, 06, 16));
      Show secondShow = new Show("Portland, OR", new DateTime(2017, 06, 16));
      Assert.Equal(firstShow, secondShow);
    }
    [Fact]
    public void Test_Save_ToShowDatabase()
    {
      Show testShow = new Show("Portland, OR", new DateTime(2017, 06, 16));
      testShow.Save();

      List<Show> result = Show.GetAll();
      List<Show> testList = new List<Show>{testShow};
      Assert.Equal(testList, result);
    }
    [Fact]
    public void Test_Save_AssignsIdToObject()
    {
      Show testShow = new Show("Portland, OR", new DateTime(2017, 06, 16));
      testShow.Save();

      int testId = testShow.GetId();
      int savedShowId = Show.GetAll()[0].GetId();
      Assert.Equal(testId, savedShowId);
    }
    [Fact]
    public void Test_Find_FindsShowInDatabase()
    {
      Show testShow = new Show("Portland, OR", new DateTime(2017, 06, 16));
      testShow.Save();

      Show foundShow = Show.Find(testShow.GetId());
      Assert.Equal(testShow, foundShow);
    }
    [Fact]
    public void TestVenue_AddsVenueToShow_VenueList()
    {
      Show testShow = new Show("Portland, OR", new DateTime(2017, 06, 16));
      testShow.Save();

      Venue testVenue = new Venue("Crystal Ballroom", "Portland, OR", "Details about the Crystal Ballroom venue.");
      testVenue.Save();

      testShow.AddVenue(testVenue);

      List<Venue> result = testShow.GetVenue();
      List<Venue> testList = new List<Venue>{testVenue};

      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_ReturnsAllShowsVenues_VenueList()
    {
      Show testShow = new Show("Portland, OR", new DateTime(2017, 06, 16));
      testShow.Save();

      Venue testVenue1 = new Venue("Crystal Ballroom", "Portland, OR", "Details about the Crystal Ballroom venue.");
      testVenue1.Save();

      testShow.AddVenue(testVenue1);
      List<Venue> result = testShow.GetVenue();
      List<Venue> testList = new List<Venue> {testVenue1};

      Assert.Equal(testList, result);
    }
    // [Fact]
    // public void TestBand_AddsBandToShow_BandList()
    // {
    //   Show testShow = new Show("Crystal Ballroom", "Portland, OR", "Details about the Crystal Ballroom show.");
    //   testShow.Save();
    //
    //   Band testBand = new Band("Bon Iver", "Justin Vernon", "Indie Folk", "Information about the band Bon Iver.");
    //   testBand.Save();
    //
    //   testShow.AddBand(testBand);
    //
    //   List<Band> result = testShow.GetBands();
    //   List<Band> testList = new List<Band>{testBand};
    //
    //   Assert.Equal(testList, result);
    // }
    //
    // [Fact]
    // public void Test_ReturnsAllShowsBands_BandList()
    // {
    //   Show testShow = new Show("Crystal Ballroom", "Portland, OR", "Details about the Crystal Ballroom show.");
    //   testShow.Save();
    //
    //   Band testBand1 = new Band("Bon Iver", "Justin Vernon", "Indie Folk", "Information about the band Bon Iver.");
    //   testBand1.Save();
    //
    //   Band testBand2 = new Band("Band of Horses", "Ben Bridwell, Creighton Barrett, Ryan Monroe", "Indie Rock", "Information about the Band of Horses band.");
    //   testBand2.Save();
    //
    //   testShow.AddBand(testBand1);
    //   testShow.AddBand(testBand2);
    //   List<Band> result = testShow.GetBands();
    //   List<Band> testList = new List<Band> {testBand1, testBand2};
    //
    //   Assert.Equal(testList, result);
    // }
    // public void Delete_DeletesShowAssociationsFromDataBase_ShowList()
    // {
    //   Band testBand = new Band("Bon Iver", "Justin Vernon", "Indie Folk", "Information about the band Bon Iver.");
    //   testBand.Save();
    //
    //   Show testShow = new Show("Crystal Ballroom", "Portland, OR", "Details about the Crystal Ballroom show.");
    //   testShow.Save();
    //
    //   testShow.AddBand(testBand);
    //   testShow.Delete();
    //
    //   List<Show> result = testBand.GetShows();
    //   List<Show> test = new List<Show>{};
    //
    //   Assert.Equal(test, result);
    // }
    [Fact]
    public void Test_Search_SearchShowByCityState()
    {
      Show testShow1 = new Show("Portland, OR", new DateTime(2017, 06, 16));
      testShow1.Save();

      Show testShow2 = new Show("Portland, OR", new DateTime(2017, 08, 17));
      testShow2.Save();

      Show testShow3 = new Show("Seattle, OR", new DateTime(2017, 06, 18));
      testShow3.Save();

      List<Show> searchedShowInput = Show.SearchShowCityState("portland");

      List<Show> Result = new List<Show>{testShow1, testShow2};

      Assert.Equal(Result, searchedShowInput);
    }
    public void Dispose()
    {
      Show.DeleteAll();
      Band.DeleteAll();
      Venue.DeleteAll();
    }
  }
}
