using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testApp
{
    public struct EmployeeModel
    {
        public string name;
        public string lastName;
        public string email;
        public string birthday;
        public string department;
        public string position;

        public EmployeeModel(string name, string lastName, string email, string birthday, string department, string position)
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
