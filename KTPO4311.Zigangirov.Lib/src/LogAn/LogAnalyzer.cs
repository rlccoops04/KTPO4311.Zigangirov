using System;
using System.Collections.Generic;
using System.Text;

namespace KTPO4311.Zigangirov.Lib.src.LogAn
{
    // Объявление делегата
    public delegate void LogAnalyzerAction();
    /// <summary>Анализатор лог. файлов</summary>

    public class LogAnalyzer : ILogAnalyzer
    {
        //Объявление события
        public event LogAnalyzerAction Analyzed = null;
        /// <summary> Проверка правильности имя файла </summary>
        public bool IsValidLogFileName(string fileName)
        {
            try
            {
                IExtensionManager mgr = ExtensionManagerFactory.Create();
                return mgr.IsValid(fileName);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>Анализировать лог. файл</summary>
        /// <param name="filename"></param>
        public void Analyze(string fileName)
        {
            if (fileName.Length < 15)
            {
                try
                {
                    //Передать внешней службе сообщение об ошибке
                    IWebService srvc = WebServiceFactory.Create();
                    srvc.LogError("Слишком короткое имя файла: " + fileName);
                }
                catch (Exception e)
                {
                    //Отправить сообщение по электронной почте
                    //email.SendEmail("someone@somwhere.zigangirov", "Невозможно вызвать веб-сервис", e.Message);
                    IEmailService emlsrvc = EmailServiceFactory.Create();
                    emlsrvc.SendEmail("someone@somewhere.zigangirov", "Невозможно вызвать веб-сервис", e.Message);
                }
            }
            //Вызов события
            if (Analyzed != null)
            {
                Analyzed();
            }
        }
        protected void RaiseAnalyzedEvent()
        {
            Analyzed();
        }
    }
}
