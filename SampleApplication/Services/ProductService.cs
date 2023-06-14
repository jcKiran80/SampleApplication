using SampleApplication.Models;
using System.Data;
using System.Data.SqlClient;

namespace SampleApplication.Services
{
    public class ProductService
    {
        private static string dbsource = "tcp:dbservaz2041.database.windows.net";
        private static string dbUser = "dbadmin";
        private static string dbPassword = "Jck600125127@";
        private static string dbDatabase = "dbpoc204";


        private SqlConnection GetConnection()
        {
            var _builder = new SqlConnectionStringBuilder();
            _builder.DataSource = dbsource;
            _builder.UserID = dbUser;
            _builder.Password = dbPassword;
            _builder.InitialCatalog = dbDatabase;

            return new SqlConnection(_builder.ConnectionString);
        }

        public List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();
            SqlConnection conn = GetConnection();
            
            string SelectStatement = "SELECT ProductID, ProductName, ProductPrice, Quantity FROM dbo.tbl_products";
            SqlCommand cmd = new SqlCommand(SelectStatement, conn);

            conn.Open();

            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                while(dr.Read())
                {
                    Product prod = new Product();
                    prod.ProductID = dr.GetInt32("ProductID");
                    prod.ProductName = dr.GetString("ProductName");
                    prod.ProductCost = dr.GetFloat("ProductPrice");
                    prod.Quantity = dr.GetInt32("Quantity");

                    products.Add(prod);
                }
            }

            return products;
        }
    }
}
