using Newtonsoft.Json;

namespace CreditCardCheckerSystem.Models
{
    public class Product
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("cardName")]
        public string CardName { get; set; }

        [JsonProperty("aprRate")]
        public double AprRate { get; set; }

        [JsonProperty("promoMessage")]
        public string PromoMessage { get; set; }
    }
}
