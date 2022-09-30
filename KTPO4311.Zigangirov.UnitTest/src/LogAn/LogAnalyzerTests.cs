using KTPO4311.Zigangirov.Lib.src.LogAn;
using NUnit.Framework;
using System;

namespace KTPO4311.Zigangirov.UnitTest.src.LogAn
{
    [TestFixture]
    public class LogAnalyzerTests
    {
        [Test]
        public void IsValidFileName_BadExtension_ReturnsFalse()
        {
            //Подготовка теста
            LogAnalyzer analyzer = new LogAnalyzer();

            //Воздействие на тестируемый объект
            bool result = analyzer.IsValidLogFileName("filewithbadextension.FOO");

            //Проверка ожидаемого результата
            Assert.False(result);
        }


        [Test]
        public void IsValidLogFileName_GoodExtensionUppercase_ReturnsTrue()
        {
            //Подготовка теста
            LogAnalyzer analyzer = new LogAnalyzer();

            //Воздействие на тестируемый объект
            bool result = analyzer.IsValidLogFileName("filewithbadextension.ZIGANGIROV");

            //Проверка ожидаемого результата
            Assert.True(result);
        }


        [Test]
        public void IsValidLogFileName_GoodExtensionLowercase_ReturnsTrue()
        {
            //Подготовка теста
            LogAnalyzer analyzer = new LogAnalyzer();

            //Воздействие на тестируемый объект
            bool result = analyzer.IsValidLogFileName("filewithbadextension.zigangirov");

            //Проверка ожидаемого результата
            Assert.True(result);
        }

        [Test]
        public void IsValidFileName_EmptyFileName_Throws()
        {
            //Подготовка теста
            LogAnalyzer analyzer = new LogAnalyzer();

            //Функция catch перехватывает и возвращает исключение, которое было возбуждено внутри лямбда-выражения
            var ex = Assert.Catch<Exception>(() => analyzer.IsValidLogFileName(""));

            //Проверка, что исключение содержит ожидаемую строку
            StringAssert.Contains("имя файла должно быть задано", ex.Message);
        }

        [TestCase("filewithgoodextension.ZIGANGIROV")]
        [TestCase("filewithgoodextension.zigangirov")]
        public void IsValidLogFileName_ValidExtension_ReturnsTrue(string file)
        {
            LogAnalyzer analyzer = new LogAnalyzer();

            bool result = analyzer.IsValidLogFileName(file);

            Assert.True(result);
        }

        [TestCase("badfile.foo", false)]
        [TestCase("goodfale.zigangirov", true)]
        public void IsValidFileName_WhenCalled_ChangesWasLastFileNameValid(string file, bool expected)
        {
            LogAnalyzer analyzer = new LogAnalyzer();

            analyzer.IsValidLogFileName(file);

            Assert.AreEqual(expected, analyzer.WasLastFileNameValid);
        }
    }
}
