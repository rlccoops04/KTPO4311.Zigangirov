﻿using System;
using System.Collections.Generic;
using System.Text;

namespace KTPO4311.Zigangirov.Lib.src.LogAn
{
    public class Presenter
    {
        ILogAnalyzer logAnalyzer;
        IView view;
        public Presenter(ILogAnalyzer logAnalyzer, IView view)
        {
            this.logAnalyzer = logAnalyzer;
            this.view = view;
            logAnalyzer.Analyzed += OnLogAnalyzed;
        }
        private void OnLogAnalyzed()
        {
            view.Render("Обработка завершена");
        }
    }
}
