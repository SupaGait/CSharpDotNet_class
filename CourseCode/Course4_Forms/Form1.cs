using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Course4_Forms
{
    public partial class Form1 : Form
    {
        TextBox tb1;
        public Form1()
        {
            InitializeComponent();

            tb1 = new TextBox();
            tb1.Text = "A dynamicly generated textbox";
            tb1.Size = new Size(200, 100);
            Controls.Add(tb1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Whoooo");
        }
    }
}
