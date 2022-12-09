using System;
using System.Configuration;

namespace KTPO4311.Zigangirov.Lib.src.LogAn
{
    /// <summary>Менеджер расширений файлов</summary>
    public class FileExtensionManager : IExtensionManager
    {
        private string extension = ConfigurationManager.AppSettings["extension"];

        /// <summary>Проверка правильности расширения</summary>
        public bool IsValid(string fileName)
        {
            string ext = ConfigurationManager.AppSettings["extension"];
            if(extension == fileName.Substring(fileName.LastIndexOf(".")))
            {
                return true;
            }
            return false;
        }
    }
}