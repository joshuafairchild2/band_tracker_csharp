using Xunit;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BandTracker.Objects
{
  [Collection("BandTracker")]

  public class VenueTest : IDisposable
  {
    public VenueTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=band_tracker_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Venue_GetAll_DatabaseEmptyOnload()
    {
      List<Venue> testList = Venue.GetAll();
      List<Venue> controlList = new List<Venue>{};

      Assert.Equal(controlList, testList);
    }

    [Fact]
    public void Venue_Save_SaveToDatabase()
    {
      Venue newVenue = new Venue("The Overlook", "123 1st Street");
      newVenue.Save();

      Venue testVenue = Venue.GetAll()[0];
      Assert.Equal(newVenue, testVenue);
    }

    [Fact]
    public void Venue_Equals_VenueEqualsVenue()
    {
      Venue controlVenue = new Venue("The Overlook", "123 1st Street");
      Venue testVenue = new Venue("The Overlook", "123 1st Street");

      Assert.Equal(controlVenue, testVenue);
    }

    [Fact]
    public void Venue_Find_FindsVenueById()
    {
      Venue controlVenue = new Venue("The Overlook", "123 1st Street");
      controlVenue.Save();

      Venue testVenue = Venue.Find(controlVenue.Id);

      Assert.Equal(controlVenue, testVenue);
    }

    [Fact]
    public void Venue_Delete_DeletesSingleVenue()
    {
      Venue venue1 = new Venue("The Overlook", "123 1st Street");
      venue1.Save();
      Venue venue2 = new Venue("Wonder Ballroom", "128 NE Russell St");
      venue2.Save();

      venue1.Delete();

      List<Venue> testList = Venue.GetAll();
      List<Venue> controlList = new List<Venue>{venue2};

      Assert.Equal(controlList, testList);
    }

    [Fact]
    public void Venue_Update_UpdateVenueInfo()
    {
      Venue venue = new Venue("The Overlook", "123 1st Street");
      venue.Save();

      Venue controlVenue = new Venue("Wonder Ballroom", "128 NE Russell St", venue.Id);
      venue.Update("Wonder Ballroom", "128 NE Russell St");

      Assert.Equal(controlVenue, venue);
    }

    [Fact]
    public void Venue_AddBand_CreatesRelationShipInDB()
    {
      Band band = new Band("The Beatles", 4);
      band.Save();
      Venue venue = new Venue("Wonder Ballroom", "128 NE Russell St");
      venue.Save();

      venue.AddBand(band);

      List<Band> testList = venue.GetBands();
      List<Band> controlList = new List<Band>{band};

      Assert.Equal(controlList, testList);
    }

    [Fact]
    public void Venue_DeleteBandRelationship_DeletesRelationship()
    {
      Venue venue = new Venue("The Overlook", "123 1st Street");
      venue.Save();
      Band band1 = new Band("The Beatles", 4);
      band1.Save();
      Band band2 = new Band("Phantogram", 2);
      band2.Save();

      venue.AddBand(band1);
      venue.AddBand(band2);
      venue.DeleteBandRelationship(band1);

      List<Band> testList = venue.GetBands();
      List<Band> controlList = new List<Band>{band2};

      Assert.Equal(controlList, testList);
    }

    public void Dispose()
    {
      Venue.DeleteAll();
      Band.DeleteAll();
    }
  }
}
