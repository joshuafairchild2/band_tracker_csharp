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
      DB.CloseConnection();

      return allVenues;
    }
  }
}
