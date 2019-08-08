using System;
using System.Collections.Generic;
using System.Diagnostics;
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

            #region Тестування із явно вказаним очікуванням в головному потоці
#if false
            // створюємо потік
            ThreadWorker<string> thread = new ThreadWorker<string>(TestMethod);

            // запускаємо на виконання
            thread.Start();

            // сигнал
            Console.WriteLine("Виконання базового потоку");

            // очікуємо завершення
            thread.Wait();
            //Console.WriteLine("Очікування запущено через Main");

            // присвоюємо значення
            string s = thread.Result;

            Console.WriteLine(s);
#endif 
            #endregion

            #region Тестування без явно вказаного очікування в головному потоці
#if true
            // створюємо потік
            ThreadWorker<string> thread = new ThreadWorker<string>(TestMethod);
            
            // запускаємо на виконання
            thread.Start();

            // сигнал
            Console.WriteLine("Виконання базового потоку");

            // присвоюємо значення
            string s = thread.Result;

            Console.WriteLine(s);
#endif 
            #endregion

            #region Тестування на випадок спроба вивести результат без запуску потоку
#if false
            // створюємо потік
            ThreadWorker<string> thread = new ThreadWorker<string>(TestMethod);
            
            // сигнал
            Console.WriteLine("Виконання базового потоку");

            // присвоюємо значення
            string s = thread.Result;

            Console.WriteLine(s);
#endif 
            #endregion

            #region Тестування коли програма виконається із помилкою
#if false
            // створюємо потік
            ThreadWorker<string> thread = new ThreadWorker<string>(TestMethod2);

            // запускаємо на виконання
            thread.Start();

            // сигнал
            Console.WriteLine("Виконання базового потоку");

            // присвоюємо значення (має ще спрацювати внутрішнє очікування результату)
            string s = thread.Result;

            Console.WriteLine(s);
#endif 
            #endregion

            // delay
            Console.ReadKey(true);
        }

        /// <summary>
        /// Тестовий метод із затримкою
        /// </summary>
        /// <returns></returns>
        private static string TestMethod()
        {
            // очікуємо 1 секунду
            Thread.Sleep(1000);
            // виводимо результат
            return "\n\tСвою роботу з успіхом завершив TestMethod";
        }

        /// <summary>
        /// Тестовий метод 2 із помилкою виконання
        /// </summary>
        /// <returns></returns>
        private static string TestMethod2()
        {
            // створюємо помилку ділення на 0 для цілих чисел
            int forError = 5;
            forError /= 0;
            // виводимо результат
            return "\n\tСвою роботу з успіхом завершив TestMethod2";
        }

    }
}
