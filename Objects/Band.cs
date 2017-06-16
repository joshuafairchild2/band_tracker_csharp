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

    public static List<Band> GetAll()
    {
      return null;
    }
  }
}
