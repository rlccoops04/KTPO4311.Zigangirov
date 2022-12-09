using System;
using System.Collections.Generic;
using System.Text;
using KTPO4311.Zigangirov.Lib.src.LogAn;
using NUnit.Framework;
using NSubstitute;

namespace KTPO4311.Zigangirov.UnitTest.src.LogAn
{
    class PresenterTests
    {
        [Test]
        public void ctor_WhenAnalyzed_CallsViewRender()
        {
            FakeLogAnalyzer mockLog = new FakeLogAnalyzer();
            IView view = Substitute.For<IView>();
            Presenter presenter = new Presenter(mockLog, view);

            mockLog.CallRaiseAnalyzedEvent();
            view.Received().Render("Обработка завершена");
        }
        [Test]
        public void ctor_WhenAnalyzed_CallsViewRender_NSubstitute()
        {
            ILogAnalyzer stubLogAnalyzer = Substitute.For<ILogAnalyzer>();
            IView view = Substitute.For<IView>();
            Presenter presenter = new Presenter(stubLogAnalyzer, view);
            stubLogAnalyzer.Analyzed += Raise.Event<LogAnalyzerAction>();

            view.Received().Render("Обработка завершена");
        }
    }
    class FakeLogAnalyzer : LogAnalyzer
    {
        public void CallRaiseAnalyzedEvent()
        {
            base.RaiseAnalyzedEvent();
        }
    }
}
