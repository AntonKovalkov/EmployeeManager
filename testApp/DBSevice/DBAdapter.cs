using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using testApp.DBSevice;

namespace testApp
{
    internal class DBAdapter
    {
        private DBConnection connection = new DBConnection();
        private DataSet dataSet = null;
        private SqlDataAdapter adapter = null;
        private String currentType = "";

        public DataTable GetAll()
        {
            connection.openConnection();
            currentType = AdapterType.employees;
            adapter = new SqlDataAdapter(SQLCommands.GetData.getAll, connection.getConnection());
            connection.closeConnection();
            return AdapterHandler(adapter);
        }

        public DataTable GetInfoFor(string id)
        {
            connection.openConnection();
            currentType = AdapterType.employeeWithId;
            adapter = new SqlDataAdapter(SQLCommands.GetData.GetInfoFor(id), connection.getConnection());
            
            connection.closeConnection();
            return AdapterHandler(adapter);
        }

        public DataTable GetDepartments()
        {
            connection.openConnection();
            currentType = AdapterType.departments;
            adapter = new SqlDataAdapter(SQLCommands.GetData.getAllDepartments, connection.getConnection());
           
            connection.closeConnection();
            return AdapterHandler(adapter);
        }

        public DataTable SearchBySurname(string surname)
        {
            connection.openConnection();
            currentType = AdapterType.procedure;
            adapter = new SqlDataAdapter(SQLCommands.GetData.ProcedureCommand(surname), connection.getConnection());
            
            connection.closeConnection();

            return AdapterHandler(adapter);
        }

        public void RemoveDataAt(int index)
        {
            //connection.openConnection();
            Console.WriteLine(dataSet.Tables[currentType].Rows[index].ItemArray[0]);
            dataSet.Tables[currentType].Rows[index].Delete();
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            builder.GetDeleteCommand();
            //adapter = new SqlDataAdapter(Commands.employees, connection.getConnection());
            var _ = AdapterHandler(adapter, true);
            connection.closeConnection();
        }

        public String GetInfoForDelete(int index)
        {
            connection.openConnection();
            adapter = new SqlDataAdapter(SQLCommands.GetData.employees, connection.getConnection());
            var _ = AdapterHandler(adapter);
            //connection.closeConnection();
            if (dataSet.Tables.Count > 0)
            {
                DataTable table = dataSet.Tables[currentType];
                if (table.Rows.Count > index)
                {
                    object[] items = table.Rows[index].ItemArray;
                    if (currentType != AdapterType.departments)
                    {
                        return $"{items[1]} {items[2]}";
                    }
                }
            }
            return "";
        }

        public AdapterModel? GetEmployee(int index)
        {
            if (dataSet.Tables.Count > 0)
            {
                DataTable table = dataSet.Tables[currentType];
                if (table.Rows.Count > index)
                {
                    object[] items = table.Rows[index].ItemArray;
                    if (currentType != AdapterType.departments)
                    {
                        return new AdapterModel(
                            items[1].ToString(), 
                            items[2].ToString(), 
                            items[3].ToString(), 
                            items[4].ToString(), 
                            items[5].ToString(), 
                            items[6].ToString()
                            );
                    }
                }
            }
            return null;
        }

        private DataTable AdapterHandler(SqlDataAdapter adapter, bool isDelete = false)
        {
            try
            {
                if (isDelete)
                {
                    var test = adapter.Update(dataSet, currentType);
                    Console.WriteLine(test);
                } else
                {
                    dataSet = new DataSet();
                    adapter.Fill(dataSet, currentType);
                } 
                if (dataSet.Tables.Count == 0)
                {
                    return null;
                } else
                {
                    return dataSet.Tables[currentType];
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}