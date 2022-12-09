using System;
using System.Collections.Generic;
using System.Text;

namespace KTPO4311.Zigangirov.Lib.src.LogAn
{
    public class EmailServiceFactory
    {
        private static IEmailService customEmailService = null;
        /// <summary> Создание объектов </summary>
        public static IEmailService Create()
        {
            if (customEmailService != null)
            {
                return customEmailService;
            }
            //настоящая почтовая служба ещё не реализована
            throw new NotImplementedException();
        }

        ///<summary> Метод позволит тестам контролировать, 
        /// что возвращает фабрика </summary>
        public static void SetEmail(IEmailService emlsrvc)
        {
            customEmailService = emlsrvc;
        }
    }
}
