using System;
using System.IO;

namespace HomeWork_07
{
    /// <summary>
    /// Запись.
    /// </summary>
    public struct Note
    {
        #region Field

        /// <summary>
        /// Дата записи.
        /// </summary>
        private DateTime date;

        /// <summary>
        /// Тема.
        /// </summary>
        private string topic;

        /// <summary>
        /// Описание темы.
        /// </summary>
        private string description;

        /// <summary>
        /// Сделано?
        /// </summary>
        private bool isCompleted;

        /// <summary>
        /// Степень важности.
        /// </summary>
        private Priority priority;

        #endregion

        #region Properties

        /// <summary>
        /// Дата записи.
        /// </summary>
        public DateTime Date => this.date;

        /// <summary>
        /// Тема.
        /// </summary>
        public string Topic
        {
            get => this.topic;
            set => this.topic = value;
        }

        /// <summary>
        /// Описание темы.
        /// </summary>
        public string Description
        {
            get => this.description;
            set => this.description = value;
        }

        /// <summary>
        /// Сделано?
        /// </summary>
        public bool IsCompleted
        {
            get => this.isCompleted;
            set => this.isCompleted = value;
        }

        /// <summary>
        /// Степень важности.
        /// </summary>
        public Priority Priority
        {
            get => this.priority;
            set => this.priority = value;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Создать запись.
        /// </summary>
        /// <param name="date">Дата записи.</param>
        /// <param name="topic">Тема.</param>
        /// <param name="description">Описание темы.</param>
        /// <param name="isCompleted">Сделано?</param>
        /// <param name="priority">Степень важности.</param>
        public Note(DateTime date, string topic, string description,
            bool isCompleted, Priority priority)
        {
            this.date = date;
            this.topic = topic;
            this.description = description;
            this.isCompleted = isCompleted;
            this.priority = priority;
        }

        /// <summary>
        /// Создать запись.
        /// </summary>
        /// <param name="topic">Тема.</param>
        /// <param name="description">Описание темы.</param>
        /// <param name="priority">Степень важности.</param>
        public Note(string topic, string description, Priority priority)
            : this(DateTime.Now, topic, description, false, priority) { }

        /// <summary>
        /// Создать запись.
        /// </summary>
        /// <param name="date">Дата записи.</param>
        /// <param name="topic">Тема.</param>
        /// <param name="description">Описание темы.</param>
        /// <param name="priority">Степень важности.</param>
        public Note(DateTime date, string topic, string description, Priority priority)
            : this(date, topic, description, false, priority) { }

        /// <summary>
        /// Создать запись.
        /// </summary>
        /// <param name="topic">Тема.</param>
        /// <param name="description">Описание темы.</param>
        public Note(string topic, string description)
            : this(DateTime.Now, topic, description, false, Priority.Low) { }

        #endregion

        #region Methods

        /// <summary>
        /// Сохранение записи.
        /// </summary>
        /// <param name="sw">Поток записи.</param>
        public void Save(StreamWriter sw)
        {
            sw.WriteLine($"{date:dd.MM.yyyy};{topic};{description};{isCompleted};{priority}");
        }

        #endregion

        #region Override Methods

        /// <summary>
        /// Информация о записи.
        /// </summary>
        /// <returns>Информация.</returns>
        public override string ToString()
        {
            string result = isCompleted ? "Завершено" : "Не завершено";
            return $"Дата создания: {date:dd.MM.yyyy}\nТема: {topic}\nОписание: {description}\n" +
                $"Приоритет: {priority}\nСтатус: {result}\n";
        }

        #endregion
    }
}
