using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 锁
{
    /*
     使用锁机制时要求有一个对象可以锁上，这里的例子中是lockobj；
     运行过程中，线程1锁上lockobj，会阻塞其他线程，其他线程在lock(lockobj)时只能等待线程1不持有lockobj的状态
         */
    class Program
    {
        static object lockobj = new object();//用于当作锁的对象
        static int global = 900;//全局对象
        static void Main(string[] args)
        {
            Console.WriteLine("全局对象：{0}", global);
            new Thread(new ThreadStart(ThreadMethod1)).Start();
            lock (lockobj)
            {
                Console.WriteLine("主线程上锁，开始修改全局对象：{0}", global);
                global = 200;
                Thread.Sleep(4000);
                Console.WriteLine("主线程解锁，结束修改全局对象：{0}", global);
            }
            Console.ReadLine();
        }

        static void ThreadMethod1()
        {
            lock (lockobj)
            {
                Console.WriteLine("线程1上锁，开始修改全局对象：{0}", global);
                global = 200;
                Thread.Sleep(4000);
                Console.WriteLine("线程1解锁，结束修改全局对象：{0}", global);
            }
        }
    }
}
