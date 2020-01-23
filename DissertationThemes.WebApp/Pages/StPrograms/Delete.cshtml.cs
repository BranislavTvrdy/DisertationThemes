using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DissertationThemes.Database.Model;

namespace DissertationThemes.WebApp.Pages.StPrograms
{
    public class DeleteModel : PageModel
    {
        private readonly DissertationThemes.Database.Model.NorthwindContext _context;

        public DeleteModel(DissertationThemes.Database.Model.NorthwindContext context)
        {
            _context = context;
        }

        [BindProperty]
        public DissertationThemes.Database.Model.StProgram StPrograms { get; set; }
        public IList<Database.Model.Theme> Themes { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            StPrograms = await _context.StPrograms.FirstOrDefaultAsync(m => m.Id == Int32.Parse(id));
            Themes = await _context.Themes.Where(t => t.StProgramId == Int32.Parse(id)).ToListAsync();

            if (StPrograms == null)
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

            StPrograms = await _context.StPrograms.FindAsync(Int32.Parse(id));
            Themes = await _context.Themes.Where(t => t.StProgramId == Int32.Parse(id)).ToListAsync();

            if (StPrograms != null)
            {
                _context.StPrograms.Remove(StPrograms);
                _context.Themes.RemoveRange(Themes);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
