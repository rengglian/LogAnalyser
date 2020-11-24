using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraceAnalysis.Helper
{
    public class TraceEntry
    {
        public DateTime Timestamp { get; set; }
        public string Level { get; set; }
        public List<string> Payload { get; set; }
    }
}
