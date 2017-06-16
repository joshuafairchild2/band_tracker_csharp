using Xunit;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BandTracker.Objects
{
  [Collection("BandTracker")]

  public class MemberTest : IDisposable
  {
    public MemberTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=band_tracker_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Member_GetAll_DatabaseEmptyOnload()
    {
      List<Member> testList = Member.GetAll();
      List<Member> controlList = new List<Member>{};

      Assert.Equal(controlList, testList);
    }

    [Fact]
    public void Member_Save_SaveToDatabase()
    {
      Band newBand = new Band("The Beatles", 4);
      Member newMember = new Member("John", newBand.Id);
      newMember.Save();

      Member testMember = Member.GetAll()[0];
      Assert.Equal(newMember, testMember);
    }

    [Fact]
    public void Member_Equals_MemberEqualsMember()
    {
      Band newBand = new Band("The Beatles", 4);
      Member controlMember = new Member("John", newBand.Id);
      Member testMember = new Member("John", newBand.Id);

      Assert.Equal(controlMember, testMember);
    }

    [Fact]
    public void Member_Find_FindsMemberById()
    {
      Band newBand = new Band("The Beatles", 4);
      Member controlMember = new Member("John", newBand.Id);
      controlMember.Save();

      Member testMember = Member.Find(controlMember.Id);

      Assert.Equal(controlMember, testMember);
    }

    public void Dispose()
    {
      Member.DeleteAll();
      Band.DeleteAll();
    }
  }
}
