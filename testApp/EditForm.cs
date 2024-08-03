using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace testApp
{
    public partial class EditForm : Form
    {
        public DBAdapter adapter;
        public int selectedItem;
        private DataTable employeeTable;
        private DataTable departmentsTable;
        private DataTable positionsTable;
        private EmployeeEditModel employee; 
        private string currentDepId = "";
        private string currentPosId = "";

        public EditForm()
        {
            InitializeComponent();
        }

        private void save_button_Click(object sender, EventArgs e)
        {
            employee.name = nameTextBox.Text;
            employee.lastName = lastNameTextBox.Text;
            employee.email = emailTextBox.Text;
            if (employee.name.IsEmpty() || employee.lastName.IsEmpty() || employee.email.IsEmpty() || currentDepId.IsEmpty() || currentPosId.IsEmpty())
            {
                Hide();
            } else
            {
                adapter.UpdateEmployee(employee, currentDepId, currentPosId, selectedItem);
                Hide();
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            LoadTables();
            CreateModel();
            ConfigViews();
        }

        private void LoadTables()
        {
            employeeTable = adapter.GetInfoFor(selectedItem);
            departmentsTable = adapter.GetDepartmentsTable();
            positionsTable = adapter.GetPositionsTable();
        }

        private void CreateModel()
        {
            object[] items = employeeTable.Rows[0].ItemArray;
            string name = items[1].ToString();
            string lastName = items[2].ToString();
            string email = items[3].ToString();
            string birthday = items[4].ToString();
            string department= items[5].ToString();
            string position = items[6].ToString();
            employee = new EmployeeEditModel(name, lastName, email, birthday, department, position);
        }

        private void ConfigViews()
        {
            Console.WriteLine("Config!!!");
            nameTextBox.Text = employee.name;
            lastNameTextBox.Text = employee.lastName;
            emailTextBox.Text = employee.email;
            birthdayDateTimePicker.Text = employee.birthday;

            ConfigDepComboBox();
            ConfigPosComboBox();
        }

        private void ConfigDepComboBox()
        {
            List<string> departments = new List<string>();
            foreach (DataRow dep in departmentsTable.Rows)
            {
                string element = dep.ItemArray[1].ToString();
                if (element == employee.departmentName)
                {
                    currentDepId = dep.ItemArray[0].ToString();
                    departments.Insert(0, element);
                } else
                {
                    departments.Add(element);
                }
                
            }
            departmentComboBox.DataSource = departments;
        }

        private void ConfigPosComboBox()
        {
            List<string> positions = new List<string>();
            
            foreach (DataRow pos in positionsTable.Rows)
            {
                string element = pos.ItemArray[1].ToString();
                if (pos.ItemArray[2].ToString() == currentDepId)
                {
                    if (element == employee.positionName)
                    {
                        currentPosId = pos.ItemArray[0].ToString();
                        positions.Insert(0, element);
                    } else
                    {
                        positions.Add(element);
                    }
                }
            }
            positionComboBox.DataSource = positions;
        }

        private void birthdayDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            string date = e.ToString();
            Console.WriteLine(date);
            employee.birthday = birthdayDateTimePicker.Value.ToShortDateString();
        }

        private void departmentComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var str = departmentComboBox.SelectedItem.ToString();
            employee.departmentName = str;
            GetIds();
            ConfigPosComboBox();
            Console.WriteLine(employee.departmentName);
            Console.WriteLine(employee.positionName);
        }


        private void positionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var str = positionComboBox.SelectedItem.ToString();
            employee.positionName = str;
            GetIds();
        }

        private void GetIds()
        {
            foreach (DataRow dep in departmentsTable.Rows)
            {
                if (dep.ItemArray[1].ToString() == employee.departmentName)
                {
                    currentDepId = dep.ItemArray[0].ToString();
                    break;
                }
                
            }

            foreach (DataRow pos in positionsTable.Rows)
            {
                if (pos.ItemArray[1].ToString() == employee.positionName)
                {
                    currentPosId = pos.ItemArray[0].ToString();
                    break;
                }
            }
        }
    }
}
