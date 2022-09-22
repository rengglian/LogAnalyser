using Infrastructure.Helper;
using System.Collections.Generic;

namespace ImageAnalysis.Helper;

public class Properties
{
    public static Dictionary<string, Options> Get()
    {
        var options = new Dictionary<string, Options>
        {
            { "X um / px", new Options(30.129, "Pixel size in um") },
            { "Y um / px", new Options(30.129, "Pixel size in um") },
            { "Image Width", new Options(720, "Image Width") },
            { "Image Height", new Options(576, "Image Height") }
        };
        return options;
    }
}
