using Xunit;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BandTracker.Objects
{
  [Collection("BandTracker")]

  public class BandTest : IDisposable
  {
    public BandTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=band_tracker_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Band_GetAll_DatabaseEmptyOnload()
    {
      List<Band> testList = Band.GetAll();
      List<Band> controlList = new List<Band>{};

      Assert.Equal(controlList, testList);
    }

    [Fact]
    public void Band_Save_SaveToDatabase()
    {
      Band newBand = new Band("The Beatles", 4);
      newBand.Save();

      Band testBand = Band.GetAll()[0];
      Assert.Equal(newBand, testBand);
    }

    [Fact]
    public void Band_Equals_BandEqualsBand()
    {
      Band controlBand = new Band("The Beatles", 4);
      Band testBand = new Band("The Beatles", 4);

      Assert.Equal(controlBand, testBand);
    }

    [Fact]
    public void Band_Find_FindsBandById()
    {
      Band controlBand = new Band("The Beatles", 4);
      controlBand.Save();

      Band testBand = Band.Find(controlBand.Id);

      Assert.Equal(controlBand, testBand);
    }

    [Fact]
    public void Band_Delete_DeletesSingleBand()
    {
      Band band1 = new Band("The Beatles", 4);
      band1.Save();
      Band band2 = new Band("Phantogram", 2);
      band2.Save();

      band1.Delete();

      List<Band> testList = Band.GetAll();
      List<Band> controlList = new List<Band>{band2};

      Assert.Equal(controlList, testList);
    }

    public void Dispose()
    {
      Band.DeleteAll();
    }
  }
}
