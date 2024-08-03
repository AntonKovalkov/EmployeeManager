using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace testApp
{
    public partial class MainForm : Form
    {

        private DBAdapter adapter = null;
        private int? _selectedItem = null;
        private TableType? _tableType = null;
        private bool isFirstRun = true;

        private TableType? TableType
        {
            get => _tableType;
            set
            {
                _tableType = value;
                bool condition = (value == testApp.TableType.employees) && (SelectedItem != null);
                delete_button.Enabled = condition;
                change_button.Enabled = condition;
            }
        }

        private int? SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                bool condition = (TableType == testApp.TableType.employees) && (value != null);
                delete_button.Enabled = condition;
                change_button.Enabled = condition;
            }
        }

        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            adapter = new DBAdapter();
            delete_button.Enabled = false;
            change_button.Enabled = false;
        }

        //Load all employees
        private void get_all_Click(object sender, EventArgs e)
        {
            LoadData();
            SelectedItem = null;
        }

        private void LoadData()
        {
            dataGridView1.DataSource = adapter.GetAll();
            AdjustColumnOrder();
            TableType = testApp.TableType.employees;
        }

        //Get employee sith id
        private void get_info_Click(object sender, EventArgs e)
        {
            String id = employee_id_text.Text;
            if (id == null || id == "") { return; }
            dataGridView1.DataSource = adapter.GetInfoFor(id);
            AdjustColumnOrder();
            TableType = testApp.TableType.employees;
            SelectedItem = null;
        }

        //Get all departaments
        private void get_all_departaments_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = adapter.GetDepartments();
            TableType = testApp.TableType.departments;
            SelectedItem = null;
        }

        //Get employee from procedure
        private void finde_employee_Click(object sender, EventArgs e)
        {
            String last_name = last_name_text.Text;
            dataGridView1.DataSource = adapter.SearchBySurname(last_name);
            AdjustColumnOrder();
            TableType = testApp.TableType.employees;
            SelectedItem = null;
        }

        //DataGrid configuration
        private void AdjustColumnOrder()
        {
            if (dataGridView1.Columns.Count == 0) { return; }
            dataGridView1.Columns["id"].DisplayIndex = 0;
            dataGridView1.Columns["first_name"].DisplayIndex = 1;
            dataGridView1.Columns["last_name"].DisplayIndex = 2;
            dataGridView1.Columns["email"].DisplayIndex = 3;
            dataGridView1.Columns["birthday"].DisplayIndex = 4;
            dataGridView1.Columns["name"].DisplayIndex = 5;
            dataGridView1.Columns["name1"].DisplayIndex = 6;

            dataGridView1.Columns["id"].HeaderText = "ID";
            dataGridView1.Columns["first_name"].HeaderText = "First Name";
            dataGridView1.Columns["last_name"].HeaderText = "Last Name";
            dataGridView1.Columns["email"].HeaderText = "eMail";
            dataGridView1.Columns["birthday"].HeaderText = "Birthday";
            dataGridView1.Columns["name"].HeaderText = "Department";
            dataGridView1.Columns["name1"].HeaderText = "Position";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectedItem = e.RowIndex;
        }

        private void delete_button_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Delete!!");
            if (SelectedItem.HasValue)
            {
                string employee = adapter.GetInfoForDelete(SelectedItem.Value);
                if (MessageBox.Show($"Вы уверены что хотите удалить сотрудника {employee}?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    adapter.RemoveDataAt(SelectedItem.Value);
                    dataGridView1.Rows.RemoveAt(SelectedItem.Value);
                    SelectedItem = null;
                }
            }
            
        }

        //Edit secletd employee
        private void change_button_Click(object sender, EventArgs e)
        {
            if (SelectedItem.HasValue)
            {
                EditForm form = new EditForm();
                form.adapter = adapter;
                form.selectedItem = SelectedItem.Value;
                form.ShowDialog();
            }
        }

        //Reload data when form is activated
        private void MainForm_Activated(object sender, EventArgs e)
        {
            if (isFirstRun)
            {
                isFirstRun = false;
            } else
            {
                SelectedItem = null;
                LoadData();
            }
        }
    }

    enum TableType
    {
        employees, 
        departments
    }
}
