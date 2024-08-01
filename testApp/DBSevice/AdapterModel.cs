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
        public static String employeeWithId = "Employee";
        public static String departments = "Departments";
        public static String procedure = "Procedure";
    }

    public struct AdapterModel
    {
        public string name;
        public string lastName;
        public string email;
        public string birthday;
        public string department;
        public string position;

        public AdapterModel(string name, string lastName, string email, string birthday, string department, string position)
        {
            this.name = name;
            this.lastName = lastName;
            this.email = email;
            this.birthday = birthday;
            this.department = department;
            this.position = position;
        }
    }
}
