using Infrastructure.Helper;
using System.Collections.Generic;

namespace CameraCalibration.Helper
{
    public class AnalyseOptions
    {
        public static Dictionary<string, Options> Get()
        {
            var options = new Dictionary<string, Options>
            {
                { "Center X", new Options(0, "Center of region of interest X") },
                { "Center Y", new Options(0, "Center of region of interest Y") },
                { "ROI Width", new Options(520, "Region of interest Width") },
                { "ROI Height", new Options(520, "Region of interest Height") },
                { "Features X", new Options(16, "Number of features to detect in X") },
                { "Features Y", new Options(16, "Number of features to detect in Y") }
            };
            return options;
        }
    }
}
