using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LesApp0
{
    /// <summary>
    /// Клас-обгортка для виконання в іншому потоці: value Method(){...}
    /// </summary>
    /// <typeparam name="TResult">Тип значення, яке повертатиметься</typeparam>
    class ThreadWorker<TResult>
    {
        /// <summary>
        /// Делегат який містить метод для виконання
        /// </summary>
        private readonly Func<TResult> func = null;
        /// <summary>
        /// Результат виконання метода
        /// </summary>
        private TResult result;
        /// <summary>
        /// Потік, відміннй від головного, в якому виконуватиметься метод
        /// </summary>
        private Thread thread;

        /// <summary>
        /// Конструктор
        /// </summary>
        public ThreadWorker(Func<TResult> func)
        {
            // лише присвоєння, що буде перезаписувати методи
            // і виконуватиметься лише один метод а не група
            this.func = func;

            // установка стартових параметрів
            IsCompleted = false;
            IsSuccess = false;
            // присвоюємо виключення про те що потік не почався
            this.Extention = new IsNotStarted();
        }

        /// <summary>
        /// Перевірка завершення
        /// </summary>
        public bool IsCompleted { get; private set; }
        /// <summary>
        /// Перевірка успіху виконання
        /// </summary>
        public bool IsSuccess { get; private set; }
        /// <summary>
        /// Виключення
        /// </summary>
        public Exception Extention { get; private set; }

        /// <summary>
        /// Результат
        /// </summary>
        public TResult Result
        {
            // результат можна запросити в декылькох випадках
            // 1. Потік ще не був запущений і нічого не виконано, при цьому треба вивести
            // виключення про те, що  потік не запущено
            // 2. Потік запущено, але його виконання ще не завершено, при цьому
            // можна потсавити потік який звернувся до цього потоку в очікування завершення
            // 3. Потік завершився, але з помилкою, в разі чого треба видати виключення
            // 4. Потік завершився з успіхом, отже можна видати результат
            get
            {
                // 1.
                if (IsCompleted == false &&                                 // не завершився
                    this.Extention.Message == new IsNotStarted().Message)   // перевірка чи було почато виконання
                {
                    throw this.Extention;
                }

                // 2.
                if (this.Extention != null &&                           // можлвио потік уже виконався
                    this.Extention.Message ==
                        new IsNotComplatedExtention().Message)          // потік був запущений
                {
                    //Console.WriteLine("Очікування запущене через ThreadWorker");    // для тестування
                    // очікуємо завершення виконання потоку
                    Wait();
                }

                // 3. перевыряємо статус успішоного виконання
                if (IsCompleted == true &&                      // завершився
                    IsSuccess == false)                         // немає успіху
                {
                    throw this.Extention;
                }

                // 4. на даному моменті має бути все успішно, 
                // а смисла ставити перевірки немає
                // отже повертаємо результат
                return result;
            }
        }

        /// <summary>
        /// Запуск делегата який містить метод для виконання
        /// </summary>
        public void Start()
        {
            // передаємо потоку делегат на виконання через лямда вираз
            // при цьому в середині задаємо присвоєння даних у разі успішного
            // виконання методу
            thread = new Thread(() =>
            {
                // необхідно ловити всі помилки які можуть виникнути
                // при виконанні самого методу і у випадку їх виникнення
                // ссилатися на IsNotSuccessExtention
                try
                {
                    this.result = this.func();

                    // якщо операція завершилася успішно, міняємо стан
                    IsCompleted = true;
                    IsSuccess = true;

                    // очищуємо виключення
                    this.Extention = null;
                }
                catch (Exception)   // відловнювання всіх помилок
                {
                    // опреація завершилася, але з помилкою, отже:
                    IsCompleted = true;
                    IsSuccess = false;

                    // присвоюємо виключення про те що потік завершився але з помилкою
                    this.Extention = new IsNotSuccessExtention();
                }
            });

            // Запуск виконання
            thread.Start();

            // присвоюємо виключення про те що потік не завершився, але ще виконується
            this.Extention = new IsNotComplatedExtention();
        }

        /// <summary>
        /// Блокування поток, доки виконується операція
        /// </summary>
        public void Wait()
        {
            // ставимо на очікування завершення потоку який виконується
            thread.Join();
        }
    }
    // Nested classes

    /// <summary>
    /// Виклчюення спричинене незавершенням виконання операції
    /// </summary>
    class IsNotComplatedExtention : Exception
    {
        // повідомлення, яке сигналізує про вид помилки
        public override string Message
            => "Ви не можете отримати результат, так як виконання операції ще не завершено!";

        /// <summary>
        /// Виклчюення спричинене незавершенням виконання операції
        /// </summary>
        public IsNotComplatedExtention() { }
    }
    /// <summary>
    /// Виключення спричинене виникненням помилки при виконанні операції
    /// </summary>
    class IsNotSuccessExtention : Exception
    {
        // повідомлення, яке сигналізує про вид помилки
        public override string Message
            => "Ви не можете отримати результат, так як операція завершилась з помилкою!";

        /// <summary>
        /// Виключення спричинене виникненням помилки при виконанні операції
        /// </summary>
        public IsNotSuccessExtention() { }
    }
    /// <summary>
    /// Виключення спричинене спробою отримати результат без його попереднього розрахунку
    /// </summary>
    class IsNotStarted : Exception
    {
        // повідомлення, яке сигналізує про вид помилки
        public override string Message
            => "Ви не можете отримати результат, так як виконання операції ще не почалося!";

        /// <summary>
        ///  Виключення спричинене спробою отримати результат без його попереднього розрахунку
        /// </summary>
        public IsNotStarted() { }
    }

}
