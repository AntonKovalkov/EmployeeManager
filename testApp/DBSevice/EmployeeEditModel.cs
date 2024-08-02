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
        public string departmentName;
        public string positionName;

        public EmployeeEditModel(string name, 
            string lastName, 
            string email, 
            string birthday,
            string departmentName,
            string positionName)
        {
            this.name = name;
            this.lastName = lastName;
            this.email = email;
            this.birthday = birthday;
            this.departmentName = departmentName;
            this.positionName = positionName;
        }
    }
}

public static class StringExtension
{
    public static bool IsEmpty(this string str)
    {
        return (str == "") || (str == null);
    }
}