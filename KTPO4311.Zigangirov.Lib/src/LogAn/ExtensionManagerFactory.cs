using System;
using System.Collections.Generic;
using System.Text;

namespace KTPO4311.Zigangirov.Lib.src.LogAn
{
    /// <summary> Фабрика диспетчеров расширений файлов </summary>
    public static class ExtensionManagerFactory
    {
        private static IExtensionManager customManager = null;
        /// <summary> Создание объектов </summary>
        public static IExtensionManager Create()
        {
            if(customManager != null)
            {
                return customManager;
            }

            return new FileExtensionManager();
        }

        ///<summary> Метод позволит тестам контролировать, 
        /// что возвращает фабрика </summary>
        public static void SetManager(IExtensionManager mgr)
        {
            customManager = mgr;
        }
    }
}
