using Newtonsoft.Json;

namespace ArkWeb.Models
{
    public class Player
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("user")]
        public string User { get; set; }
    }
}
