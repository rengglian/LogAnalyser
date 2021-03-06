﻿using Prism.Commands;
using System;
using System.Linq;
using TraceAnalysis.Db;
using TraceAnalysis.Helper;

namespace TraceAnalysis.ViewModels
{
    public class TraceAnalysisViewModel
    {
        public DelegateCommand OpenTraceCommand { get; set; }

        private static readonly SettingsContext _context = new SettingsContext();

        public TraceAnalysisViewModel()
        {

            var ret = _context.Database.EnsureCreated();
            if (ret)
            {
                _context.InitDb();
            }

            OpenTraceCommand = new DelegateCommand(OpenTraceHandler);
        }

        private void OpenTraceHandler()
        {
            var parsers = _context.Parsers.ToList();
            TraceLog log = new TraceLog(parsers);
            Console.WriteLine("test");
        }
    }
}
