using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DissertationThemes.Database.Model;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;


namespace DissertationThemes.WebApp.Pages
{
    public class ImportData : PageModel
    {
        private readonly DissertationThemes.Database.Model.NorthwindContext _context;

        public string Data { get; set; }

        public ImportData(DissertationThemes.Database.Model.NorthwindContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public DissertationThemes.Database.Model.Theme Themes { get; set; }

        public async Task<IActionResult> OnPostUploadFile(IFormFile fileName)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            #region ClearDbCommands

            _context.Database.ExecuteSqlCommand("DELETE FROM Theme");
            await _context.SaveChangesAsync();

            _context.Database.ExecuteSqlCommand("DELETE FROM Supervisor");
            await _context.SaveChangesAsync();

            _context.Database.ExecuteSqlCommand("DELETE FROM StProgram");
            await _context.SaveChangesAsync();

            #endregion

            StreamReader reader = new StreamReader(fileName.OpenReadStream());
            reader.ReadLine();
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (!string.IsNullOrWhiteSpace(line))
                {
                    var values = line.Split(';');

                    if (_context.Supervisors.Count(s => s.FullName == values[1]) == 0)
                    {
                        var inserting = new Supervisor();
                        inserting.FullName = values[1];
                        _context.Supervisors.Add(inserting);
                        await _context.SaveChangesAsync();
                    }
                    if (_context.StPrograms.Where(s => s.Name == values[2]).Count(s => s.FieldOfStudy == values[3]) == 0)
                    {
                        var inserting = new StProgram();
                        inserting.Name = values[2];
                        inserting.FieldOfStudy = values[3];
                        _context.StPrograms.Add(inserting);
                        await _context.SaveChangesAsync();
                    }
                    if (_context.Themes.Where(t => t.Name == values[0])
                            .Where(t => t.StProgramId == _context.StPrograms.FirstOrDefault(s => s.Name == values[2]).Id)
                            .Count(t => t.StProgramId == _context.StPrograms.FirstOrDefault(s => s.FieldOfStudy == values[3]).Id) == 0)
                    {
                        var inserting = new Theme();
                        inserting.Name = values[0];
                        var sup = _context.Supervisors.First(s => s.FullName == values[1]);
                        inserting.SupervisorId = sup.Id;
                        inserting.Supervisor = sup;
                        var stu = _context.StPrograms
                            .Where(s => s.Name == values[2]).First(s => s.FieldOfStudy == values[3]);
                        inserting.StProgramId = stu.Id;
                        inserting.StProgram = stu;
                        inserting.IsFullTimeStudy = Boolean.Parse(values[4]);
                        inserting.IsExternalStudy = Boolean.Parse(values[5]);
                        ResearchType? resType = null;
                        switch (values[6].ToLower())
                        {
                            case "základný výskum":
                                resType = ResearchType.BasicResearch;
                                break;
                            case "aplikovaný výskum":
                                resType = ResearchType.AppliedResearch;
                                break;
                            case "aplikovaný výskum a experimentálny vývoj":
                                resType = ResearchType.AppliedResearchExpDevelopment;
                                break;
                            case null:
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                        inserting.ResearchType = resType;
                        string desc = values[7];
                        desc = desc.Replace("<br>", Environment.NewLine);
                        inserting.Description = desc;
                        inserting.Created = DateTime.Parse(values[8]);
                        _context.Themes.Add(inserting);
                        await _context.SaveChangesAsync();
                    }
                }
            }
            reader.Close();

            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Database.ExecuteSqlCommand("DELETE FROM Theme");
            await _context.SaveChangesAsync();

            _context.Database.ExecuteSqlCommand("DELETE FROM Supervisor");
            await _context.SaveChangesAsync();

            _context.Database.ExecuteSqlCommand("DELETE FROM StProgram");
            await _context.SaveChangesAsync();

            var reader = new StreamReader("..\\Data\\phd_temy.csv");
            reader.ReadLine();
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (!string.IsNullOrWhiteSpace(line))
                {
                    var values = line.Split(';');

                    if (_context.Supervisors.Count(s => s.FullName == values[1]) == 0)
                    {
                        var inserting = new Supervisor();
                        inserting.FullName = values[1];
                        _context.Supervisors.Add(inserting);
                        await _context.SaveChangesAsync();
                    }
                    if (_context.StPrograms.Where(s => s.Name == values[2]).Count(s => s.FieldOfStudy == values[3]) == 0)
                    {
                        var inserting = new StProgram();
                        inserting.Name = values[2];
                        inserting.FieldOfStudy = values[3];
                        _context.StPrograms.Add(inserting);
                        await _context.SaveChangesAsync();
                    }
                    if (_context.Themes.Count(s => s.Name == values[0]) == 0)
                    {
                        var inserting = new Theme();
                        inserting.Name = values[0];
                        var sup = _context.Supervisors.First(s => s.FullName == values[1]);
                        inserting.SupervisorId = sup.Id;
                        inserting.Supervisor = sup;
                        var stu = _context.StPrograms
                            .Where(s => s.Name == values[2]).First(s => s.FieldOfStudy == values[3]);
                        inserting.StProgramId = stu.Id;
                        inserting.StProgram = stu;
                        inserting.IsFullTimeStudy = Boolean.Parse(values[4]);
                        inserting.IsExternalStudy = Boolean.Parse(values[5]);
                        ResearchType? resType = null;
                        switch (values[6].ToLower())
                        {
                            case "základný výskum":
                                resType = ResearchType.BasicResearch;
                                break;
                            case "aplikovaný výskum":
                                resType = ResearchType.AppliedResearch;
                                break;
                            case "aplikovaný výskum a experimentálny vývoj":
                                resType = ResearchType.AppliedResearchExpDevelopment;
                                break;
                            case null:
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                        inserting.ResearchType = resType;
                        string desc = values[7];
                        desc = desc.Replace("<br>", Environment.NewLine);
                        inserting.Description = desc;
                        inserting.Created = DateTime.Parse(values[8]);
                        _context.Themes.Add(inserting);
                        await _context.SaveChangesAsync();
                    }
                }
            }
            reader.Close();
/*

            foreach (var supervisor in _context.Supervisors)
            {
                var range = _context.Themes.Where(t => t.SupervisorId == supervisor.Id).Except(supervisor.Themes);
                foreach (var theme in range)
                {
                    supervisor.Themes.Add(theme);
                }
            }
            foreach (var program in _context.StPrograms)
            {
                var range = _context.Themes.Where(t => t.StProgramId == program.Id).Except(program.Themes);
                foreach (var theme in range)
                {
                    program.Themes.Add(theme);
                }
            }
*/

            return RedirectToPage("./Index");
        }


    }
}