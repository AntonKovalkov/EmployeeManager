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
    public partial class Form1 : Form
    {

        private DBAdapter adapter = null;
        private int? _selectedItem = null;
        private TableType? tableType = null;

        private int? SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                delete_button.Enabled = !(value == null);
                change_button.Enabled = !(value == null);
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            adapter = new DBAdapter();
            delete_button.Enabled = false;
            change_button.Enabled = false;
        }

        private void get_all_Click(object sender, EventArgs e)
        {   
            dataGridView1.DataSource = adapter.GetAll();

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                DataGridViewLinkCell linkCell = new DataGridViewLinkCell();
                dataGridView1[7, i] = linkCell;
            }
            AdjustColumnOrder();
            tableType = TableType.employees;
        }

        private void get_info_Click(object sender, EventArgs e)
        {
            String id = employee_id_text.Text;
            if (id == null || id == "") { return; }
            dataGridView1.DataSource = adapter.GetInfoFor(id);
            AdjustColumnOrder();
            tableType = TableType.employees;
        }

        private void get_all_departaments_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = adapter.GetDepartments();
            tableType = TableType.departments;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String last_name = last_name_text.Text;
            dataGridView1.DataSource = adapter.SearchBySurname(last_name);
            AdjustColumnOrder();
            tableType = TableType.employees;
        }


        private void AdjustColumnOrder()
        {
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

            //MessageBox.Show($"Row index =  {e.RowIndex}", "Test", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                }
            }
            
        }

        private void change_button_Click(object sender, EventArgs e)
        {
            if (SelectedItem.HasValue)
            {
                EmployeeModel? emloyee = adapter.GetEmployee(SelectedItem.Value);
                if (emloyee.HasValue)
                {
                    Form2 form = new Form2();
                    form.employee = emloyee.Value;
                    form.ShowDialog();
                }
            }
            
        }
    }

    enum TableType
    {
        employees, 
        departments
    }
}
