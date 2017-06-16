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
    public void Dispose()
    {
      Band.DeleteAll();
    }
  }
}
