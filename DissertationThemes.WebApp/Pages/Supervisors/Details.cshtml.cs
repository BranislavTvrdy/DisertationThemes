using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DissertationThemes.Database.Model;

namespace DissertationThemes.WebApp.Pages.Supervisors
{
    public class DetailsModel : PageModel
    {
        private readonly DissertationThemes.Database.Model.NorthwindContext _context;

        public DetailsModel(DissertationThemes.Database.Model.NorthwindContext context)
        {
            _context = context;
        }

        public DissertationThemes.Database.Model.Supervisor Supervisors { get; set; }
        public IList<Database.Model.Theme> Themes { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Supervisors = await _context.Supervisors.FirstOrDefaultAsync(m => m.Id == Int32.Parse(id));
            Themes = await _context.Themes.Where(t => t.SupervisorId == Int32.Parse(id)).ToListAsync();

            if (Supervisors == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
