using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course6_Listview {

    public class Student : INotifyPropertyChanged {

        public Student() {

        }

        // Name of the student 3 chars
        private string m_name = "";
        public string Name
        {
            get { return m_name; }
            set
            {
                if (value.Length >= 2)
                {
                    Console.WriteLine(this.Name + " changed to: " + value);
                    m_name = value;

                    // Send update
                    OnPropertyChanged("Name");
                }
            }
        }

        // Student score 0-20
        private double m_score = 0;

        public event PropertyChangedEventHandler PropertyChanged;

        public double Score
        {
            get { return m_score; }
            set
            {
                if (value >= 0 && value <= 20)
                {
                    Console.WriteLine(this.Name + " score change from: " + this.Score + " to: " + value);
                    m_score = value;

                    // Send update
                    OnPropertyChanged("Score");
                }

            }
        }

        public Student(string name, double score)
        {
            this.m_name = name;
            this.m_score = score;
        }

        public void print()
        {
            System.Console.WriteLine(this.Name + " scored a fancy: " + this.Score);
        }

        private void OnPropertyChanged(string propertyName) {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
