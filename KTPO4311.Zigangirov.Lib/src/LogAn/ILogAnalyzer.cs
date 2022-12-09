namespace KTPO4311.Zigangirov.Lib.src.LogAn
{
    public interface ILogAnalyzer
    {
        event LogAnalyzerAction Analyzed;

        void Analyze(string fileName);
        bool IsValidLogFileName(string fileName);
    }
}