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

    [Fact]
    public void Band_Update_UpdateBandInfo()
    {
      Band band = new Band("The Beatles", 4);
      band.Save();

      Band controlBand = new Band("Phantogram", 2, band.Id);
      band.Update("Phantogram", 2);

      Assert.Equal(controlBand, band);
    }

    [Fact]
    public void Band_AddVenue_CreatesRelationShipInDB()
    {
      Band band = new Band("The Beatles", 4);
      band.Save();
      Venue venue = new Venue("Wonder Ballroom", "128 NE Russell St");
      venue.Save();

      band.AddVenue(venue);

      List<Venue> testList = band.GetVenues();
      List<Venue> controlList = new List<Venue>{venue};

      Assert.Equal(controlList, testList);
    }

    [Fact]
    public void Band_GetMembers_ReturnsListOfMembers()
    {
      Band band = new Band("The Beatles", 4);
      band.Save();
      Band band2 = new Band("Phantogram", 2);
      band2.Save();

      Member member1 = new Member("John", band.Id);
      member1.Save();
      Member member2 = new Member("John", band.Id);
      member2.Save();
      Member member3 = new Member("John", band2.Id);
      member3.Save();

      List<Member> testList = band.GetMembers();
      List<Member> controlList = new List<Member>{member1, member2};

      Assert.Equal(controlList, testList);
    }

    public void Dispose()
    {
      Band.DeleteAll();
      Venue.DeleteAll();
      Member.DeleteAll();
    }
  }
}
