using KTPO4311.Zigangirov.Lib.src.LogAn;
using System;
using System.Collections.Generic;
using System.Text;

namespace KTPO4311.Zigangirov.Lib.src.SampleCommands
{
    public class SecondCommand : ISampleCommand
    {
        public SecondCommand(IView view)
        {
            this.view = view;
        }
        private readonly IView view;
        private int IExecute = 0;
        public void Execute()
        {
            IExecute++;
            view.Render(this.GetType().ToString() + "\n IExecute = " + IExecute);
        }
    }
}
