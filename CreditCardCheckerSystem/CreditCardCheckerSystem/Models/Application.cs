using Newtonsoft.Json;
using System;

namespace CreditCardCheckerSystem.Models
{
    public class Application
    {
        [JsonProperty("forename")]
        public string Forename { get; set; }
        [JsonProperty("surname")]
        public string Surname { get; set; }
        [JsonProperty("dateOfBirth")]
        public DateTime DateOfBirth { get; set; }
        [JsonProperty("annualIncome")]
        public double AnnualIncome { get; set; }
    }
}
