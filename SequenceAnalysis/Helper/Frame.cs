using System.Text.Json.Serialization;

namespace SequenceAnalysis.Helper
{
    public class Frame
    {
        [JsonPropertyName("cycloRotationScore")]
        public int CycloRotationScore { get; set; }

        [JsonPropertyName("cycloRotationTracking")]
        public int CycloRotationTracking { get; set; }

        [JsonPropertyName("elevationScore")]
        public int ElevationScore { get; set; }

        [JsonPropertyName("elevationTracking")]
        public int ElevationTracking { get; set; }

        [JsonPropertyName("frameNumber")]
        public int FrameNumber { get; set; }

        [JsonPropertyName("gazeScore")]
        public int GazeScore { get; set; }

        [JsonPropertyName("gazeTracking")]
        public int GazeTracking { get; set; }

        [JsonPropertyName("hasCorrespondingImage")]
        public bool HasCorrespondingImage { get; set; }

        [JsonPropertyName("isCyclorotationImagePresent")]
        public bool IsCyclorotationImagePresent { get; set; }

        [JsonPropertyName("limbus")]
        public Limbus Limbus { get; set; }

        [JsonPropertyName("limbusScore")]
        public int LimbusScore { get; set; }

        [JsonPropertyName("limbusTracking")]
        public int LimbusTracking { get; set; }

        [JsonPropertyName("trackingState")]
        public int TrackingState { get; set; }
    }
}
