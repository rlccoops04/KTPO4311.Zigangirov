using Castle.MicroKernel.Internal;
using KTPO4311.Zigangirov.Lib.src.LogAn;
using KTPO4311.Zigangirov.Lib.src.SampleCommands;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using System.Text;

namespace KTPO4311.Zigangirov.UnitTest.src.Sample
{
    public class SampleCommandTests
    {
        [Test]
        public void FirstCommand_Execute_IsValidText()
        {
            IView mockView = Substitute.For<IView>();
            FirstCommand firstCommand = new FirstCommand(mockView);
            firstCommand.Execute();
            int IExecute = 1;
            mockView.Received().Render(firstCommand.GetType().ToString() + "\n IExecute = " + IExecute);
        }
        [Test]
        public void SampleCommandDecorator_Execute_CallsExecute()
        {
            IView mockView = Substitute.For<IView>();
            ISampleCommand mockSampleCommand = Substitute.For<ISampleCommand>();
            SampleCommandDecorator sampleCommandDecorator = new SampleCommandDecorator(mockSampleCommand, mockView);
            sampleCommandDecorator.Execute();
            mockSampleCommand.Received().Execute();
        }
        [Test]
        public void SampleCommandDecorator_Execute_IsValidtext()
        {
            IView mockView = Substitute.For<IView>();
            ISampleCommand mockSampleCommand = Substitute.For<ISampleCommand>();
            SampleCommandDecorator sampleCommandDecorator = new SampleCommandDecorator(mockSampleCommand, mockView);
            sampleCommandDecorator.Execute();

            mockView.Received().Render("Начало: " + sampleCommandDecorator.GetType().ToString());
            mockView.Received().Render("Конец: " + sampleCommandDecorator.GetType().ToString());
        }
        [Test]
        public void ExceptionCommandDecorator_Execute_CallsExecute()
        {
            IView fakeView = Substitute.For<IView>();
            ISampleCommand sampleCommand = Substitute.For<ISampleCommand>();
            ExceptionCommandDecorator exceptionCommandDecorator = new ExceptionCommandDecorator(sampleCommand, fakeView);
            exceptionCommandDecorator.Execute();

            sampleCommand.Received().Execute();
        }
        [Test]
        public void ExceptionCommandDecorator_Execute_CatchException()
        {
            IView view = Substitute.For<IView>();
            ISampleCommand sampleCommand = Substitute.For<ISampleCommand>();
            sampleCommand.When(x => x.Execute()).Do(context => { throw new Exception(); });
            ExceptionCommandDecorator exceptionCommandDecorator = new ExceptionCommandDecorator(sampleCommand, view);
            exceptionCommandDecorator.Execute();
            view.Received().Render("Перехват исключений: " + exceptionCommandDecorator.GetType().ToString());
        }
    }
}
