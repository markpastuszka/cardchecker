using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditCardCheckerSystem.Controllers
{
    [Route("api/[controller]")]
    public class ApplicationCheckController : Controller
    {

        [HttpPost("[action]")]
        public SuggestedProduct SubmitApplication([FromBody] CustomerApplication application)
        {
            var suggestion = CheckAvailability(application);

            // write to DB

            return suggestion;
        }

        private SuggestedProduct CheckAvailability(CustomerApplication application)
        {
            if (!CustomerIsOver18(application.DateOfBirth))
            {
                return Ineligible;
            }

            return application.AnnualIncome < 30000
                ? Vanquis
                : Barclaycard;
        }

        private bool CustomerIsOver18(DateTime customerDob) 
        {
            DateTime today = DateTime.Today;
            int age = today.Year - customerDob.Year;
            if (customerDob > today.AddYears(-age)) age--;
            return age >= 18;
        }

        private readonly SuggestedProduct Ineligible = new SuggestedProduct
        {
            CardName = "Ineligible",
            AprRate = 0.0d,
            PromoMessage = "No credit cards are available"
        };

        private readonly SuggestedProduct Vanquis = new SuggestedProduct
        {
            CardName = "Vanquis",
            AprRate = 50.0d,
            PromoMessage = "Free pizza every time you use it!"
        };

        private readonly SuggestedProduct Barclaycard = new SuggestedProduct
        {
            CardName = "Barclaycard",
            AprRate = 5.0d,
            PromoMessage = "Entitles you to a free Ferrari every year!"
        };

        public class SuggestedProduct
        {
            [JsonProperty("cardName")]
            public string CardName { get; set; }

            [JsonProperty("aprRate")]
            public double AprRate { get; set; }

            [JsonProperty("promoMessage")]
            public string PromoMessage { get; set; }
        }

        public class CustomerApplication
        {
            [JsonProperty("forename")]
            public string Forename { get; set; }
            [JsonProperty("surname")]
            public string Surname { get; set; }
            [JsonProperty("dateOfBirth")]
            public DateTime DateOfBirth { get; set; }
            [JsonProperty("annualIncome")]
            public int AnnualIncome { get; set; }
        }
    }
}
