using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Course9_Threading {
    class Program {

        static void Main(string[] args) {
            Program program = new Program();

            // Create and start a new thread
            Thread myThread = new Thread(new ThreadStart(program.myMethod));
            myThread.Start();

            var a = Console.ReadLine();
            myThread.Abort();
        }

        public void myMethod() {
            while (true) { 
                Console.WriteLine("Hello from a new thread!.");
                Thread.Sleep(1000);
            }
        }
    }
}
