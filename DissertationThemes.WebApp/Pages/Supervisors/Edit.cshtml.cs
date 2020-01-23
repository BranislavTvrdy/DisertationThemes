using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DissertationThemes.Database.Model;

namespace DissertationThemes.WebApp.Pages.Supervisors
{
    public class EditModel : PageModel
    {
        private readonly DissertationThemes.Database.Model.NorthwindContext _context;

        public EditModel(DissertationThemes.Database.Model.NorthwindContext context)
        {
            _context = context;
        }

        [BindProperty]
        public DissertationThemes.Database.Model.Supervisor Supervisors { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Supervisors = await _context.Supervisors.FirstOrDefaultAsync(m => m.Id == Int32.Parse(id));

            if (Supervisors == null)
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

            _context.Attach(Supervisors).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomersExists(Supervisors.Id.ToString()))
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
