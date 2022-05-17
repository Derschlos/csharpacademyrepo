using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Water_Logger.Models;

namespace Water_Logger.Pages.Shared
{
    public class CreateModel : PageModel
    {
        
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
            var connection = "Insert into x ";
            return RedirectToPage("./index");
        }
         
        public DrinkingWaterModel DrinkingWater { get; set; }
    }
}
