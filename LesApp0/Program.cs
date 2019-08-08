using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LesApp0
{
    class Program
    {
        static void Main()
        {
            // Join Unicode
            Console.OutputEncoding = Encoding.Unicode;

            // створюємо потік
            ThreadWorker<string> thread = new ThreadWorker<string>(TestMethod);

            // запускаємо на виконання
            thread.Start();

            // сигнал
            Console.WriteLine("Виконання базового потоку");

            // очікуємо завершення
            thread.Wait();

            // присвоюємо значення
            string s = thread.Result;

            Console.WriteLine(s);

            // delay
            Console.ReadKey(true);
        }

        /// <summary>
        /// Тестовий метод
        /// </summary>
        /// <returns></returns>
        private static string TestMethod()
        {
            // очікуємо 1 секунду
            Thread.Sleep(100);
            // виводимо результат
            return "\n\tСвою роботу з успіхом завершив TestMethod";
        }

    }
}
