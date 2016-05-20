using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Course5
{
    public partial class Form1 : Form
    {
        private Color originalColor;
        private Timer timeOutTimer = new Timer();

        public Form1()
        {
            InitializeComponent();

            // Default color
            colorDialog_ButtonBackground.Color = Color.Red;
        }

        private void button_PressMe_MouseUp(object sender, MouseEventArgs e)
        {
            // Change background 
            this.originalColor = button_PressMe.BackColor;
            button_PressMe.BackColor = colorDialog_ButtonBackground.Color;

            // Set background back to original
            timeOutTimer.Tick += TimeOut_Tick;
            timeOutTimer.Interval = 2000;
            timeOutTimer.Start();
        }

        private void TimeOut_Tick(object sender, EventArgs e)
        {
            button_PressMe.BackColor = this.originalColor;
            timeOutTimer.Stop();
        }

        private void button_setColor_Click(object sender, EventArgs e)
        {
            DialogResult result = colorDialog_ButtonBackground.ShowDialog();
            if(result.Equals(DialogResult.OK)) {
                Console.WriteLine("Changed color");
            }
        }
    }
}
