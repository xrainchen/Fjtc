using System;
using cms.rponey.cc.Utilty;

namespace fjtc.UnitTest
{
    static class Program
    {
        static void Main()
        {
            Console.WriteLine("无种子");
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(Tools.GetRandom("0123456789", 2));
            }
            Console.WriteLine("有种子");
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(Tools.GetRandom("0123456789", 2, i));
            }
            Console.WriteLine("线程唯一");
            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine(System.Threading.Thread.GetDomainID());
            }
            Console.WriteLine("毫秒");
            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine(DateTime.Now.Millisecond);
            }
            Console.Read();
        }
    }
}
