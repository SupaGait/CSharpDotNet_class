using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Course6_Listview
{
    public partial class Form1 : Form
    {
        List<Student> students;

        public Form1()
        {
            InitializeComponent();
            students = new List<Student>();

            // Databinding
            //listView_student.DataBindings
        }

        private void button_addStudent_Click(object sender, EventArgs e)
        {
            // Get the values
            string studentName = textBox_studentName.Text;
            string studentGrade = textBox_studentGrade.Text;

            // Convert grade
            try
            {
                double grade = double.Parse(studentGrade);

                // Create a new student, add it to the student list
                Student newStudent = new Student(studentName, grade);
                students.Add(newStudent);

                // Add the student to the list
                ListViewItem items = new ListViewItem(studentName);
                items.SubItems.Add(studentGrade);

                listView_student.Items.Add(items);
            }
            catch (Exception)
            {
                string error = "Invalid input, try again.";
                MessageBox.Show(error);
                Console.WriteLine(error);
            }
        }

        private void listView_student_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Change the student sorting order and update.
            if(listView_student.Sorting == SortOrder.Ascending) {
                listView_student.Sorting = SortOrder.Descending;
            }
            else {
                listView_student.Sorting = SortOrder.Ascending;
            }

            listView_student.Sort();
        }

        private void button_Load_Click(object sender, EventArgs e) {
            // De-Serialize the students array
            XmlSerializer xs = new XmlSerializer(typeof(List<Student>));
            using (StreamReader reader = new StreamReader("students.xml")) {
                students = xs.Deserialize(reader) as List<Student>;
            }

            // Refresh the listView
            listView_student.Items.Clear();
            foreach (Student student in students) {
                ListViewItem item = new ListViewItem(student.Name);
                item.SubItems.Add(student.Score.ToString());
                listView_student.Items.Add(item);
            }
        }

        private void button_save_Click(object sender, EventArgs e) {
            // Serialize the students array
            XmlSerializer xs = new XmlSerializer(typeof(List<Student>));
            using(StreamWriter writer = new StreamWriter("students.xml")) {
                xs.Serialize(writer, students);
            }
        }
    }
}
