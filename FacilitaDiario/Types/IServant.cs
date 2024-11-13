using System.Text.Json.Serialization;

namespace FacilitaDiario.Types
{
    public class IServant
    {
        public int Id { get; set; }

        [JsonPropertyName("enrollment")]
        public string? Enrollment { get; set; }

        [JsonPropertyName("contract")]
        public string? Contract { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("email")]
        public string? Email { get; set; }

        public bool Active { get; set; }
    }
}
