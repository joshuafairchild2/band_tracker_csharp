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

    public void Dispose()
    {

    }
  }
}
