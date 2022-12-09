using KTPO4311.Zigangirov.Lib.src.LogAn;
using System;
using System.Collections.Generic;
using System.Text;

namespace KTPO4311.Zigangirov.Lib.src.SampleCommands
{
    public class ExceptionCommandDecorator : ISampleCommand
    {
        private readonly ISampleCommand sampleCommand;
        private readonly IView view;
        public ExceptionCommandDecorator(ISampleCommand sampleCommand, IView view)
        {
            this.sampleCommand = sampleCommand;
            this.view = view;
        }
        public void Execute()
        {
            try
            {
                sampleCommand.Execute();
            }
            catch
            {
                view.Render("Перехват исключений: " + this.GetType().ToString());
            }
        }
    }
}
