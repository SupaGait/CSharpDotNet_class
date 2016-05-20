using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course3
{
    class Program
    {
        static void Main(string[] args)
        {
            int studentCount = 0;
            while (studentCount < 1)
            {
                try {
                    Console.Write("How many students: ");
                    string studentCountString = Console.ReadLine();
                    studentCount = int.Parse(studentCountString);
                }
                catch (Exception) {
                    Console.WriteLine("Invalid input, try again.");
                }
            }

            Student[] students = new Student[studentCount];


            foreach (var emptyStudent in students)
            {
                if(emptyStudent == null) {

                    Console.WriteLine("I have an emptystudent here.");
                }
            }

            // Fill some students.
            Console.WriteLine("--> Leave the name empty to stop filling students. <--");
            int studentFillNr = 0;
            while(studentFillNr < studentCount)
            { 
                Console.Write("Student name: ");
                string name = Console.ReadLine();

                // Stop on empty name
                if (name.Length == 0)
                    break;

                double score = 0;
                bool gotInput = false;
                while (!gotInput)
                {
                    try {
                        Console.Write("Score: ");
                        string scoreString = Console.ReadLine(); 
                        score = double.Parse(scoreString);
                        gotInput = true;
                    }
                    catch (Exception) {
                        Console.WriteLine("Invalid input, try again.");
                    }
                }
                

                // Fill the student
                students[studentFillNr] = new Student(name, score);
         
                studentFillNr++;
            }

            // Print all filled students
            for (int studentNr = 0; studentNr < studentFillNr; studentNr++)
            {
                students[studentNr].print();
            }

            // Wait for it..
            Console.WriteLine("... Press any key to exit ...");
            Console.ReadKey();
        }
    }
}
