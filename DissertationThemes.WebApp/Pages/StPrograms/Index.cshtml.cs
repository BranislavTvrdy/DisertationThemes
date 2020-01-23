using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
//using DissertationThemes.WebApp.Model;
using DissertationThemes.Database.Model;

namespace DissertationThemes.WebApp.Pages.StPrograms
{
    public class IndexModel : PageModel
    {
        private readonly Database.Model.NorthwindContext _context;

        public IndexModel(Database.Model.NorthwindContext context)
        {
            _context = context;
        }

        public IList<Database.Model.StProgram> StPrograms { get;set; }

        public async Task OnGetAsync()
        {
            StPrograms = await _context.StPrograms.ToListAsync();
        }
    }
}
