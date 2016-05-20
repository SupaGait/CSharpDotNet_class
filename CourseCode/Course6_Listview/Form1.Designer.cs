namespace Course6_Listview
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.listView_student = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button_addStudent = new System.Windows.Forms.Button();
            this.textBox_studentName = new System.Windows.Forms.TextBox();
            this.textBox_studentGrade = new System.Windows.Forms.TextBox();
            this.Student = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.bindingSource_students = new System.Windows.Forms.BindingSource(this.components);
            this.button_Load = new System.Windows.Forms.Button();
            this.button_save = new System.Windows.Forms.Button();
            this.Student.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource_students)).BeginInit();
            this.SuspendLayout();
            // 
            // listView_student
            // 
            this.listView_student.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listView_student.Location = new System.Drawing.Point(12, 72);
            this.listView_student.Name = "listView_student";
            this.listView_student.Size = new System.Drawing.Size(990, 291);
            this.listView_student.TabIndex = 0;
            this.listView_student.UseCompatibleStateImageBehavior = false;
            this.listView_student.View = System.Windows.Forms.View.Details;
            this.listView_student.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView_student_ColumnClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 234;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Grade";
            this.columnHeader2.Width = 274;
            // 
            // button_addStudent
            // 
            this.button_addStudent.Location = new System.Drawing.Point(64, 114);
            this.button_addStudent.Name = "button_addStudent";
            this.button_addStudent.Size = new System.Drawing.Size(156, 60);
            this.button_addStudent.TabIndex = 1;
            this.button_addStudent.Text = "Add";
            this.button_addStudent.UseVisualStyleBackColor = true;
            this.button_addStudent.Click += new System.EventHandler(this.button_addStudent_Click);
            // 
            // textBox_studentName
            // 
            this.textBox_studentName.Location = new System.Drawing.Point(64, 26);
            this.textBox_studentName.Name = "textBox_studentName";
            this.textBox_studentName.Size = new System.Drawing.Size(256, 20);
            this.textBox_studentName.TabIndex = 2;
            // 
            // textBox_studentGrade
            // 
            this.textBox_studentGrade.Location = new System.Drawing.Point(63, 71);
            this.textBox_studentGrade.Name = "textBox_studentGrade";
            this.textBox_studentGrade.Size = new System.Drawing.Size(82, 20);
            this.textBox_studentGrade.TabIndex = 3;
            // 
            // Student
            // 
            this.Student.Controls.Add(this.label2);
            this.Student.Controls.Add(this.label1);
            this.Student.Controls.Add(this.textBox_studentGrade);
            this.Student.Controls.Add(this.textBox_studentName);
            this.Student.Controls.Add(this.button_addStudent);
            this.Student.Location = new System.Drawing.Point(12, 369);
            this.Student.Name = "Student";
            this.Student.Size = new System.Drawing.Size(410, 206);
            this.Student.TabIndex = 4;
            this.Student.TabStop = false;
            this.Student.Text = "Student";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Grade";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Name";
            // 
            // listBox1
            // 
            this.listBox1.DataSource = this.bindingSource_students;
            this.listBox1.DisplayMember = "Score";
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(564, 440);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(438, 134);
            this.listBox1.TabIndex = 5;
            this.listBox1.ValueMember = "Score";
            // 
            // bindingSource_students
            // 
            this.bindingSource_students.DataSource = typeof(Course6_Listview.Student);
            // 
            // button_Load
            // 
            this.button_Load.Location = new System.Drawing.Point(12, 12);
            this.button_Load.Name = "button_Load";
            this.button_Load.Size = new System.Drawing.Size(110, 54);
            this.button_Load.TabIndex = 6;
            this.button_Load.Text = "Load";
            this.button_Load.UseVisualStyleBackColor = true;
            this.button_Load.Click += new System.EventHandler(this.button_Load_Click);
            // 
            // button_save
            // 
            this.button_save.Location = new System.Drawing.Point(128, 12);
            this.button_save.Name = "button_save";
            this.button_save.Size = new System.Drawing.Size(110, 54);
            this.button_save.TabIndex = 7;
            this.button_save.Text = "Save";
            this.button_save.UseVisualStyleBackColor = true;
            this.button_save.Click += new System.EventHandler(this.button_save_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1014, 627);
            this.Controls.Add(this.button_save);
            this.Controls.Add(this.button_Load);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.Student);
            this.Controls.Add(this.listView_student);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Student.ResumeLayout(false);
            this.Student.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource_students)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView_student;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button button_addStudent;
        private System.Windows.Forms.TextBox textBox_studentName;
        private System.Windows.Forms.TextBox textBox_studentGrade;
        private System.Windows.Forms.GroupBox Student;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.BindingSource bindingSource_students;
        private System.Windows.Forms.Button button_Load;
        private System.Windows.Forms.Button button_save;
    }
}

