using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using KTPO4311.Zigangirov.Lib.src.LogAn;
using NSubstitute;

namespace KTPO4311.Zigangirov.UnitTest.src.Sample
{
    class SampleNSubstituteTests
    {
        [Test]
        public void Returns_ParticularArg_Works()
        {
            //Создать поддельный объект
            IExtensionManager fakeExtensionManager = Substitute.For<IExtensionManager>();

            //Настроить объект, чтобы метод возвращал true для заданного значения входного параметра
            fakeExtensionManager.IsValid("validfile.zigangirov").Returns(true);

            //Воздействие на тестируемый объект
            bool result = fakeExtensionManager.IsValid("validfile.zigangirov");

            //Проверка ожидаемого результата
            Assert.IsTrue(result);
        }
        [Test]
        public void Returns_ArgAny_Works()
        {
            //Создать поддельный объект
            IExtensionManager fakeExtensionManager = Substitute.For<IExtensionManager>();

            //Настроить объект, чтобы метод возвращал true для заданного значения входного параметра
            fakeExtensionManager.IsValid(Arg.Any<string>()).Returns(true);

            //Воздействие на тестируемый объект
            bool result = fakeExtensionManager.IsValid("anyfile.zigangirov");

            //Проверка ожидаемого результата
            Assert.IsTrue(result);
        }
        [Test]
        public void Returns_ArgAny_Throws()
        {
            //Создать поддельный объект
            IExtensionManager fakeExtensionManager = Substitute.For<IExtensionManager>();

            //Настроить объект, чтобы метод вызвал исключение, независимо от входных аргументов
            fakeExtensionManager.When(x => x.IsValid(Arg.Any<string>()))
                .Do(context => { throw new Exception("fake exception"); });

            //Проверка, что было вызвано исключение
            Assert.Throws<Exception>(() => fakeExtensionManager.IsValid("anything"));
        }
        [Test]
        public void Received_ParticularArg_Saves()
        {
            //Создать поддельный объект
            IWebService mockWebService = Substitute.For<IWebService>();

            //Воздействие на поддельный объект
            mockWebService.LogError("Поддельное сообщение");

            //Проверка, что поддельный объект сохранил параметры вызова
            mockWebService.Received().LogError("Поддельное сообщение");
        }
    }
}
