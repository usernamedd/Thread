using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 异步读取文件
{
    class Program
    {
        const string filename = "1.txt";
        static FileStream fs;
        static void Main(string[] args)
        {
            Console.WriteLine("主线程ID是：{0}", Thread.CurrentThread.ManagedThreadId);
             fs= new FileStream(filename, FileMode.Open, FileAccess.Read);
            byte[] array = new byte[100];
            //BeginRead是不是用的主线程，现在有一个疑问：启动IO请求的是主线程吗？通过查看底层代码，看起来是这样的
            fs.BeginRead(array, 0, 30, new AsyncCallback(callback), null);
            Console.ReadLine();
        }

        static void callback(IAsyncResult result) {
            //这里的线程和主线程不是一个线程
            Console.WriteLine("异步读取的回调方法，线程ID是：{0}",Thread.CurrentThread.ManagedThreadId);
            int bytesRead = fs.EndRead(result);
            fs.Close();
            Console.WriteLine("读取到了{0}字节", bytesRead);
        }
    }
}
