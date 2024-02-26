using System.Text.Json.Serialization;

namespace LaborationBlazorShop.Models
{
    public class Currency
    {
        [JsonPropertyName("currency_pair")]
        public string? currency_pair { get; set; }

        [JsonPropertyName("exchange_rate")]
        public decimal exchange_rate { get; set; }
    }
}
