using KTPO4311.Zigangirov.Lib.src.LogAn;
using NUnit.Framework;
using System;

namespace KTPO4311.Zigangirov.UnitTest.src.LogAn
{
    [TestFixture]
    public class LogAnalyzerTests
    {
        [Test]
        public void IsValidFileName_NameSupportedExtension_ReturnsTrue()
        {
            //Подготовка теста
            FakeExtensionManager fakeManager = new FakeExtensionManager();
            fakeManager.WillBeValid = true;
            //..конфигурируем фабрику для создания поддельных объектов 
            ExtensionManagerFactory.SetManager(fakeManager);
            LogAnalyzer log = new LogAnalyzer();

            //Воздействие на тестируемый объект
            bool result = log.IsValidLogFileName("short.zigangirov");

            //Проверка ожидаемого результата 
            Assert.True(result);
        }   
        [Test]
        public void IsValidFileName_NameSupportedExtension_ReturnsFalse()
        {
            //Подготовка теста
            FakeExtensionManager fakeManager = new FakeExtensionManager();
            fakeManager.WillBeValid = false;
            //..конфигурируем фабрику для создания поддельных объектов 
            ExtensionManagerFactory.SetManager(fakeManager);
            LogAnalyzer log = new LogAnalyzer();

            //Воздействие на тестируемый объект
            bool result = log.IsValidLogFileName("short.zigangirov");

            //Проверка ожидаемого результата 
            Assert.False(result);
        }
        [Test]
        public void IsValidFileName_ExtManagerThrowsException_ReturnsFalse()
        {
            //Подготовка теста
            FakeExtensionManager fakeManager = new FakeExtensionManager();
            fakeManager.WillThrow = new Exception();
            //..конфигурируем фабрику для создания поддельных объектов 
            ExtensionManagerFactory.SetManager(fakeManager);
            LogAnalyzer log = new LogAnalyzer();
            bool result = log.IsValidLogFileName("short.zigangirov");

            //Проверка ожидаемого результата 
            Assert.IsFalse(result);
        }

        [Test]
        public void Analyze_TooShortFileName_CallsWebService()
        {
            //Подготовка теста
            FakeWebService mockWebService = new FakeWebService();
            WebServiceFactory.SetService(mockWebService);
            LogAnalyzer log = new LogAnalyzer();
            string fileName = "abc.zigangirov";

            //Воздействие на тестируемый объект
            log.Analyze(fileName);

            //Проверка ожидаемого результата
            StringAssert.Contains("Слишком короткое имя файла: abc.zigangirov", mockWebService.LastError);

        }
        [Test]
        public void Analyze_WebServiceThrows_SendsEmail()
        {
            //Подготовка теста
            FakeWebService stubWebService = new FakeWebService();
            WebServiceFactory.SetService(stubWebService);
            stubWebService.WillThrow = new Exception("Это подделка");

            FakeEmailService mockEmail = new FakeEmailService();
            EmailServiceFactory.SetEmail(mockEmail);

            LogAnalyzer log = new LogAnalyzer();
            string tooShortFileName = "abc.zigangirov";

            //Воздействие на тестируемый объект 
            log.Analyze(tooShortFileName);

            //Проверка ожидаемого результата
            StringAssert.Contains("someone@somewhere.zigangirov", mockEmail.to);
            StringAssert.Contains("Это подделка", mockEmail.body);
            StringAssert.Contains("Невозможно вызвать веб-сервис", mockEmail.subject);
        }
        [Test]
        public void Analyze_WhenAnalyzed_FiredEvent()
        {
            bool analyzedFired = false;
            LogAnalyzer logAnalyzer = new LogAnalyzer();
            logAnalyzer.Analyzed += delegate ()
            {
                analyzedFired = true;
            };

            logAnalyzer.Analyze("valid.zigangirov");

            Assert.IsTrue(analyzedFired);
        }


        [TearDown]
        public void AfterEachTest()
        {
            ExtensionManagerFactory.SetManager(null);
            WebServiceFactory.SetService(null);
            EmailServiceFactory.SetEmail(null);
        }
    }



    /// <summary> Поддельный менеджер расширений </summary>
    internal class FakeExtensionManager : IExtensionManager
    {
        ///<summary>  Это поле позволяет настроить 
        /// поддельный результат для метода IsValid </summary>
        public bool WillBeValid = false;

        /// <summary>Это поле позволяет настроить 
        /// поддельное исключение вызываемое в методе IsValid</summary>
        public Exception WillThrow = null;
        public bool IsValid(string fileName)
        {
            if(WillThrow != null)
            {
                throw WillThrow;
            }

            return WillBeValid;
        }
    }
    /// <summary> Поддельная веб-служба </summary>
    internal class FakeWebService : IWebService
    {
        ///<summary>  Это поле запоминает состояние после вызова метода LogError 
        /// при тестировании взаимодействия утверждения высказываются относительно</summary>
        public string LastError;
        public Exception WillThrow = null;
        public void LogError(string message)
        {
            if (WillThrow != null)
            {
                throw WillThrow;
            }

            LastError = message;
        }
    }
    internal class FakeEmailService : IEmailService
    {
        public string to;
        public string subject;
        public string body;
        public void SendEmail(string to, string subject, string body)
        {
            this.to = to;
            this.subject = subject;
            this.body = body;
        }
    }
}
