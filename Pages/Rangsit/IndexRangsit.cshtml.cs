using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace InventorySystem.Pages.Rangsit
{
    public class IndexRangsitModel : PageModel
    {
        public List<StockInfo> listStocks = new List<StockInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Server = tcp:inventory254711.database.windows.net,1433; Initial Catalog = inventory; Persist Security Info = False; User ID = kritsada; Password =Balltaro2; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30;";
                using (SqlConnection connecttion = new SqlConnection(connectionString))
                {
                    connecttion.Open();
                    String sql = "SELECT * FROM stocks WHERE storeid = 1";
                    using (SqlCommand command = new SqlCommand(sql, connecttion))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                StockInfo stockInfo = new StockInfo();
                                stockInfo.itemid = "" + reader.GetInt32(0);
                                stockInfo.item = reader.GetString(1);
                                stockInfo.storeid = reader.GetString(2);
                                stockInfo.supplier = reader.GetString(3);
                                stockInfo.amount = reader.GetString(4);
                                stockInfo.create_at = reader.GetDateTime(5).ToString();
                                listStocks.Add(stockInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Expection: " + ex.ToString());
            }
        }
    }
    public class StockInfo
    {
        public String itemid;
        public String item;
        public String storeid;
        public String supplier;
        public String amount;
        public String create_at;
    }
}