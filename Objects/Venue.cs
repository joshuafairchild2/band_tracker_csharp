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
      return null;
    }
  }
}
