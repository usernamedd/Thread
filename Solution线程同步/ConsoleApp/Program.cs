using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            new Thread(new ThreadStart(ThreadMethod)).Start();
            Console.ReadLine();
        }

        static void ThreadMethod()
        {
            Console.WriteLine("托管线程ID:{0}",Thread.CurrentThread.ManagedThreadId);
        }
    }
}
