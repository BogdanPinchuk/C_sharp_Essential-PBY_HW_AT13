using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesApp0
{
    /// <summary>
    /// Клас-обертка
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    class ThreadWorker<TResult>
    {
        /// <summary>
        /// делегат
        /// </summary>
        private Func<TResult> func = null;
        /// <summary>
        /// Результат
        /// </summary>
        private TResult result;

        /// <summary>
        /// Конструктор
        /// </summary>
        public ThreadWorker(Func<TResult> func)
        {
            this.func = func;
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
        public TResult Result { get; }

        /// <summary>
        /// Запуск делегата
        /// </summary>
        public void Start()
        {

        }
        /// <summary>
        /// Блокування поток, доки виконується операція
        /// </summary>
        public void Wait()
        {

        }
    }
}
