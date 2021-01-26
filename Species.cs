using System.Text.Json.Serialization;

namespace ApiClient
{
    public class Species
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("average_height")]
        public string AvgHeight { get; set; }

        [JsonPropertyName("classification")]
        public string Classification { get; set; }
        [JsonPropertyName("language")]
        public string Language { get; set; }
        [JsonPropertyName("average_lifespan")]
        public string AvgLifespan { get; set; }
        [JsonPropertyName("eye_colors")]
        public string EyeColor { get; set; }

        [JsonPropertyName("hair_colors")]
        public string HairColor { get; set; }

        [JsonPropertyName("designation")]
        public string Designation { get; set; }
    }
}