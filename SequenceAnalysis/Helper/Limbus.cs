using System.Text.Json.Serialization;

namespace SequenceAnalysis.Helper
{
    public class Limbus
    {
        //https://stackoverflow.com/questions/26882986/overwrite-json-property-name-in-c-sharp

        [JsonPropertyName("height")]
        public double Height { get; set; }

        [JsonPropertyName("rotationAngle")]
        public double RotationAngle { get; set; }

        [JsonPropertyName("width")]
        public double Width { get; set; }

        [JsonPropertyName("x")]
        public double X { get; set; }

        [JsonPropertyName("y")]
        public double Y { get; set; }

    }
}