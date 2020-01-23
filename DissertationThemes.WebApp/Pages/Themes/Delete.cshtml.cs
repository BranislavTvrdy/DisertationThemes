using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DissertationThemes.Database.Model;

namespace DissertationThemes.WebApp.Pages.Themes
{
    public class DeleteModel : PageModel
    {
        private readonly DissertationThemes.Database.Model.NorthwindContext _context;

        public DeleteModel(DissertationThemes.Database.Model.NorthwindContext context)
        {
            _context = context;
        }

        [BindProperty]
        public DissertationThemes.Database.Model.Theme Themes { get; set; }
        public Supervisor Supervisor { get; set; }
        public StProgram StProgram { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Themes = await _context.Themes.FirstOrDefaultAsync(m => m.Id == Int32.Parse(id));
            Supervisor = await _context.Supervisors.FirstOrDefaultAsync(m => m.Id == Themes.SupervisorId);
            StProgram = await _context.StPrograms.FirstOrDefaultAsync(m => m.Id == Themes.StProgramId);

            if (Themes == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Themes = await _context.Themes.FindAsync(Int32.Parse(id));
            Supervisor = await _context.Supervisors.FirstOrDefaultAsync(m => m.Id == Themes.SupervisorId);
            StProgram = await _context.StPrograms.FirstOrDefaultAsync(m => m.Id == Themes.StProgramId);

            if (Themes != null)
            {
                _context.Themes.Remove(Themes);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
