using KTPO4311.Zigangirov.Lib.src.LogAn;
using System;
using System.Collections.Generic;
using System.Text;

namespace KTPO4311.Zigangirov.Service.src.Views
{
    class ConsoleView : IView
    {
        public void Render(string text)
        {
            Console.WriteLine(text);
        }
    }
}
