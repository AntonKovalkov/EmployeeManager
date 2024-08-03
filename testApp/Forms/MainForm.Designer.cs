namespace testApp
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.get_all = new System.Windows.Forms.Button();
            this.get_info = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.employee_id_text = new System.Windows.Forms.TextBox();
            this.get_all_departaments = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.last_name_text = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.delete_button = new System.Windows.Forms.Button();
            this.change_button = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // get_all
            // 
            this.get_all.Location = new System.Drawing.Point(12, 12);
            this.get_all.Name = "get_all";
            this.get_all.Size = new System.Drawing.Size(75, 23);
            this.get_all.TabIndex = 0;
            this.get_all.Text = "Get All";
            this.get_all.UseVisualStyleBackColor = true;
            this.get_all.Click += new System.EventHandler(this.get_all_Click);
            // 
            // get_info
            // 
            this.get_info.Location = new System.Drawing.Point(3, 45);
            this.get_info.Name = "get_info";
            this.get_info.Size = new System.Drawing.Size(117, 24);
            this.get_info.TabIndex = 1;
            this.get_info.Text = "Get Info";
            this.get_info.UseVisualStyleBackColor = true;
            this.get_info.Click += new System.EventHandler(this.get_info_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.employee_id_text);
            this.panel1.Controls.Add(this.get_info);
            this.panel1.Location = new System.Drawing.Point(12, 41);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(124, 72);
            this.panel1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label1.Location = new System.Drawing.Point(3, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Eneter employee ID";
            // 
            // employee_id_text
            // 
            this.employee_id_text.Location = new System.Drawing.Point(3, 3);
            this.employee_id_text.Name = "employee_id_text";
            this.employee_id_text.Size = new System.Drawing.Size(117, 20);
            this.employee_id_text.TabIndex = 2;
            // 
            // get_all_departaments
            // 
            this.get_all_departaments.Location = new System.Drawing.Point(11, 130);
            this.get_all_departaments.Name = "get_all_departaments";
            this.get_all_departaments.Size = new System.Drawing.Size(126, 23);
            this.get_all_departaments.TabIndex = 3;
            this.get_all_departaments.Text = "Get All Daprtaments";
            this.get_all_departaments.UseVisualStyleBackColor = true;
            this.get_all_departaments.Click += new System.EventHandler(this.get_all_departaments_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.last_name_text);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Location = new System.Drawing.Point(12, 170);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(124, 72);
            this.panel2.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label2.Location = new System.Drawing.Point(3, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Eneter last name";
            // 
            // last_name_text
            // 
            this.last_name_text.Location = new System.Drawing.Point(3, 3);
            this.last_name_text.Name = "last_name_text";
            this.last_name_text.Size = new System.Drawing.Size(117, 20);
            this.last_name_text.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 45);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(117, 24);
            this.button1.TabIndex = 1;
            this.button1.Text = "Get Info";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.finde_employee_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(168, 12);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(435, 230);
            this.dataGridView1.TabIndex = 5;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // delete_button
            // 
            this.delete_button.BackColor = System.Drawing.Color.Red;
            this.delete_button.ForeColor = System.Drawing.Color.Snow;
            this.delete_button.Location = new System.Drawing.Point(528, 251);
            this.delete_button.Name = "delete_button";
            this.delete_button.Size = new System.Drawing.Size(75, 23);
            this.delete_button.TabIndex = 6;
            this.delete_button.Text = "Delete";
            this.delete_button.UseVisualStyleBackColor = false;
            this.delete_button.Click += new System.EventHandler(this.delete_button_Click);
            // 
            // change_button
            // 
            this.change_button.Location = new System.Drawing.Point(447, 251);
            this.change_button.Name = "change_button";
            this.change_button.Size = new System.Drawing.Size(75, 23);
            this.change_button.TabIndex = 7;
            this.change_button.Text = "Change";
            this.change_button.UseVisualStyleBackColor = true;
            this.change_button.Click += new System.EventHandler(this.change_button_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(615, 286);
            this.Controls.Add(this.change_button);
            this.Controls.Add(this.delete_button);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.get_all_departaments);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.get_all);
            this.Name = "MainForm";
            this.Text = "Employees";
            this.Activated += new System.EventHandler(this.MainForm_Activated);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button get_all;
        private System.Windows.Forms.Button get_info;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox employee_id_text;
        private System.Windows.Forms.Button get_all_departaments;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox last_name_text;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button delete_button;
        private System.Windows.Forms.Button change_button;
    }
}

