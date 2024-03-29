using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BandTracker.Objects
{
  public class Venue
  {
    public int Id {get;set;}
    public string Name {get;set;}
    public string Address {get;set;}

    public Venue()
    {
      Id = 0;
      Name = null;
      Address = null;
    }

    public Venue(string name, string address, int id = 0)
    {
      Id = id;
      Name = name;
      Address = address;
    }

    public override bool Equals(System.Object otherVenue)
    {
      if (!(otherVenue is Venue))
      {
        return false;
      }
      else
      {
        Venue newVenue = (Venue) otherVenue;
        return (this.Id == newVenue.Id &&
                this.Name == newVenue.Name &&
                this.Address == newVenue.Address);
      }
    }

    public static List<Venue> GetAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM venues", conn);

      SqlDataReader rdr = cmd.ExecuteReader();

      List<Venue> allVenues = new List<Venue>{};
      while (rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        string address = rdr.GetString(2);

        Venue newVenue = new Venue(name, address, id);
        allVenues.Add(newVenue);
      }


      if (rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }

      return allVenues;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO venues (name, address) OUTPUT INSERTED.id VALUES (@VenueName, @VenueAddress)", conn);

      cmd.Parameters.Add(new SqlParameter("@VenueName", this.Name));
      cmd.Parameters.Add(new SqlParameter("@VenueAddress", this.Address));

      SqlDataReader rdr = cmd.ExecuteReader();
      while(rdr.Read())
      {
        this.Id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
    }

    public static Venue Find(int idToFind)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM venues WHERE id = @VenueId", conn);
      cmd.Parameters.Add(new SqlParameter("@VenueId", idToFind));

      SqlDataReader rdr = cmd.ExecuteReader();

      Venue foundVenue = new Venue();
      while (rdr.Read())
      {
        foundVenue.Id = rdr.GetInt32(0);
        foundVenue.Name = rdr.GetString(1);
        foundVenue.Address = rdr.GetString(2);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }

      return foundVenue;
    }

    public void Update(string newName, string newAddress)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE venues SET name = @VenueName, address = @VenueAddress OUTPUT INSERTED.name, INSERTED.address WHERE id = @VenueId;", conn);

      cmd.Parameters.Add(new SqlParameter("@VenueName", newName));
      cmd.Parameters.Add(new SqlParameter("@VenueAddress", newAddress));
      cmd.Parameters.Add(new SqlParameter("@VenueId", this.Id));

      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this.Name = rdr.GetString(0);
        this.Address = rdr.GetString(1);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
    }

    public void AddBand(Band toAdd)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO venues_bands (venue_id, band_id) VALUES (@VenueId, @BandId);", conn);
      cmd.Parameters.Add(new SqlParameter("@VenueId", this.Id));
      cmd.Parameters.Add(new SqlParameter("@BandId", toAdd.Id));

      cmd.ExecuteNonQuery();
      if(conn != null)
      {
        conn.Close();
      }
    }

    public List<Band> GetBands()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT bands.* FROM venues JOIN venues_bands ON (venues.id = venues_bands.venue_id) JOIN bands ON (bands.id = venues_bands.band_id) WHERE venue_id = @VenueId;", conn);
      cmd.Parameters.Add(new SqlParameter("@VenueId", this.Id));

      SqlDataReader rdr = cmd.ExecuteReader();

      List<Band> bands = new List<Band>{};
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        int numbOfMembs = rdr.GetInt32(2);

        Band newBand = new Band(name, numbOfMembs, id);
        bands.Add(newBand);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }

      return bands;
    }

    public void DeleteBandRelationship(Band toDelete)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM venues_bands WHERE band_id = @BandId;", conn);

      cmd.Parameters.Add(new SqlParameter("@BandId", toDelete.Id));
      cmd.ExecuteNonQuery();
      if(conn != null)
      {
        conn.Close();
      }
    }

    public void Delete()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM venues WHERE id = @VenueId; DELETE FROM venues_bands WHERE venue_id = @VenueId", conn);
      cmd.Parameters.Add(new SqlParameter("@VenueId", this.Id));
      cmd.ExecuteNonQuery();
      if(conn != null)
      {
        conn.Close();
      }
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM venues; DELETE FROM venues_bands;", conn);
      cmd.ExecuteNonQuery();
      if(conn != null)
      {
        conn.Close();
      }
    }
  }
}
