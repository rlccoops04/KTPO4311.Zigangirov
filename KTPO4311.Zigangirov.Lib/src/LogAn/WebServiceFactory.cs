using System;
using System.Collections.Generic;
using System.Text;

namespace KTPO4311.Zigangirov.Lib.src.LogAn
{
    public static class WebServiceFactory
    {
        private static IWebService customService = null;
        /// <summary> Создание объектов </summary>
        public static IWebService Create()
        {
            if (customService != null)
            {
                return customService;
            }

            return new WebService();
        }

        ///<summary> Метод позволит тестам контролировать, 
        /// что возвращает фабрика </summary>
        public static void SetService(IWebService srvc)
        {
            customService = srvc;
        }
    }
}
