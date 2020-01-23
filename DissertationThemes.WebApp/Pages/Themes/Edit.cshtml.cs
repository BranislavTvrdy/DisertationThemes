using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DissertationThemes.Database.Model;

namespace DissertationThemes.WebApp.Pages.Themes
{
    public class EditModel : PageModel
    {
        private readonly DissertationThemes.Database.Model.NorthwindContext _context;

        public EditModel(DissertationThemes.Database.Model.NorthwindContext context)
        {
            _context = context;
        }

        [BindProperty]
        public DissertationThemes.Database.Model.Theme Themes { get; set; }
        public DissertationThemes.Database.Model.Supervisor Supervisors { get; set; }
        public DissertationThemes.Database.Model.StProgram StPrograms { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Themes = await _context.Themes.FirstOrDefaultAsync(m => m.Id == Int32.Parse(id));
            Supervisors = await _context.Supervisors.FirstOrDefaultAsync(m => m.Id == Themes.SupervisorId);
            StPrograms = await _context.StPrograms.FirstOrDefaultAsync(m => m.Id == Themes.StProgramId);

            if (Themes == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Themes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomersExists(Themes.Id.ToString()))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool CustomersExists(string id)
        {
            return _context.Supervisors.Any(e => e.Id == Int32.Parse(id));
        }
    }
}
