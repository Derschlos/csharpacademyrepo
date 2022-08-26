using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralScripts_Classes
{
    internal class mvcPageSetup
    {
        [BindProperty]
        public int MyProperty { get; set; }
        public NamedModel Name { get; set; }

        private readonly IConfiguration _config;
        public CreateModel(IConfiguration config)
        {
            _config = config;
        }
        public IActionResult OnGet()
        {
            //fill values here
            return Page();
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            return RedirectToPage("./Index");
        }
    }
}
