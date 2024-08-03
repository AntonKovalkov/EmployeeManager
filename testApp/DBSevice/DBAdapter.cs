using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using testApp.DBSevice;
using System.Reflection;

namespace testApp
{
    public class DBAdapter
    {
        private DBConnection connection = new DBConnection();
        private DataSet dataSet = new DataSet();
        private SqlDataAdapter adapter = null;

        //Get initital data
        public DBAdapter()
        {
            getDepartments();
            getEmployees();
            getPositions();
        }

        //Employees table
        private void getEmployees()
        {
            connection.openConnection();
            adapter = new SqlDataAdapter(SQLCommands.GetData.getEmployees, connection.getConnection());
            connection.closeConnection();
            var _ =  AdapterHandler(adapter, AdapterType.employees);
        }

        //Departaments table
        private void getDepartments()
        {
            connection.openConnection();
            adapter = new SqlDataAdapter(SQLCommands.GetData.getDepartaments, connection.getConnection());
            connection.closeConnection();
            var _ = AdapterHandler(adapter, AdapterType.departments);
        }

        //Positions table
        private void getPositions()
        {
            connection.openConnection();
            adapter = new SqlDataAdapter(SQLCommands.GetData.getPositions, connection.getConnection());
            connection.closeConnection();
            var _ = AdapterHandler(adapter, AdapterType.positions);
        }

        //Get joined employees table
        public DataTable GetAll()
        {
            connection.openConnection();
            adapter = new SqlDataAdapter(SQLCommands.GetData.getAll, connection.getConnection());
            connection.closeConnection();
            return AdapterHandler(adapter, AdapterType.joinedEmployees);
        }

        //Finde employee by id
        public DataTable GetInfoFor(string id)
        {
            connection.openConnection();
            adapter = new SqlDataAdapter(SQLCommands.GetData.GetInfoFor(id), connection.getConnection());
            connection.closeConnection();
            return AdapterHandler(adapter, AdapterType.joinedEmployeeWithId);
        }

        //Finde employee from DataSet by selected index
        public DataTable GetInfoFor(int index)
        {
            DataTable employeeTable = dataSet.Tables[AdapterType.employees];
            string id = employeeTable.Rows[index].ItemArray[0].ToString();
            connection.openConnection();
            adapter = new SqlDataAdapter(SQLCommands.GetData.GetInfoFor(id), connection.getConnection());
            connection.closeConnection();
            return AdapterHandler(adapter, AdapterType.joinedEmployeeWithId);
        }

        //Get joined departments table
        public DataTable GetDepartments()
        {
            connection.openConnection();
            adapter = new SqlDataAdapter(SQLCommands.GetData.getDepartmentsWithPositions, connection.getConnection());
            connection.closeConnection();
            return AdapterHandler(adapter, AdapterType.joinedDepartments);
        }

        //Exec store procedure
        public DataTable SearchBySurname(string surname)
        {
            connection.openConnection();
            adapter = new SqlDataAdapter(SQLCommands.GetData.ProcedureCommand(surname), connection.getConnection());
            connection.closeConnection();
            return AdapterHandler(adapter, AdapterType.procedure);
        }

        //Remove employee
        public void RemoveDataAt(int index)
        {
            connection.openConnection();
            string type = AdapterType.employees;
            adapter = new SqlDataAdapter(SQLCommands.GetData.getEmployees, connection.getConnection());
            dataSet.Tables[type].Rows[index].Delete();
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            builder.GetDeleteCommand();
            var _ = AdapterHandler(adapter, type, true);
            connection.closeConnection();
        }

        //Get employee name and surname bedore remove
        public String GetInfoForDelete(int index)
        {
            if (dataSet.Tables.Count > 0)
            {
                DataTable table = dataSet.Tables[AdapterType.employees];
                if (table.Rows.Count > index)
                {
                    object[] items = table.Rows[index].ItemArray;
                    return $"{items[1]} {items[2]}";
                }
            }
            return "";
        }

        //Stored departments table
        public DataTable GetDepartmentsTable()
        {
            return dataSet.Tables[AdapterType.departments];
        }

        //Stored positions table
        public DataTable GetPositionsTable()
        {
            return dataSet.Tables[AdapterType.positions];
        }

        //Update employee data
        public void UpdateEmployee(EmployeeEditModel model, string depId, string posId, int currentIndex) 
        {
            dataSet.Tables[AdapterType.employees].Rows[currentIndex]["first_name"] = model.name;
            dataSet.Tables[AdapterType.employees].Rows[currentIndex]["last_name"] = model.lastName;
            dataSet.Tables[AdapterType.employees].Rows[currentIndex]["email"] = model.email;
            dataSet.Tables[AdapterType.employees].Rows[currentIndex]["birthday"] = model.birthday;
            dataSet.Tables[AdapterType.employees].Rows[currentIndex]["department_id"] = depId;
            dataSet.Tables[AdapterType.employees].Rows[currentIndex]["position_id"] = posId;

            connection.openConnection();
            adapter = new SqlDataAdapter(SQLCommands.GetData.getEmployees, connection.getConnection());
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            builder.GetUpdateCommand();
            var _ = AdapterHandler(adapter, AdapterType.employees, true);
            connection.closeConnection();
        }

        //Checking the SQladapter for exceptions
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