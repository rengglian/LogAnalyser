using Infrastructure.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ImageAnalysis.Helper
{
    public class CameraCalibrationMessage
    {
        public static Dictionary<string, Options> Parse(string message)
        {
            var json = JsonSerializer.Deserialize< Dictionary<string, double> > (message);

            var props = Properties.Get();
            foreach (KeyValuePair<string, double> entry in json)
            {
                if (props.ContainsKey(entry.Key))
                {
                    props[entry.Key].Value = entry.Value;
                }
            }

            return props;
        }
    }
}
