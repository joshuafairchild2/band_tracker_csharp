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

    public Member()
    {
      Id = 0;
      Name = null;
      BandId = 0;
    }

    public Member(string name, int bandId, int id = 0)
    {
      Id = id;
      Name = name;
      BandId = bandId;
    }

    public override bool Equals(System.Object otherMember)
    {
      if (!(otherMember is Member))
      {
        return false;
      }
      else
      {
        Member newMember = (Member) otherMember;
        return (this.Id == newMember.Id &&
                this.Name == newMember.Name &&
                this.BandId == newMember.BandId);
      }
    }

    public static List<Member> GetAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM members", conn);

      SqlDataReader rdr = cmd.ExecuteReader();

      List<Member> allMembers = new List<Member>{};
      while (rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        int bandId = rdr.GetInt32(2);

        Member newMember = new Member(name, bandId, id);
        allMembers.Add(newMember);
      }


      if (rdr != null)
      {
        rdr.Close();
      }
      DB.CloseConnection();

      return allMembers;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO members (name, band_id) OUTPUT INSERTED.id VALUES (@MemberName, @BandId)", conn);

      cmd.Parameters.Add(new SqlParameter("@MemberName", this.Name));
      cmd.Parameters.Add(new SqlParameter("@BandId", this.BandId));

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

    public static Member Find(int idToFind)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM members WHERE id = @MemberId", conn);
      cmd.Parameters.Add(new SqlParameter("@MemberId", idToFind));

      SqlDataReader rdr = cmd.ExecuteReader();

      Member foundMember = new Member();
      while (rdr.Read())
      {
        foundMember.Id = rdr.GetInt32(0);
        foundMember.Name = rdr.GetString(1);
        foundMember.BandId = rdr.GetInt32(2);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      DB.CloseConnection();

      return foundMember;
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM members; DELETE FROM bands; DELETE FROM venues_bands;", conn);
      cmd.ExecuteNonQuery();
      DB.CloseConnection();
    }
  }
}
