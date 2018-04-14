using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Data.SqlClient;

namespace Core.Data
{
    public class DBUtil
    {
        private SqlConnection sqlConnection;
        public string ConnectionString;

        public DBUtil()
        {
            sqlConnection = new SqlConnection();

            ConnectionString = "Data Source=(local)\\sqlserver; Initial Catalog=coinmarketcap; Integrated Security=True; Connection Timeout=300;";
            sqlConnection.ConnectionString = ConnectionString;
        }

        public DataTable GetData(string sql, Array coll)
        {
            SqlCommand cmd = new SqlCommand(sql, sqlConnection);
            if (coll != null && coll.Length > 0)
                cmd.Parameters.AddRange(coll);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }

            da.Fill(dt);
            sqlConnection.Close();

            return dt;
        }

        public int InsertUpdate(string sql, Array coll)
        {
            int result = 0;
            SqlCommand cmd = new SqlCommand(sql, sqlConnection);
            if (coll != null && coll.Length > 0)
                cmd.Parameters.AddRange(coll);

            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }

            result = cmd.ExecuteNonQuery();
            sqlConnection.Close();

            return result;
        }
    }
}
