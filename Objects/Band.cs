using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BandTracker.Objects
{
  public class Band
  {
    public int Id {get;set;}
    public string Name {get;set;}
    public int NumberOfMembers {get;set;}

    public Band()
    {
      Id = 0;
      Name = null;
      NumberOfMembers = 0;
    }

    public Band(string name, int numberOfMembers, int id = 0)
    {
      Id = id;
      Name = name;
      NumberOfMembers = numberOfMembers;
    }

    public override bool Equals(System.Object otherBand)
    {
      if (!(otherBand is Band))
      {
        return false;
      }
      else
      {
        Band newBand = (Band) otherBand;
        return (this.Id == newBand.Id &&
                this.Name == newBand.Name &&
                this.NumberOfMembers == newBand.NumberOfMembers);
      }
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO bands (name, number_of_members) OUTPUT INSERTED.id VALUES (@BandName, @BandMembers);", conn);

      cmd.Parameters.Add(new SqlParameter("@BandName", this.Name));
      cmd.Parameters.Add(new SqlParameter("@BandMembers", this.NumberOfMembers));

      SqlDataReader rdr = cmd.ExecuteReader();
      while(rdr.Read())
      {
        this.Id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      DB.CloseConnection();
    }

    public static List<Band> GetAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM bands;", conn);

      SqlDataReader rdr = cmd.ExecuteReader();

      List<Band> allBands = new List<Band>{};
      while (rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        int numbOfMembs = rdr.GetInt32(2);

        Band newBand = new Band(name, numbOfMembs, id);
        allBands.Add(newBand);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      DB.CloseConnection();

      return allBands;
    }

    public static Band Find(int idToFind)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM bands WHERE id = @BandId;", conn);
      cmd.Parameters.Add(new SqlParameter("BandId", idToFind));

      SqlDataReader rdr = cmd.ExecuteReader();

      Band foundBand = new Band();
      while (rdr.Read())
      {
        foundBand.Id = rdr.GetInt32(0);
        foundBand.Name = rdr.GetString(1);
        foundBand.NumberOfMembers = rdr.GetInt32(2);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      DB.CloseConnection();

      return foundBand;
    }

    public void Update(string newName, int newMemberAmount)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE bands SET name = @BandName, number_of_members = @BandMembers OUTPUT INSERTED.name, INSERTED.number_of_members WHERE id = @BandId;", conn);

      cmd.Parameters.Add(new SqlParameter("@BandName", newName));
      cmd.Parameters.Add(new SqlParameter("@BandMembers", newMemberAmount));
      cmd.Parameters.Add(new SqlParameter("@BandId", this.Id));

      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this.Name = rdr.GetString(0);
        this.NumberOfMembers = rdr.GetInt32(1);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      DB.CloseConnection();
    }

    public void AddVenue(Venue toAdd)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO venues_bands (venue_id, band_id) VALUES (@VenueId, @BandId);", conn);
      cmd.Parameters.Add(new SqlParameter("@VenueId", toAdd.Id));
      cmd.Parameters.Add(new SqlParameter("@BandId", this.Id));

      cmd.ExecuteNonQuery();
      DB.CloseConnection();
    }

    public List<Venue> GetVenues()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT venues.* FROM bands JOIN venues_bands ON (bands.id = venues_bands.band_id) JOIN venues ON (venues.id = venues_bands.venue_id) WHERE band_id = @BandId;", conn);
      cmd.Parameters.Add(new SqlParameter("@BandId", this.Id));

      SqlDataReader rdr = cmd.ExecuteReader();

      List<Venue> venues = new List<Venue>{};
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        string address = rdr.GetString(2);

        Venue newVenue = new Venue(name, address, id);
        venues.Add(newVenue);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      DB.CloseConnection();

      return venues;
    }

    public List<Member> GetMembers()
    {
      return null;
    }

    public void Delete()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM bands WHERE id = @BandId;", conn);
      cmd.Parameters.Add(new SqlParameter("BandId", this.Id));
      cmd.ExecuteNonQuery();
      DB.CloseConnection();
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM bands;", conn);
      cmd.ExecuteNonQuery();
      DB.CloseConnection();
    }
  }
}
