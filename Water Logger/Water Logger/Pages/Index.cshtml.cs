using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using System.Globalization;
using Water_Logger.Models;

namespace Water_Logger.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _config;
        public List<DrinkingWaterModel> Records { get; set; }

        //public IndexModel(ILogger<IndexModel> logger)
        //{
        //    _logger = logger;
        //}
        public IndexModel(IConfiguration configuration)
        {
            _config = configuration;
        }
        public void OnGet()
        {
            Records = GetAllRecords();
        }

        private List<DrinkingWaterModel> GetAllRecords()
        {
            using (var conn = new SqliteConnection(_config.GetConnectionString("ConnectionString")))
            {
                conn.Open();
                var tableCmd = conn.CreateCommand();
                tableCmd.CommandText = $@"SELECT * FROM drinking_water";
                var tableData = new List<DrinkingWaterModel>();
                SqliteDataReader reader = tableCmd.ExecuteReader();
                while (reader.Read())
                {
                    tableData.Add(new DrinkingWaterModel
                    {
                        Id = reader.GetInt32(0),
                        Date = DateTime.Parse(reader.GetString(1), CultureInfo.CurrentUICulture.DateTimeFormat),
                        Quantity = reader.GetInt32(2)
                    }); 
                }
                return tableData;
            }   
        }
    }
}