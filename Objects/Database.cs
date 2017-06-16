using System.Data;
using System.Data.SqlClient;

namespace BandTracker.Objects
{
  public class DB
  {
    private static SqlConnection _conn;
    public static SqlConnection Connection()
    {
      SqlConnection conn = new SqlConnection(DBConfiguration.ConnectionString);
      return conn;
    }
    public static void CloseConnection()
    {
      if(_conn != null)
      {
        _conn.Close();
      }
    }
  }
}
