using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using Water_Logger.Models;

namespace Water_Logger.Pages.Shared
{
    public class CreateModel : PageModel
    {
        private readonly IConfiguration _config;
        public CreateModel(IConfiguration config)
        {
            _config = config;
        }
        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }
            using(var conn = new SqliteConnection(_config.GetConnectionString("ConnectionString")))
            {
                conn.Open();
                var tableCmd = conn.CreateCommand();
                tableCmd.CommandText =
                    $"INSERT INTO drinking_water(date, quantity) VALUES('{DrinkingWater.Date}',{DrinkingWater.Quantity})";
                tableCmd.ExecuteNonQuery();
                conn.Close();
            }
            return RedirectToPage("./Index");
        }
        [BindProperty] 
        public DrinkingWaterModel DrinkingWater { get; set; }

    }
}
