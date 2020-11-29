using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using TraceAnalysis.Db;

namespace TraceAnalysis.Helper
{
    public class TraceLog
    {
        public List<TraceEntry> Logfile { get; set; }
        public string Title { get; set; }

        public Dictionary<string, List<TraceEntry>> FilteredTraces { get; set; } = new Dictionary<string, List<TraceEntry>>();

        public List<DataArrayEntry> DataArrayEntries { get; set; } = new List<DataArrayEntry>();

        public TraceLog(List<Parser> parsers)
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
                Logfile = new List<TraceEntry>();
                var path = Path.Combine(filename);
                using (StreamReader sr = new StreamReader(path))
                {

                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        var log = Regex.Split(line, "\t");
                        Logfile.Add(new TraceEntry { Timestamp = DateTime.Parse(log[0]), Level = log[1], Payload = log[2..].ToList<string>() });
                    }
                }
                parsers.ForEach(parser => 
                {
                    FilteredTraces.Add(parser.Name, Logfile.Where(entry => entry.Payload[0].Contains(parser.Pattern)).Select(entry => entry).ToList<TraceEntry>());

                    FilteredTraces[parser.Name].ForEach(trace =>
                   {
                       if (parser.Type == typeof(DataArrayEntry).ToString())
                       {
                           DataArrayEntries.Add(new DataArrayEntry(trace));
                       }
                       else
                       {

                       }
                   });
                    
                });
            }
        }
        private object GetInstance(string strFullyQualifiedName)
        {
            Type t = Type.GetType(strFullyQualifiedName);
            return Activator.CreateInstance(t);
        }
    }
}
