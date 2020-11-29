using System;
using System.Collections.Generic;

namespace TraceAnalysis.Helper
{
    public class TraceEntry
    {
        public DateTime Timestamp { get; set; }
        public string Level { get; set; }
        public List<string> Payload { get; set; }
    }
}
