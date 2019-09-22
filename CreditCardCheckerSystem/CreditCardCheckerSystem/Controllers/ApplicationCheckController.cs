using CreditCardCheckerSystem.Models;
using CreditCardCheckerSystem.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CreditCardCheckerSystem.Controllers
{
    [Route("api/[controller]")]
    public class ApplicationCheckController : Controller
    {
        private readonly IDBAccessService dbAccess;
        public ApplicationCheckController(IDBAccessService access)
        {
            dbAccess = access;
        }
        [HttpPost("[action]")]
        public Product SubmitApplication([FromBody] Application application)
        {
            var suggestion = CheckAvailability(application);
            dbAccess.RecordRecommendation(application, suggestion);
            return suggestion;

            //var products = dbAccess.RetrieveAllProducts();
            //var approvedProducts = new List<Product>();
            //foreach (Product product in products)
            //{
            //    var criteria = dbAccess.GetCriteriaForProduct(product.Id);
            //    if (VerifyCriteria(criteria, application))
            //    {
            //        approvedProducts.Add(product);
            //    }
            //}

            //if (approvedProducts.Any())
            //{
            //    return approvedProducts.First();
            //} else
            //{
            //    return Ineligible;
            //}
        }

        private Product CheckAvailability(Application application)
        {
            if (!VerifyCustomerAge(application.DateOfBirth))
            {
                return Ineligible;
            }

            return application.AnnualIncome < 30000
                ? Vanquis
                : Barclaycard;
        }

        private bool VerifyCustomerAge(DateTime customerDob) 
        {
            if (customerDob.Year < 1900 || customerDob > DateTime.Today)
            {
                return false;
            }
            DateTime today = DateTime.Today;
            int age = today.Year - customerDob.Year;
            if (customerDob > today.AddYears(-age)) age--;
            return age >= 18;
        }

        private readonly Product Ineligible = new Product
        {
            CardName = "Ineligible",
            AprRate = 0.0d,
            PromoMessage = "No credit cards are available"
        };

        private readonly Product Vanquis = new Product
        {
            CardName = "Vanquis",
            AprRate = 50.0d,
            PromoMessage = "Free pizza every time you use it!"
        };

        private readonly Product Barclaycard = new Product
        {
            CardName = "Barclaycard",
            AprRate = 5.0d,
            PromoMessage = "Entitles you to a free Ferrari every year!"
        };
    }
}
