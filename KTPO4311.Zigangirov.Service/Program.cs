using System;
using KTPO4311.Zigangirov.Lib.src.Common;
using KTPO4311.Zigangirov.Lib.src.LogAn;
using KTPO4311.Zigangirov.Lib.src.SampleCommands;
using KTPO4311.Zigangirov.Service.src.WindsorInstallers;

namespace KTPO4311.Zigangirov.Service
{
    class Program
    {
        static void Main(string[] args)
        {
/*            LogAnalyzer logAnalyzer = new LogAnalyzer();
            Console.WriteLine(logAnalyzer.IsValidLogFileName("file.zigangirov"));
            Console.WriteLine(logAnalyzer.IsValidLogFileName("file.exe"));
            Console.WriteLine(logAnalyzer.IsValidLogFileName("file.pdf"));
            Console.WriteLine(logAnalyzer.IsValidLogFileName("file.txt"))*/
            CastleFactory.container.Install(
                new SampleCommandInstaller(),
                new ViewInstaller()
                );
            for (int i = 0; i < 3; i++)
            {
                ISampleCommand someCommand = CastleFactory.container.Resolve<ISampleCommand>();
                someCommand.Execute();
            }
        }
    }
}
