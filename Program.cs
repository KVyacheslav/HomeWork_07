using System;
using System.Text.RegularExpressions;
using System.Threading.Channels;

namespace HomeWork_07
{
    /// <summary>
    /// Класс, описыващающий действие программы.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Записная книжка.
        /// </summary>
        private static Notebook notebook;

        /// <summary>
        /// Точка входа в программу.
        /// </summary>
        /// <param name="args">Параметры запуска.</param>
        public static void Main(string[] args)
        {
            Initialize();
            Start();
        }

        /// <summary>
        /// Инициализировать данные.
        /// </summary>
        private static void Initialize()
        {
            notebook = new Notebook();
            notebook.Initialization();
        }

        /// <summary>
        /// Начать работу программы. 
        /// </summary>
        private static void Start()
        {
            Console.WriteLine("Выберите один из следующих вариантов.");
            Console.WriteLine("1) Работа с записями.\n2) Сохранение записей.");
            Console.WriteLine("3) Загрузка записей.\n4) Загрузка записей по диапазону дат.");
            Console.WriteLine("5) Упорядочить записи по выбранному полю.\n0) Выход из программы.");
            int userInput = GetUserInput();
            switch (userInput)
            {
                case 0:
                    Console.WriteLine("До скорых встреч!");
                    return;
                case 1:
                    WorkWithNotes();
                    break;
                case 2:
                    SaveNotes();
                    Start();
                    break;
                case 3:
                    LoadNotes();
                    Start();
                    break;
                case 4:
                    LoadNotesByDate();
                    Start();
                    break;
                case 5:
                    SortBySelectedField();
                    Start();
                    break;
                default:
                    Start();
                    break;
            }
        }

        /// <summary>
        /// Работа с записями.
        /// </summary>
        private static void WorkWithNotes()
        {
            Console.WriteLine("\n1) Показать список всех записей.\n2) Создание записей.");
            Console.WriteLine("3) Генерация записей. \n4) Удаление записей.\n5) Редактирование записей.");
            Console.WriteLine("Или любую другую цифру для выхода в главное меню.");
            int userInput = GetUserInput();
            switch (userInput)
            {
                case 1:
                    notebook.PrintAllNotes();
                    break;
                case 2:
                    notebook.CreateNote();
                    break;
                case 3:
                    notebook.GenerateNotes(GetCountNotesForGenerate());
                    break;
                case 4:
                    notebook.Delete();
                    break;
                case 5:
                    notebook.Edit();
                    break;
                default:
                    Console.WriteLine("Возврат из меню \"Работа с записями.\"\n");
                    break;
            }

            Start();
        }

        /// <summary>
        /// Получить количество записей.
        /// </summary>
        /// <returns>Количество записей.</returns>
        private static uint GetCountNotesForGenerate()
        {
            while (true)
            {
                Console.WriteLine("Сколько записей сгенерировать?");
                Console.Write(">>> ");
                if (!uint.TryParse(Console.ReadLine(), out uint count)) continue;
                if (count == 0) continue;
                return count;
            }
        }

        /// <summary>
        /// Сохранение записей в файл
        /// </summary>
        private static void SaveNotes()
        {
            notebook.Save(GetPathToFile());
        }

        /// <summary>
        /// Загрузить записи из файла.
        /// </summary>
        private static void LoadNotes()
        {
            if (notebook.Count > 0)
                Initialize();
            notebook.Load(GetPathToFile());
        }

        /// <summary>
        /// Загрузить записи по датам.
        /// </summary>
        private static void LoadNotesByDate()
        {
            Console.WriteLine("Введите начальную точку даты.");
            DateTime start = GetUserDate();
            Console.WriteLine("Введите конечную точку даты.");
            DateTime end = GetUserDate();
            CheckDates(ref start, ref end);
            if (notebook.Count > 0) 
                Initialize();
            notebook.LoadByDate(GetPathToFile(), start, end);
        }

        /// <summary>
        /// Проверка дат на порядок.
        /// </summary>
        /// <param name="start">Начальная точка отрезка даты.</param>
        /// <param name="end">Конечная точка отрезка даты</param>
        private static void CheckDates(ref DateTime start, ref DateTime end)
        {
            if (start.EarlierThan(end)) return;
            Console.WriteLine("Даты не по порядку.");
            var temp = start;
            start = end;
            end = temp;
        }

        /// <summary>
        /// Получить дату от пользователя.
        /// </summary>
        /// <returns>Дата.</returns>
        private static DateTime GetUserDate()
        {
            Regex reg = new Regex(@"\d{1,2}\.\d{1,2}\.\d{4}");
            while (true)
            {
                Console.WriteLine("Формат даты: 01.12.2013 или 7.4.2005");
                if (!DateTime.TryParse(Console.ReadLine(), out var date)) continue;
                return date;
            }
        }

        /// <summary>
        /// Отсортировать записи по полю.
        /// </summary>
        private static void SortBySelectedField()
        {
            if (notebook.Count < 2)
            {
                Console.WriteLine("Список записей пуст или существует только 1 запись.");
                return;
            }

            Console.Clear();
            Console.WriteLine("По какому полю будем сортировать записи?");
            Console.WriteLine("1) По дате.\n2) По теме.\n3) По приоритету.\n4) По статусу.");
            int userInput = GetUserInput();
            switch (userInput)
            {
                case 1:
                    notebook.SortByDate();
                    Start();
                    break;
                case 2:
                    notebook.SortByTopic();
                    Start();
                    break;
                case 3:
                    notebook.SortByPriority();
                    Start();
                    break;
                case 4:
                    notebook.SortByStatus();
                    Start();
                    break;
                default:
                    SortBySelectedField();
                    break;
            }
        }

        /// <summary>
        /// Получить число от пользоваетля.
        /// </summary>
        /// <returns>Число.</returns>
        private static int GetUserInput()
        {
            while (true)
            {
                Console.Write(">>> ");
                if (int.TryParse(Console.ReadLine(), out int userInput))
                    return userInput;
            }
        }

        /// <summary>
        /// Поулучить путь к файлу.
        /// </summary>
        /// <returns>Путь к файлу.</returns>
        private static string GetPathToFile()
        {
            Console.Write("Введите путь файла или название без расширения: ");
            return Console.ReadLine() + ".csv";
        }
    }
}
