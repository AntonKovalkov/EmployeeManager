using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testApp
{
    public struct AdapterType
    {
        public static String employees = "Employees";
        public static String positions = "Positions";
        public static String departments = "Departments";
        public static String joinedEmployees = "JoinedEmployees";
        public static String joinedEmployeeWithId = "JoinedEmployee";
        public static String joinedDepartments = "JoinedDepartments";
        public static String procedure = "Procedure";
    }

    public struct EmployeeEditModel
    {
        public string name;
        public string lastName;
        public string email;
        public string birthday;
        public int departamentId;
        public int positionId;
        public Dictionary<int, string> departments;
        public Dictionary<int, string []> positions;

        public EmployeeEditModel(string name, 
            string lastName, 
            string email, 
            string birthday,
            int departamentId,
            int positionId,
            Dictionary<int, string> departments, 
            Dictionary<int, string []> positions)
        {
            this.name = name;
            this.lastName = lastName;
            this.email = email;
            this.birthday = birthday;
            this.departamentId = departamentId;
            this.positionId = positionId;
            this.departments = departments;
            this.positions = positions;
        }
    }
}
