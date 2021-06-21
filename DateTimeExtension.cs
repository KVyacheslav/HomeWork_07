using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_07
{
    public static class DateTimeExtension
    {
        /// <summary>
        /// Раньше чем...
        /// </summary>
        /// <param name="start">Начальная точка времени.</param>
        /// <param name="date">Выбранная точка.</param>
        /// <returns>Раньше или нет.</returns>
        public static bool EarlierThan(this DateTime start, DateTime date)
        {
            if (start.Year > date.Year)
                return false;
            if (start.Month > date.Month)
                return false;
            if (start.Day > date.Day)
                return false;

            return true;
        }

        /// <summary>
        /// Позже чем...
        /// </summary>
        /// <param name="end">Конечная точка времени.</param>
        /// <param name="date">Выбранная точка времени.</param>
        /// <returns>Позже или нет.</returns>
        public static bool LaterThan(this DateTime end, DateTime date)
        {
            if (end.Year < date.Year)
                return false;
            if (end.Month < date.Month)
                return false;
            if (end.Day < date.Day)
                return false;

            return true;
        }
    }
}
