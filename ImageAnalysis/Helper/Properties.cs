using Infrastructure.Helper;
using System.Collections.Generic;

namespace ImageAnalysis.Helper
{
    public class Properties
    {
        public static Dictionary<string, Options> Get()
        {
            var options = new Dictionary<string, Options>();
            options.Add("X um / px", new Options(30.129, "Outer most radius of spiral"));
            options.Add("Y um / px", new Options(30.129, "Number of revolutions"));
            options.Add("Image Width", new Options(720, "Distance between points"));
            options.Add("Image Height", new Options(576, "Distance between points"));
            return options;
        }
    }
}
