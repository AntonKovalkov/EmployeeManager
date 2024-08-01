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
        private DataSet dataSet = new DataSet();
        private SqlDataAdapter adapter = null;

        public DBAdapter()
        {
            getDepartments();
            getEmployees();
            getPositions();
        }

        private void getEmployees()
        {
            connection.openConnection();
            adapter = new SqlDataAdapter(SQLCommands.GetData.getEmployees, connection.getConnection());
            connection.closeConnection();
            var _ =  AdapterHandler(adapter, AdapterType.employees);
        }

        private void getDepartments()
        {
            connection.openConnection();
            adapter = new SqlDataAdapter(SQLCommands.GetData.getDepartaments, connection.getConnection());
            connection.closeConnection();
            var _ = AdapterHandler(adapter, AdapterType.departments);
        }

        private void getPositions()
        {
            connection.openConnection();
            adapter = new SqlDataAdapter(SQLCommands.GetData.getPositions, connection.getConnection());
            connection.closeConnection();
            var _ = AdapterHandler(adapter, AdapterType.positions);
        }

        public DataTable GetAll()
        {
            connection.openConnection();
            adapter = new SqlDataAdapter(SQLCommands.GetData.getAll, connection.getConnection());
            connection.closeConnection();
            return AdapterHandler(adapter, AdapterType.joinedEmployees);
        }

        public DataTable GetInfoFor(string id)
        {
            connection.openConnection();
            adapter = new SqlDataAdapter(SQLCommands.GetData.GetInfoFor(id), connection.getConnection());
            connection.closeConnection();
            return AdapterHandler(adapter, AdapterType.joinedEmployeeWithId);
        }

        public DataTable GetDepartments()
        {
            connection.openConnection();
            adapter = new SqlDataAdapter(SQLCommands.GetData.getDepartmentsWithPositions, connection.getConnection());
            connection.closeConnection();
            return AdapterHandler(adapter, AdapterType.joinedDepartments);
        }

        public DataTable SearchBySurname(string surname)
        {
            connection.openConnection();
            adapter = new SqlDataAdapter(SQLCommands.GetData.ProcedureCommand(surname), connection.getConnection());
            connection.closeConnection();
            return AdapterHandler(adapter, AdapterType.procedure);
        }

        public void RemoveDataAt(int index)
        {
            //connection.openConnection();
            Console.WriteLine(dataSet.Tables[AdapterType.employees].Rows[index].ItemArray[0]);
            dataSet.Tables[AdapterType.employees].Rows[index].Delete();
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            builder.GetDeleteCommand();
            var _ = AdapterHandler(adapter, AdapterType.employees, true);
            connection.closeConnection();
        }

        public String GetInfoForDelete(int index)
        {
            connection.openConnection();
            string type = AdapterType.joinedEmployees;
            adapter = new SqlDataAdapter(SQLCommands.GetData.getEmployees, connection.getConnection());
            var _ = AdapterHandler(adapter, type);
            if (dataSet.Tables.Count > 0)
            {
                DataTable table = dataSet.Tables[type];
                if (table.Rows.Count > index)
                {
                    object[] items = table.Rows[index].ItemArray;
                    if (type != AdapterType.joinedDepartments)
                    {
                        return $"{items[1]} {items[2]}";
                    }
                }
            }
            return "";
        }

        public EmployeeEditModel? GetEmployee(int index)
        {
            if (dataSet.Tables.Count > 0)
            {
                DataTable emloyeeTable = dataSet.Tables[AdapterType.employees];
                DataTable departamentsTable = dataSet.Tables[AdapterType.departments];
                DataTable positionsTable = dataSet.Tables[AdapterType.positions];
                if (emloyeeTable.Rows.Count > index)
                {
                    object[] employeeItems = emloyeeTable.Rows[index].ItemArray;
                    Dictionary<int, string> departamentsItems = new Dictionary<int, string>();
                    foreach (DataRow dep in departamentsTable.Rows) {
                        string idRecord = dep.ItemArray[0].ToString();
                        string nameRecord = dep.ItemArray[1].ToString();
                        int id = 0;
                        int.TryParse(idRecord, out id);
                        departamentsItems[id] = nameRecord;
                    }
                    Dictionary<int, string[]> positionItems = new Dictionary<int, string[]>();
                    foreach(DataRow pos in positionsTable.Rows)
                    {
                        string idRecord = pos.ItemArray[0].ToString();
                        string[] dataRecord = { pos.ItemArray[1].ToString(), pos.ItemArray[2].ToString() };
                        int id = 0;
                        int.TryParse(idRecord, out id);
                        positionItems[id] = dataRecord;
                    }

                    if (employeeItems.Length >= 7)
                    {
                        int departamentId = 0;
                        int.TryParse(employeeItems[5].ToString(), out departamentId);
                        int positionId = 0;
                        int.TryParse(employeeItems[6].ToString(), out positionId);
                        return new EmployeeEditModel(
                            employeeItems[1].ToString(), 
                            employeeItems[2].ToString(), 
                            employeeItems[3].ToString(), 
                            employeeItems[4].ToString(), 
                            departamentId, 
                            positionId,
                            departamentsItems,
                            positionItems
                            );
                    }
                }
            }
            return null;
        }

        private DataTable AdapterHandler(SqlDataAdapter adapter, string type, bool isDelete = false)
        {
            try
            {
                if (isDelete)
                {
                    var _ = adapter.Update(dataSet, type);
                } else
                {
                    dataSet.Tables[type]?.Clear();
                    adapter.Fill(dataSet, type);
                } 
                if (dataSet.Tables.Count == 0)
                {
                    return null;
                } else
                {
                    return dataSet.Tables[type];
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