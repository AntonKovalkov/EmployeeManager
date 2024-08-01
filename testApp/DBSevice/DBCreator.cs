using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using testApp.DBSevice;

namespace testApp
{
    internal class DBCreator
    {
        private string connectioPath = "";
        public void CreateDB()
        {
            SqlConnection connection = new SqlConnection("Server=localhost;Integrated security=SSPI;database=master");
            string str = SQLCommands.CreateData.createDatabase;
            SqlCommand myCommand = new SqlCommand(str, connection);
            try
            {
                connection.Open();
                myCommand.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString(), "MyProgram", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
    }
}
