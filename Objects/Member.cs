using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BandTracker.Objects
{
  public class Member
  {
    public int Id {get;set;}
    public string Name {get;set;}
    public int BandId {get;set;}

    public Member(string name, int bandId, int id = 0)
    {
      Id = id;
      Name = name;
      BandId = bandId;
    }


  }
}
