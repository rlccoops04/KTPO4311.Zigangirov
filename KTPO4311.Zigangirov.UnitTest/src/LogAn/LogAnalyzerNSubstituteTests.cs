using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using KTPO4311.Zigangirov.Lib.src.LogAn;
using NSubstitute;

namespace KTPO4311.Zigangirov.UnitTest.src.LogAn
{
    class LogAnalyzerNSubstituteTests
    {
        [Test]
        public void IsValidFileName_NameSupportedExtension_ReturnsTrue()
        {
            IExtensionManager fakeExtensionManager = Substitute.For<IExtensionManager>();
            fakeExtensionManager.IsValid("fileName.zigangirov").Returns(true);
            ExtensionManagerFactory.SetManager(fakeExtensionManager);
            LogAnalyzer log = new LogAnalyzer();
            bool result = log.IsValidLogFileName("fileName.zigangirov");
            Assert.IsTrue(result);
        }
        [Test]
        public void IsValidFileName_NoneSupportedExtension_ReturnsFalse()
        {
            IExtensionManager fakeExtensionManager = Substitute.For<IExtensionManager>();
            fakeExtensionManager.IsValid("wrongfilename.zigangirov").Returns(false);
            ExtensionManagerFactory.SetManager(fakeExtensionManager);
            LogAnalyzer log = new LogAnalyzer();
            bool result = log.IsValidLogFileName("wrongfilename.zigangirov");
            Assert.IsFalse(result);
        }
        [Test]
        public void IsValidFileName_ExtManagerThrowsException_ReturnsFalse()
        {
            IExtensionManager fakeExtensionManager = Substitute.For<IExtensionManager>();
            fakeExtensionManager.When(x => x.IsValid("wrongname.zigangirov"))
                .Do(context => { throw new Exception("fake exception"); });
            ExtensionManagerFactory.SetManager(fakeExtensionManager);
            LogAnalyzer log = new LogAnalyzer();
            bool result = log.IsValidLogFileName("wrongname.zigangirov");
            Assert.IsFalse(result);
        }
        [Test]
        public void Analyze_TooShortFileName_CallsWebService()
        {
            //Подготовка теста
            IWebService mockWebService = Substitute.For<IWebService>();
            WebServiceFactory.SetService(mockWebService);
            LogAnalyzer log = new LogAnalyzer();
            string fileName = "abc.zigangirov";

            //Воздействие на тестируемый объект
            log.Analyze(fileName);

            //Проверка ожидаемого результата
            mockWebService.Received().LogError("Слишком короткое имя файла: abc.zigangirov");
        }
        [Test]
        public void Analyze_WebServiceThrows_SendsEmail()
        {
            //Подготовка теста
            IWebService stubWebService = Substitute.For<IWebService>();
            WebServiceFactory.SetService(stubWebService);
            stubWebService.When(x => x.LogError(Arg.Any<string>()))
                 .Do(context => { throw new Exception("Это подделка"); });

            IEmailService mockEmail = Substitute.For<IEmailService>();
            EmailServiceFactory.SetEmail(mockEmail);

            LogAnalyzer log = new LogAnalyzer();
            string tooShortFileName = "abc.zigangirov";

            //Воздействие на тестируемый объект 
            log.Analyze(tooShortFileName);

            //Проверка ожидаемого результата
            mockEmail.Received().SendEmail("someone@somewhere.zigangirov", "Невозможно вызвать веб-сервис", "Это подделка");
        }


        [TearDown]
        public void AfterEachTest()
        {
            ExtensionManagerFactory.SetManager(null);
            WebServiceFactory.SetService(null);
            EmailServiceFactory.SetEmail(null);
        }
    }
}
