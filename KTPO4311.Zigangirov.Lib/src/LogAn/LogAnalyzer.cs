using System;
using System.Collections.Generic;
using System.Text;

namespace KTPO4311.Zigangirov.Lib.src.LogAn
{
    /// <summary>Анализатор лог. файлов</summary>
    public class LogAnalyzer
    {
        ////<summary> Результат предыдущей проверки имени файла </summary>
        public bool WasLastFileNameValid { get; set; }

        /// <summary> Проверка правильности имя файла </summary>
        public bool IsValidLogFileName(string fileName)
        {
            WasLastFileNameValid = false;
            if(string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentException("имя файла должно быть задано");
            }

            if (!fileName.EndsWith(".Zigangirov", StringComparison.CurrentCultureIgnoreCase))
            {
                return false;
            }
            WasLastFileNameValid = true;
            return true;
        }
    }
}
