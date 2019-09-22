using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using CreditCardCheckerSystem.Models;
using System;

namespace CreditCardCheckerSystem.Services
{
    public interface IDBAccessService
    {
        IEnumerable<Product> RetrieveAllProducts();

        IEnumerable<string> GetCriteriaForProduct(int productId);

        void RecordRecommendation(Application application, Product recommendation);
    }

    public class DBAccessService: IDBAccessService
    {
        private readonly SqlParameter[] NoParams = new SqlParameter[] { };
        private const string ConnectionString = "Data Source=MARK-PC;Initial Catalog=CardCheckerDatabase;Integrated Security=True";
        public IEnumerable<Product> RetrieveAllProducts()
        {
            var products = new List<Product>();
            DataTable dataFromTable = GetDataTable("SELECT * FROM PRODUCTS", NoParams);
            foreach (DataRow r in dataFromTable.Rows)
            {
                products.Add(new Product
                {
                    Id = (int)r["id"],
                    AprRate = (double)r["apr_rate"],
                    CardName = r["product_name"].ToString(),
                    PromoMessage = r["promotional_msg"].ToString()
                });
            }
            return products;
        }

        public IEnumerable<string> GetCriteriaForProduct(int productId)
        {
            var criteria = new List<string>();
            var productIdParam = new SqlParameter
            {
                ParameterName = "@ProductId",
                Value = productId
            };
            var criteriaFromProduct = GetDataTable(
                "SELECT * FROM CRITERIA WHERE id in (SELECT criteria_id FROM PRODUCT_CRITERIA_MAP WHERE product_id = @ProductId)", new[] { productIdParam });
            foreach (DataRow r in criteriaFromProduct.Rows)
            {
                criteria.Add(r["criteria"].ToString());
            }


            return criteria;

        }

        public void RecordRecommendation(Application application, Product recommendation)
        {
            var query = "insert into APPLICATION_HISTORY (forename, surname, dob, annual_income, product_id) values(@Forename, @Surname, @DateOfBirth, @Income, @ProductName)";
            Write(query, GenerateParameters(application, recommendation.CardName));
        }

        private void Write(string query, SqlParameter[] sqlParams)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                SqlCommand com = new SqlCommand(query, conn);
                com.Parameters.AddRange(sqlParams);
                var result = com.ExecuteNonQuery();
                if (result < 0)
                {
                    Console.WriteLine("Error writing to DB");
                }
                conn.Close();
            }
        }

        private SqlParameter[] GenerateParameters(Application application, string ProductName)
        {
            return new SqlParameter[]
            {
                new SqlParameter
                {
                    ParameterName = "@Forename",
                    Value = application.Forename
                },
                new SqlParameter
                {
                    ParameterName = "@Surname",
                    Value = application.Surname
                },
                new SqlParameter
                {
                    ParameterName = "@DateOfBirth",
                    Value = application.DateOfBirth
                },
                new SqlParameter
                {
                    ParameterName = "@Income",
                    Value = application.AnnualIncome
                },
                new SqlParameter
                {
                    ParameterName = "@ProductName",
                    Value = ProductName
                },
            };
        }

        private DataTable GetDataTable(string query, SqlParameter[] sqlParams)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                SqlCommand com = new SqlCommand(query, conn);
                com.Parameters.AddRange(sqlParams);
                SqlDataAdapter da = new SqlDataAdapter(com);

                da.Fill(dt);
                conn.Close();
            }

            return dt;
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection
            {
                ConnectionString = ConnectionString
            };
        }

    }
}
