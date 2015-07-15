using System;
using System.Data;
using System.Data.SqlClient;

namespace CommandClassLibrary.Data
{
	/// <summary>
	/// 通用数据库连接类
	/// </summary>
	public class clsDataAccess
	{
    private SqlConnection _conn = null;

    private static string _connectionString = "";

    public static string ConnectionString
    {
      get
      {
        return _connectionString;
      }

      set
      {
        _connectionString = value;
      }
    }

		public clsDataAccess(string ConnectionStr)
		{
      _connectionString = ConnectionStr;
      _conn = new SqlConnection(_connectionString);
		}

    public DataSet Select(string sql)
    {
      DataSet ds = new DataSet();
      SqlDataAdapter da = new SqlDataAdapter(sql,_conn);

      try
      {
        da.Fill(ds);
        return ds;
      }
      catch(Exception error)
      {
        throw error;
      }
    }

    public int Execute(string sql)
    {
      int r = 0;
      SqlCommand cmd = new SqlCommand(sql,_conn);

      try
      {
        _conn.Open();
        r = cmd.ExecuteNonQuery();
        return r;
      }
      catch(Exception error)
      {
        return -1;
      }
      finally
      {
        if(_conn.State==ConnectionState.Open)
          _conn.Close();
      }
    }
  }
}
