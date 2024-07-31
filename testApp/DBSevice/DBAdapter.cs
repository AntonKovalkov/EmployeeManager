using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel;
using System.Reflection;

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
            adapter = new SqlDataAdapter(Commands.getAll, connection.getConnection());
            connection.closeConnection();
            return AdapterHandler(adapter);
        }

        public DataTable GetInfoFor(string id)
        {
            connection.openConnection();
            currentType = AdapterType.employeeWithId;
            adapter = new SqlDataAdapter(Commands.GetInfoFor(id), connection.getConnection());
            
            connection.closeConnection();
            return AdapterHandler(adapter);
        }

        public DataTable GetDepartments()
        {
            connection.openConnection();
            currentType = AdapterType.departments;
            adapter = new SqlDataAdapter(Commands.getAllDepartments, connection.getConnection());
           
            connection.closeConnection();
            return AdapterHandler(adapter);
        }

        public DataTable SearchBySurname(string surname)
        {
            connection.openConnection();
            currentType = AdapterType.procedure;
            adapter = new SqlDataAdapter(Commands.ProcedureCommand(surname), connection.getConnection());
            
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
            adapter = new SqlDataAdapter(Commands.employees, connection.getConnection());
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

        public EmployeeModel? GetEmployee(int index)
        {
            if (dataSet.Tables.Count > 0)
            {
                DataTable table = dataSet.Tables[currentType];
                if (table.Rows.Count > index)
                {
                    object[] items = table.Rows[index].ItemArray;
                    if (currentType != AdapterType.departments)
                    {
                        return new EmployeeModel(
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


namespace testApp
{
    struct Commands
    {
        struct CreateData
        {
            public static string createDepartments = "REATE TABLE Departments (\r\n\tid INT PRIMARY KEY IDENTITY,\r\n\tname NVARCHAR(50) NOT NULL UNIQUE\r\n);";
            public static string createPositions = "CREATE TABLE Positions (\r\n\tid INT PRIMARY KEY IDENTITY,\r\n\tname NVARCHAR(50) NOT NULL,\r\n\tdepartment_id INT REFERENCES Departments(id) ON DELETE CASCADE\r\n);";
            public static string createEmployees = "CREATE TABLE Employees (\r\n\tid INT PRIMARY KEY IDENTITY,\r\n\tfirst_name NVARCHAR(50) NOT NULL,\r\n\tlast_name NVARCHAR(50) NOT NULL,\r\n\temail NVARCHAR(100) NOT NULL UNIQUE,\r\n\tbirthday DATE NOT NULL,\r\n\tdepartment_id INT REFERENCES Departments(id) ON DELETE CASCADE,\r\n\tposition_id INT REFERENCES Positions(id)\r\n);";

            public static string generatDepartments = "INSERT INTO Departments\r\nVALUES\r\n\t('IT'),\r\n\t('Design'),\r\n\t('Production');";
            public static string generatePositions = "INSERT INTO Positions\r\nVALUES\r\n\t('Junior', 1),\r\n\t('Middle', 1),\r\n\t('Senior', 1),\r\n\t('Low level', 2),\r\n\t('High level', 2),\r\n\t('Inginear', 3)";
            public static string generateEmployyes = "INSERT INTO Employees\r\nVALUES\r\n\t('Antobn', 'Kovalov', 'avkovalk@ya.ru', '06.02.1988', 1, 2),\r\n\t('Arur', 'Fbnjfv', 'Fsf@ya.ru', '06.02.1999', 1, 3),\r\n\t('Olga', 'KOF', 'sds@ya.ru', '06.04.2000', 1, 1),\r\n\t('mASHA', ' MFNJND', 'asfag@ya.ru', '06.02.1988', 1, 1),\r\n\t('Kate', 'Dnkf', 'afag@ya.ru', '06.02.1988', 2, 2),\r\n\t('Petr', 'Nfjb', 'avakovalk@ya.ru', '06.02.1988', 2, 1),\r\n\t('Nasty', 'IIhfj', 'bjhvsf@ya.ru', '06.02.1988', 3, 1),\r\n\t('Sveta', 'KoFnjbvalov', 'vghajf@ya.ru', '06.02.1988', 3, 1),\r\n\t('Kuzma', 'BNkbf', 'bhjabf@ya.ru', '06.02.1988', 1, 2),\r\n\t('Poko', 'Bkf', 'bjhbasf@ya.ru', '06.02.1988', 1, 1),\r\n\t('Pablo', 'Fhihbf', 'vhaf@ya.ru', '06.02.1988', 1, 1),\r\n\t('Helena', 'Fbkf', 'vhgaf@ya.ru', '06.02.1988', 1, 1),\r\n\t('Aj', 'Fbhbfd', 'bjhaff@ya.ru', '06.02.1988', 2, 2),\r\n\t('AI', 'Bfff', 'bjhaf@ya.ru', '06.02.1988', 2, 1)";    
        }

        public static string getAll = "SELECT Employees.id, Employees.first_name, Employees.last_name, Employees.email, Employees.birthday, Departments.name, Positions.name, 'Delete' AS [Delete] FROM Employees\r\nJOIN Departments ON Departments.id = department_id\r\nJOIN Positions ON Positions.id = position_id";
        public static string getAllDepartments = "SELECT Departments.name, Positions.name FROM Departments\r\nJOIN Positions ON Positions.department_id = Departments.id";
        public static string employees = "SELECT * FROM Employees";

        public static String GetInfoFor(string id)
        {
            string command = $"SELECT Employees.id, Employees.first_name, Employees.last_name, Employees.email, Employees.birthday, Departments.name, Positions.name FROM Employees\r\nJOIN Departments ON Departments.id = department_id\r\nJOIN Positions ON Positions.id = position_id\r\nWHERE Employees.id = {id};";
            return command;
        }

        public static String ProcedureCommand(String lastName)
        {
            return $"EXEC SearchBySurname '{lastName}'";
        }

        public static String RemoveEmployee(string id)
        {
            return $"DELETE FROM Employees WHERE id = {id}";
        }
    }

    struct AdapterType
    {
        public static String employees = "Employees";
        public static String employeeWithId = "Employee";
        public static String departments = "Departments";
        public static String procedure = "Procedure";
    }
}