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

      SqlCommand cmd = new SqlCommand("INSERT INTO bands (name, number_of_members) OUTPUT INSERTED.id VALUES (@BandName, @BandMembers)", conn);

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

      SqlCommand cmd = new SqlCommand("SELECT * FROM bands", conn);

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

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM bands", conn);
      cmd.ExecuteNonQuery();
      DB.CloseConnection();
    }
  }
}
