//#define Task基本概念
//#define Task等待阻塞wait
//#define Task返回值
#define async_await
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskDemo
{
#if Task基本概念
    class Program
    {
        static void Main(string[] args)
        {
            Task.Run(() =>{
                Console.WriteLine("是否后台线程:{0}", Thread.CurrentThread.IsBackground);//true
                Console.WriteLine("是否线程池线程:{0}", Thread.CurrentThread.IsThreadPoolThread);//true
                Console.WriteLine("线程ID:{0}", Thread.CurrentThread.ManagedThreadId);
            });
            Console.WriteLine("主线程ID:{0}", Thread.CurrentThread.ManagedThreadId);
            Console.ReadLine();
        }
    }
#endif
#if Task等待阻塞wait
    class Program
    {
        static void Main(string[] args)
        {
            Task task = Task.Run(() => {
                Thread.Sleep(100);
                Console.WriteLine("我是任务中的线程");               
                Thread.Sleep(2000);
                Console.WriteLine("我是任务中的线程,要结束");
            });
            //task.Wait();//无线等待
            //task.Wait(-1);//无线等待
            //task.Wait(1000);//等待1000ms后，不再等待，直接去执行
            Console.WriteLine("主线程ID:{0}", Thread.CurrentThread.ManagedThreadId);
            Console.ReadLine();
        }
    }
#endif
#if Task返回值
    class Program
    {
        static void Main(string[] args)
        {
            Task<string> task = Task<string>.Run(() =>
            {
                Console.WriteLine("Task中的线程");
                Thread.Sleep(2000);
                return "fdsafds";
            });
            Console.WriteLine(task.Result);//在获取结果时会等待任务中的线程结束
            Console.WriteLine("主线程ID:{0}", Thread.CurrentThread.ManagedThreadId);
            Console.ReadLine();
        }
    }
#endif
#if async_await
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("-------主线程启动-------");//1
            Task<int> task = GetStrLengthAsync();
            Console.WriteLine("主线程继续执行");
            Console.WriteLine("Task返回的值" + task.Result);//等待阻塞
            Console.WriteLine("-------主线程结束-------");
            Console.Read();
        }

        static async Task<int> GetStrLengthAsync()
        {
            Console.WriteLine("GetStrLengthAsync方法开始执行");//2
            //此处返回的<string>中的字符串类型，而不是Task<string>
            //await修饰的方法需要返回值是Task<T>类型
            string str = await GetString();
            Console.WriteLine("GetStrLengthAsync方法执行结束");
            return str.Length;
        }

        static Task<string> GetString()
        {
            Console.WriteLine("GetString方法开始执行");//3
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            return Task<string>.Run(() =>
            {
                Console.WriteLine("任务中的线程：{0}",Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(2000);
                return "GetString的返回值";
            });
        }
    }
#endif
}
