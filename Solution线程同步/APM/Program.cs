using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace APM
{
    /*
     体现异步的类中有两个方法Begin***和End***；
     End***方法什么意义，什么作用？
         */
    class Program
    {    
        static void Main(string[] args)
        {
            var webReq = WebRequest.Create("https://github.com/");
          IAsyncResult result=  webReq.BeginGetResponse(new AsyncCallback((ar) => {
              webReq.EndGetResponse(ar);
              Console.WriteLine("回调函数所在的线程:{0}", Thread.CurrentThread.ManagedThreadId);
              Thread.Sleep(9000);
              Console.WriteLine("回调函数9秒后");//3
          }), null);
            Console.WriteLine("主线程:{0}",Thread.CurrentThread.ManagedThreadId);//1
            Console.WriteLine("同步操作是否完成：{0}", result.CompletedSynchronously);//1
            Console.WriteLine("异步操作是否完成：{0}", result.IsCompleted);//1
            result.AsyncWaitHandle.WaitOne();//这里等的不是回调函数占据的线程
            Console.WriteLine("同步操作是否完成：{0}", result.CompletedSynchronously);//2
            Console.WriteLine("异步操作是否完成：{0}", result.IsCompleted);//2
            Console.ReadLine();
        }
    }
}
