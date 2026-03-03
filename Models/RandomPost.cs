using System.Text.Json.Serialization;

namespace Random_Posts.Models
{
    public class RandomPost
    {
        [JsonPropertyName("Id")]
        public Guid Guid { get; set; }
        public string Title {  get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public DateTime? CreateAt { get; set; }
    }
}
