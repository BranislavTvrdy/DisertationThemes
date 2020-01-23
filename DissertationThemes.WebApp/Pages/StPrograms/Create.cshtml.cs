using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DissertationThemes.Database.Model;

namespace DissertationThemes.WebApp.Pages.StPrograms
{
    public class CreateModel : PageModel
    {
        private readonly DissertationThemes.Database.Model.NorthwindContext _context;

        public CreateModel(DissertationThemes.Database.Model.NorthwindContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public DissertationThemes.Database.Model.StProgram StPrograms { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.StPrograms.Add(StPrograms);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}