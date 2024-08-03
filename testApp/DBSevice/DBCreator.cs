using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace testApp.DBSevice
{
    internal class DBCreator
    {
        private DBConnection dbConnection = new DBConnection();
        private SqlDataAdapter adapter = null;

        public void CreateDB()
        {
            SqlConnection createConnection = new SqlConnection("Server=localhost;Integrated security=SSPI;database=master");
            string str = SQLCommands.CreateData.createDatabase;
            SqlCommand myCommand = new SqlCommand(str, createConnection);
            try
            {
                createConnection.Open();
                myCommand.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString(), "MyProgram", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                if (createConnection.State == ConnectionState.Open)
                {
                    createConnection.Close();
                    CreateTables();
                }
            }
        }

        private void CreateTables()
        {
            string command =
                SQLCommands.CreateData.createDepartments +
                SQLCommands.CreateData.createPositions +
                SQLCommands.CreateData.createEmployees;
            
           
            SqlCommand myCommand = new SqlCommand(command, dbConnection.getConnection());
            try
            {
                dbConnection.openConnection();
                myCommand.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString(), "MyProgram", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                CreateProcedure();
            }
        }

        private void CreateProcedure()
        {
            string command = SQLCommands.CreateData.createProcedure;


            SqlCommand myCommand = new SqlCommand(command, dbConnection.getConnection());
            try
            {
                dbConnection.openConnection();
                myCommand.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString(), "MyProgram", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                CreateData();
            }
        }

        private void CreateData()
        {
            string command =
               SQLCommands.CreateData.generatDepartments +
               SQLCommands.CreateData.generatePositions +
               SQLCommands.CreateData.generateEmployyes;


            SqlCommand myCommand = new SqlCommand(command, dbConnection.getConnection());
            try
            {
                dbConnection.openConnection();
                myCommand.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString(), "MyProgram", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                dbConnection.closeConnection();
                Properties.Settings.Default.isFirstRun = false;
                Properties.Settings.Default.Save();
            }
        }
    }
}
