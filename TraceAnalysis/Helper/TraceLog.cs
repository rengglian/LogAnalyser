using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TraceAnalysis.Helper
{
    public class TraceLog
    {
        public List<TraceEntry> Logfile { get; set; }
        public string Title { get; set; }

        public TraceLog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                DefaultExt = ".log",
                Filter = "Trace Files (*.log)|*.log"
            };

            Nullable<bool> result = openFileDialog.ShowDialog();

            if (result == true)
            {
                string filename = openFileDialog.FileName;
                Title = Path.GetFileName(filename);
                List<TraceEntry> logs = new List<TraceEntry>();
                var path = Path.Combine(filename);
                using (StreamReader sr = new StreamReader(path))
                {

                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        var log = Regex.Split(line, "\t");
                        logs.Add(new TraceEntry { Timestamp = DateTime.Parse(log[0]), Level = log[1], Payload = log[2..].ToList<string>() });
                    }
                }

                List<TraceEntry> efbtrigger = logs.Where(entry => entry.Payload[0] == "RequestEfbTriggeredADC").Select(entry => entry).ToList<TraceEntry>();
                Console.WriteLine("test");
            }
        }
    }
}
