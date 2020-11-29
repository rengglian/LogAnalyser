using System;
using System.Collections.Generic;
using System.Linq;

namespace TraceAnalysis.Helper
{
    public class DataArrayEntry
    {
        public DateTime Timestamp { get; set; }
        public string Description { get; set; }
        public List<double> Values { get; set; }

        public DataArrayEntry(TraceEntry log)
        {
            Timestamp = log.Timestamp;
            Description = log.Payload[0];
            var payload = log.Payload.Skip(1);
            Values = payload.Select(x => double.Parse(x)).ToList();
        }

    }
}
