using Newtonsoft.Json;

namespace HuckHack.Models
{
    public class OAuthResponseModel
    {
        [JsonProperty("Given_name")]
        public string FirstName { get; set; }

        [JsonProperty("Family_name")]
        public string LastName { get; set; }

        [JsonProperty("Email")]
        public string Email { get; set; }
    }
}
