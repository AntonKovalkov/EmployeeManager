using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testApp.DBSevice
{
    internal struct SQLCommands
    {
        internal struct CreateData
        {
            public static string createDatabase = "If(db_id(N'EmployeeManagement') IS NULL) CREATE DATABASE [EmployeeManagement]";
            public static string createDepartments = "CREATE TABLE Departments (\r\n\tid INT PRIMARY KEY IDENTITY,\r\n\tname NVARCHAR(50) NOT NULL UNIQUE\r\n);";
            public static string createPositions = "CREATE TABLE Positions (\r\n\tid INT PRIMARY KEY IDENTITY,\r\n\tname NVARCHAR(50) NOT NULL,\r\n\tdepartment_id INT REFERENCES Departments(id) ON DELETE CASCADE\r\n);";
            public static string createEmployees = "CREATE TABLE Employees (\r\n\tid INT PRIMARY KEY IDENTITY,\r\n\tfirst_name NVARCHAR(50) NOT NULL,\r\n\tlast_name NVARCHAR(50) NOT NULL,\r\n\temail NVARCHAR(100) NOT NULL UNIQUE,\r\n\tbirthday DATE NOT NULL,\r\n\tdepartment_id INT REFERENCES Departments(id) ON DELETE CASCADE,\r\n\tposition_id INT REFERENCES Positions(id)\r\n);";
            public static string createProcedure = "CREATE PROCEDURE SearchBySurname\r\n@last_name NVARCHAR(50)\r\nAS\r\nSELECT Employees.id, first_name, last_name, email, birthday, Departments.name, Positions.name From Employees\r\nJOIN Departments ON Departments.id = department_id\r\nJOIN Positions ON Positions.id = position_id\r\nWHERE last_name LIKE '%'+@last_name+'%'";

            public static string generatDepartments = "INSERT INTO Departments\r\nVALUES\r\n\t('IT'),\r\n\t('Design'),\r\n\t('Production');";
            public static string generatePositions = "INSERT INTO Positions\r\nVALUES\r\n\t('Junior', 1),\r\n\t('Middle', 1),\r\n\t('Senior', 1),\r\n\t('Low level', 2),\r\n\t('High level', 2),\r\n\t('Inginear', 3)";
            public static string generateEmployyes = "INSERT INTO Employees\r\nVALUES\r\n('Antobn', 'Kovalov', 'avkovalk@ya.ru', '06.02.1988', 1, 2),\r\n('Arur', 'Fbnjfv', 'Fsf@ya.ru', '06.02.1999', 1, 3),\r\n('Olga', 'KOF', 'sds@ya.ru', '06.04.2000', 1, 1),\r\n('mASHA', ' MFNJND', 'asfag@ya.ru', '06.02.1988', 1, 1),\r\n('Kate', 'Dnkf', 'afag@ya.ru', '06.02.1988', 2, 4),\r\n('Petr', 'Nfjb', 'avakovalk@ya.ru', '06.02.1988', 2, 5),\r\n('Nasty', 'IIhfj', 'bjhvsf@ya.ru', '06.02.1988', 3, 6),\r\n('Sveta', 'KoFnjbvalov', 'vghajf@ya.ru', '06.02.1988', 3, 6),\r\n('Kuzma', 'BNkbf', 'bhjabf@ya.ru', '06.02.1988', 1, 2),\r\n('Poko', 'Bkf', 'bjhbasf@ya.ru', '06.02.1988', 1, 1),\r\n('Pablo', 'Fhihbf', 'vhaf@ya.ru', '06.02.1988', 1, 3),\r\n('Helena', 'Fbkf', 'vhgaf@ya.ru', '06.02.1988', 1, 1),\r\n('Aj', 'Fbhbfd', 'bjhaff@ya.ru', '06.02.1988', 2, 5),\r\n('AI', 'Bfff', 'bjhaf@ya.ru', '06.02.1988', 2, 4)";
        }

        internal struct GetData
        {
            public static string getEmployees = "SELECT * FROM Employees";
            public static string getDepartaments = "SELECT * FROM Departments";
            public static string getPositions = "SELECT * FROM Positions";
            public static string getAll = "SELECT Employees.id, Employees.first_name, Employees.last_name, Employees.email, Employees.birthday, Departments.name, Positions.name, 'Delete' AS [Delete] FROM Employees\r\nJOIN Departments ON Departments.id = department_id\r\nJOIN Positions ON Positions.id = position_id";
            public static string getDepartmentsWithPositions = "SELECT Departments.name, Positions.name FROM Departments\r\nJOIN Positions ON Positions.department_id = Departments.id";

            public static String GetInfoFor(string id)
            {
                return $"SELECT Employees.id, Employees.first_name, Employees.last_name, Employees.email, Employees.birthday, Departments.name, Positions.name FROM Employees\r\nJOIN Departments ON Departments.id = department_id\r\nJOIN Positions ON Positions.id = position_id\r\nWHERE Employees.id = {id};"; ;
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
    }
}
