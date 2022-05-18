using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using Water_Logger.Models;

namespace Water_Logger.Pages
{
    public class UpdateModel : PageModel
    {
        private readonly IConfiguration _config;
        public UpdateModel(IConfiguration configuration)
        {
            _config = configuration;
        }
        [BindProperty]
        public DrinkingWaterModel DrinkingWater { get; set; }

        public IActionResult OnGet(int id)
        {
            DrinkingWater = GetByID(id);
            return Page();
        }

        private DrinkingWaterModel GetByID(int id)
        {
            var waterRecord = new DrinkingWaterModel();
            using (var conn = new SqliteConnection(_config.GetConnectionString("ConnectionString")))
            {
                conn.Open();
                var tableCmd = conn.CreateCommand();
                tableCmd.CommandText =
                    $@"SELECT * FROM drinking_water WHERE {id}";
                SqliteDataReader reader = tableCmd.ExecuteReader();
                while (reader.Read())
                {
                    waterRecord.Id = reader.GetInt32(0);
                    waterRecord.Date = DateTime.Parse(reader.GetString(1));
                    waterRecord.Quantity = reader.GetInt32(2);
                }
                return waterRecord;
            }
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            using (var conn = new SqliteConnection(_config.GetConnectionString("ConnectionString")))
            {
                conn.Open();
                var tableCmd = conn.CreateCommand();
                tableCmd.CommandText =
                    @$"UPDATE drinking_water SET date ='{DrinkingWater.Date}',
                        quantity = {DrinkingWater.Quantity} WHERE id = {DrinkingWater.Id}";
                tableCmd.ExecuteNonQuery();
                conn.Close();
            }
            return RedirectToPage("./Index");
        }
    }
}
