using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient; 

namespace testApp
{
    internal class DBConnection
    {
        private SqlConnection sqlConnection = new SqlConnection(@"Data Source=localhost;Initial Catalog=MyDatabase; Integrated Security=True");

        public void openConnection()
        {
            try
            {
                if (sqlConnection.State == System.Data.ConnectionState.Closed) { sqlConnection.Open(); }
            } 
            catch {
                Console.WriteLine("Open connection failed");
            }
            
        }

        public void closeConnection()
        {
            try
            {
                if (sqlConnection.State == System.Data.ConnectionState.Open) { sqlConnection.Close(); }
            }
            catch
            {
                Console.WriteLine("Close connection failed");
            }
        }

        public SqlConnection getConnection() {
            return sqlConnection;
        }
    }
}
