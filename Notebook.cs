using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.String;

namespace HomeWork_07
{
    /// <summary>
    /// Записная книжка.
    /// </summary>
    public struct Notebook
    {
        #region Field.

        /// <summary>
        /// Массив для хранения записей.
        /// </summary>
        private Note[] notes;

        /// <summary>
        /// Индекс последней записи массива записей.
        /// </summary>
        private int currentIndex;

        #endregion

        #region Properties


        /// <summary>
        /// Количество записей.
        /// </summary>
        public int Count => this.currentIndex;

        /// <summary>
        /// Получить запись по номеру записки.
        /// </summary>
        /// <param name="index">Номер записки.</param>
        /// <returns></returns>
        public Note this[int index] => this.notes[index];

        #endregion

        #region Constructors

        /// <summary>
        /// Создать записную книжку.
        /// </summary>
        /// <param name="notes">Записи.</param>
        public Notebook(params Note[] notes)
        {
            this.notes = notes;
            this.currentIndex = notes.Length;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Инициализация переменных для пустого конструктора.
        /// </summary>
        public void Initialization()
        {
            notes = new Note[1];
            currentIndex = 0;
        }

        /// <summary>
        /// Вывести на консоль все записи из записной книжки.
        /// </summary>
        public void PrintAllNotes()
        {
            if (currentIndex == 0)
            {
                Console.WriteLine("Записей нет.\n");
                return;
            }

            for (int i = 0; i < currentIndex; i++)
            {
                Console.WriteLine($"Запись №{i + 1}");
                Console.WriteLine(notes[i]);
            }
        }

        /// <summary>
        /// Добавить запись в записную книжку.
        /// </summary>
        /// <param name="note">Запись.</param>
        public void Add(Note note)
        {
            Resize(currentIndex >= notes.Length);
            notes[currentIndex] = note;
            currentIndex++;
        }

        /// <summary>
        /// Изменить размер массива.
        /// </summary>
        /// <param name="flag">Больше ли индекс, чем длина массива?</param>
        private void Resize(bool flag)
        {
            if (flag) 
                Array.Resize(ref notes, notes.Length * 2);
        }

        /// <summary>
        /// Удалить запись.
        /// </summary>
        public void Delete()
        {
            int index = GetIndex();
            if (index >= 0 && index < currentIndex)
            {
                Note[] tempNotes = new Note[currentIndex - 1];
                Array.Copy(notes, 0, tempNotes, 0, index);
                Array.Copy(notes, index + 1, tempNotes, index, currentIndex - index - 1);
                currentIndex--;
                Console.WriteLine($"\nУспешно удалена запись \"{notes[index].Topic}\".\n");
                notes = tempNotes;
            }
            else
            {
                Console.WriteLine("\nЗапись с таким номером не найдена!\n");
            }
        }

        /// <summary>
        /// Получить индекс записи.
        /// </summary>
        /// <returns>Индекс.</returns>
        private int GetIndex()
        {
            while (true)
            {
                Console.Write(">>> ");
                if (!int.TryParse(Console.ReadLine(), out int index)) continue;
                if (index < 1 || index == currentIndex) continue;
                return --index;
            }
        }

        /// <summary>
        /// Редактирование записи по номеру.
        /// </summary>
        public void Edit()
        {
            Console.WriteLine("Выберите номер записи.");
            int index = GetIndex();
            if (index >= 0 && index < currentIndex)
            {
                Console.WriteLine("Что вы хотите изменить?");
                Console.WriteLine("1) Название темы\n2) Описание темы");
                Console.WriteLine("3) Приоритет\n4) Статус");
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.D1:
                        ChangeTopic(ref notes[index]);
                        break;
                    case ConsoleKey.D2:
                        ChangeDescription(ref notes[index]);
                        break;
                    case ConsoleKey.D3:
                        ChangePriority(ref notes[index]);
                        break;
                    case ConsoleKey.D4:
                        ChangeIsCompleted(ref notes[index]);
                        break;
                    default:
                        Edit();
                        break;
                }
            }
            else
            {
                Console.WriteLine("\nЗапись с таким номером не найдена!\n");
            }
        }

        /// <summary>
        /// Изменить название темы.
        /// </summary>
        /// <param name="note">Запись.</param>
        private void ChangeTopic(ref Note note)
        {
            Console.WriteLine($"Текущее название записи: {note.Topic}.");
            Console.WriteLine("Введите свое название темы: ");
            note.Topic = Console.ReadLine();
            Console.WriteLine("Тема успешно изменена.");
        }

        /// <summary>
        /// Изменение описания записи.
        /// </summary>
        /// <param name="note">Запись.</param>
        private void ChangeDescription(ref Note note)
        {
            Console.WriteLine($"Текущее описание записи: {note.Description}.");
            Console.WriteLine("Введите свое описание темы: ");
            note.Description = Console.ReadLine();
            Console.WriteLine("Описание успешно изменено.");
        }

        /// <summary>
        /// Изменение приоритета записи.
        /// </summary>
        /// <param name="note">Запись.</param>
        private void ChangePriority(ref Note note)
        {
            Console.WriteLine($"Текущий приоритет записи: {note.Priority}.");
            note.Priority = GetPriority();
            Console.WriteLine("Приоритет записи успешно изменен.");
        }

        /// <summary>
        /// Изменение статуса записи.
        /// </summary>
        /// <param name="note">Запись.</param>
        private void ChangeIsCompleted(ref Note note)
        {
            note.IsCompleted = !note.IsCompleted;
            var result = note.IsCompleted ? "Завершено" : "Не завершено";
            Console.WriteLine($"Статус записи успешно изменен на {result}.");
        }

        /// <summary>
        /// Создать запись.
        /// </summary>
        public void CreateNote()
        {
            var date = DateTime.Now;
            Console.Write("Введите название темы: ");
            var topic = Console.ReadLine();
            Console.Write("Введите описание темы: ");
            var description = Console.ReadLine();
            Console.Write("Завершена ли задача? (д/н): ");
            var isCompleted = Console.ReadKey(true).Key == ConsoleKey.L;
            Console.WriteLine();
            var priority = GetPriority();

            this.Add(new Note(date, topic, description, isCompleted, priority));
            Console.WriteLine($"\nУспешно создана тема \"{topic}\".\n");
        }

        /// <summary>
        /// Получить приоритет задачи.
        /// </summary>
        /// <returns>Приоритет.</returns>
        private Priority GetPriority()
        {
            Console.WriteLine("Введите приоритет задачи:");
            Console.WriteLine("1) Маловажно\n2) Средняя важность\n3) Очень важно");
            Console.Write("Ваш вариант: ");
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.D1:
                    return Priority.Low;
                case ConsoleKey.D2:
                    return Priority.Medium;
                case ConsoleKey.D3:
                    return Priority.High;
                default:
                    Console.WriteLine("\nОшибка ввода данных");
                    return GetPriority();
            }
        }

        /// <summary>
        /// Генерация записей.
        /// </summary>
        /// <param name="count">Количество записей.</param>
        public void GenerateNotes(uint count)
        {
            Random rnd = new Random();

            for (int i = 0; i < count; i++)
            {
                DateTime date = new DateTime(2021, rnd.Next(1, 13), rnd.Next(1, 28));
                string topic = $"Topic#{i + 1}";
                string description = $"Description#{i + 1}";
                bool isCompleted = rnd.Next(10) == 0;
                Priority priority = (Priority)rnd.Next(3);
                this.Add(new Note(date, topic, description, isCompleted, priority));
            }
        }

        /// <summary>
        /// Сохранение записной книжки в файл.
        /// </summary>
        /// <param name="path">Пусть к файлу.</param>
        public void Save(string path)
        {
            Console.Clear();
            Console.WriteLine("Сохранение данных...");
            using (StreamWriter sw = new StreamWriter(path))
            {
                for (int i = 0; i < currentIndex; i++)
                {
                    notes[i].Save(sw);
                }
            }
            Console.WriteLine("Данных сохранены!");
        }

        /// <summary>
        /// Загрузить все данные из файла.
        /// </summary>
        /// <param name="path">Пусть к файлу.</param>
        public void Load(string path)
        {
            if (File.Exists(path))
            {
                using StreamReader sr = new StreamReader(path);
                string line;
                int countNotes = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    var values = line.Split(';');
                    this.Add(new Note(
                        DateTime.Parse(values[0]),
                        values[1],
                        values[2],
                        bool.Parse(values[3]),
                        (Priority)Enum.Parse(typeof(Priority), values[4])
                    ));
                    countNotes++;
                }
                
                Console.Clear();
                Console.WriteLine($"Записей по дате загружено: {countNotes}");
            }
            else
            {
                Console.WriteLine("Такого файла не существует!");
            }
        }

        /// <summary>
        /// Загрузить все данные из файла по дате.
        /// </summary>
        /// <param name="path">Пусть к файлу.</param>
        /// <param name="start">Начальная точка отрезка времени.</param>
        /// <param name="end">Конечная точка отрезка времени.</param>
        public void LoadByDate(string path, DateTime start, DateTime end)
        {
            if (File.Exists(path))
            {
                using StreamReader sr = new StreamReader(path);
                string line;
                int countNotes = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    var values = line.Split(';');

                    var date = DateTime.Parse(values[0]);

                    if (date.EarlierThan(start) || date.LaterThan(end)) continue;

                    var topic = values[1];
                    var description = values[2];
                    var isCompleted = bool.Parse(values[3]);
                    var priority = (Priority) Enum.Parse(typeof(Priority), values[4]);

                    this.Add(new Note(
                        date,
                        topic,
                        description,
                        isCompleted,
                        priority
                    ));
                    countNotes++;
                }

                Console.WriteLine($"Записей по дате загружено: {countNotes}");
            }
            else
            {
                Console.WriteLine("Такого файла не существует!");
            }
        }

        /// <summary>
        /// Сортировка по дате.
        /// </summary>
        public void SortByDate()
        {
            for (int i = 0; i < currentIndex - 1; i++)
            {
                for(int j = 1 + i; j < currentIndex; j++)
                {
                    if (DateTime.Compare(notes[i].Date, notes[j].Date) > 0)
                    {
                        Swap(ref notes[i], ref notes[j]);
                    }
                }
            }
            
            Console.Clear();
            Console.WriteLine("Записи отсортированы.");
        }

        /// <summary>
        /// Сортировка по теме.
        /// </summary>
        public void SortByTopic()
        {
            for (int i = 0; i < currentIndex - 1; i++)
            {
                for (int j = 1 + i; j < currentIndex; j++)
                {
                    if (Compare(notes[i].Topic, notes[i - 1].Topic, StringComparison.Ordinal) < 0)
                    {
                        Swap(ref notes[i], ref notes[j]);
                    }
                }
            }

            Console.Clear();
            Console.WriteLine("Записи отсортированы.");
        }

        /// <summary>
        /// Сортировка по приоритету.
        /// </summary>
        public void SortByPriority()
        {
            for (int i = 0; i < currentIndex - 1; i++)
            {
                for (int j = 1 + i; j < currentIndex; j++)
                {
                    if (notes[j].Priority > notes[i].Priority)
                        Swap(ref notes[j], ref notes[i]);
                }
            }

            Console.Clear();
            Console.WriteLine("Записи отсортированы.");
        }

        /// <summary>
        /// Сортировка по статусу.
        /// </summary>
        public void SortByStatus()
        {
            for (int i = 0; i < currentIndex - 1; i++)
            {
                for (int j = 1 + i; j < currentIndex; j++)
                {
                    if (!notes[j].IsCompleted && notes[i].IsCompleted)
                        Swap(ref notes[j], ref notes[i]);
                }
            }

            Console.Clear();
            Console.WriteLine("Записи отсортированы.");
        }

        /// <summary>
        /// Поменять местами записи.
        /// </summary>
        /// <param name="note1">Первая запись.</param>
        /// <param name="note2">Вторая запись.</param>
        private void Swap(ref Note note1, ref Note note2)
        {
            var temp = note1;
            note1 = note2;
            note2 = temp;
        }
        #endregion
    }
}
