using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course4
{
    public class SomeClass
    {

    }
    class Program
    {
        static void Main(string[] args)
        {
            SomeClass[] SomeClasses = new SomeClass[10];

            foreach (var something in SomeClasses)
            {
                if (something == null)
                {
                    Console.WriteLine("I have something empty overhere.");
                }
            }
            // Wait for it..
            Console.WriteLine("... Press any key to exit ...");
            Console.ReadKey();

            throw (new System.OutOfMemoryException("WHelllo Exception"));
        }
    }
}
