using Prism.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TraceAnalysis.Helper;

namespace TraceAnalysis.ViewModels
{
    public class TraceAnalysisViewModel
    {
        public DelegateCommand OpenTraceCommand { get; set; }

        public TraceAnalysisViewModel()
        {
            OpenTraceCommand = new DelegateCommand(OpenTraceHandler);
        }

        private void OpenTraceHandler()
        {
            TraceLog log = new TraceLog();
            Console.WriteLine("test");
        }
    }
}
