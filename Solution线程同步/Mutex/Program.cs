using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MutexDemo
{
    class Program
    {
        /*
         使用Mutex可以设置超时的时间
             */
        static Mutex mutex = new Mutex();
        static void Main(string[] args)
        {
            Thread thread1 = new Thread(new ThreadStart(method1));            
            Thread thread2 = new Thread(new ThreadStart(method2));
            thread1.Start();
            thread2.Start();
            Console.ReadLine();
        }
        static void method1()
        {
            mutex.WaitOne();//进入后会阻塞其他线程
            Console.WriteLine("我是线程1");
            Thread.Sleep(3000);
            Console.WriteLine("我是线程1,线程1结束");
            mutex.ReleaseMutex();
        }
        static void method2()
        {
            mutex.WaitOne();
            Console.WriteLine("我是线程2");
            Thread.Sleep(3000);
            Console.WriteLine("我是线程2,线程2结束");            
            mutex.ReleaseMutex();
        }
    }
}
