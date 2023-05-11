using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Sql;
using System.IO;

namespace Exmpl1
{
    internal class DB
    {
        SqlConnection con = new SqlConnection(@"Data Source=PC209-5\SQLEXPRESS;Initial Catalog=platnaya_poliklinika;Integrated Security=True");

        public void OpenConnection()
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }
        }
        public void CloseConnection()
        {
            if (con.State == System.Data.ConnectionState.Open) 
            {
                con.Close();
            }
        }
        public SqlConnection GetConnection()
        {
            return con;
        }
    }
}
