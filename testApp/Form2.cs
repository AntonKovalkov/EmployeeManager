using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace testApp
{
    public partial class Form2 : Form
    {
        public EmployeeEditModel employee;

        

        public Form2()
        {
            InitializeComponent();
        }

        private void save_button_Click(object sender, EventArgs e)
        {
            
            Hide();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            nameTextBox.Text = employee.name;
            lastNameTextBox.Text = employee.lastName;
            emailTextBox.Text = employee.email;
            birthdayDateTimePicker.Text = employee.birthday;
            departmentComboBox.Text = employee.departamentId.ToString();
            positionComboBox.Text = employee.positionId.ToString();
        }

        private void birthdayDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            string date = e.ToString();
            Console.WriteLine(date);
            employee.birthday = birthdayDateTimePicker.Value.ToShortDateString();
        }
    }
}
