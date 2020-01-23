using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using DissertationThemes.Database.Model;
using Xceed.Words.NET;
using DissertationThemes.Database.Model;
using Microsoft.EntityFrameworkCore;

namespace DissertationThemes.ServiceApp
{
    public class DissertationThemesService : IDissertationThemesService
    {
        private readonly WcfDbContext _context;
        public List<Theme> Temy { get; set; }

        public DissertationThemesService()
        {
            _context = new WcfDbContext();
            Temy = _context.Themes.ToList();
        }

        public byte[] GenerateDocx(int themeId)
        {
            var theme = _context.GetThemes().FirstOrDefault(t => t.Id == themeId);
            var supervisor = _context.Supervisors.FirstOrDefault(s => s.Id == theme.SupervisorId);
            var stProgram = _context.StPrograms.FirstOrDefault(s => s.Id == theme.StProgramId);

            DirectoryInfo di = new DirectoryInfo("..\\..\\..\\Data\\Word");
            DocX template = DocX.Load(di.FullName + "\\PhD_temy_sablona.docx");
            byte[] ret = null;
            string fileNamePath = di.FullName + "\\Result" + di.GetFiles().Length + ".docx";
            using (DocX newFile = DocX.Create(fileNamePath))
            {
                newFile.InsertDocument(template);
                newFile.ReplaceText("#=ThemeName=#", theme.Name);
                newFile.ReplaceText("#=Supervisor=#", supervisor.FullName);
                newFile.ReplaceText("#=StProgram=#", stProgram.Name);
                newFile.ReplaceText("#=FieldOfStudy=#", stProgram.FieldOfStudy);
                string resType = "";
                switch (theme.ResearchType)
                {
                    case ResearchType.BasicResearch:
                        resType = "základný výskum";
                        break;
                    case ResearchType.AppliedResearch:
                        resType = "aplikovaný výskum";
                        break;
                    case ResearchType.AppliedResearchExpDevelopment:
                        resType = "aplikovaný výskum a experimentálny vývoj";
                        break;
                    case null:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                newFile.ReplaceText("#=ResearchType=#", resType);
                newFile.ReplaceText("#=Description=#", theme.Description);
                ret = Encoding.ASCII.GetBytes(newFile.ToString());
                newFile.Save();
            }
            Process.Start(fileNamePath);
            return ret;
        }

        public StProgramDto[] GetStudyPrograms()
        {
            var tList = _context.StPrograms.ToList();
            var tSelection = tList.Select(p => new StProgramDto((p.Name + " (" + p.FieldOfStudy +")"), p.Id)).OrderBy(p => p.FullName);
            var ret = tSelection.ToArray();
//            StProgramDto[] ret = _context.StPrograms
//                .Select(p => new StProgramDto(p.Name, p.Id))
//                .OrderBy(p => p.FullName)
//                .ToArray();
            return ret;
        }

        public ThemeDto[] GetThemes(int year, int stProgramId)
        {
//            var ret2 = Temy.Where(t => t.Created.Year == year)
//                .Where(t => t.StProgramId == stProgramId)
//                .Select(t => new ThemeDto(t.Id, t.Name, _context.Supervisors.FirstOrDefault(m => m.Id == t.SupervisorId).FullName))
//                .OrderBy(t => t.Supervisor).ThenBy(t => t.Name).ToArray();
//            return ret2;

            var tFiltered = _context.Themes
                .Where(t => t.Created.Year == year)
                .Where(t => t.StProgramId == stProgramId).ToList();
            var tTransformed = tFiltered.Select(t => new ThemeDto(t.Id, t.Name, _context.Supervisors.FirstOrDefault(m => m.Id == t.SupervisorId).FullName));
            var tOrdered = tTransformed.OrderBy(t => t.Supervisor).ThenBy(t => t.Name);
            ThemeDto[] ret = tOrdered.ToArray();

//            ThemeDto[] ret = _context.Themes
//                .Where(t => t.Created.Year == year)
//                .Where(t => t.StProgramId == stProgramId)
//                .Select(t => new ThemeDto(t.Id,t.Name,
//                    //t.SupervisorId.ToString()
//                    _context.Supervisors.FirstOrDefault(m => m.Id == t.SupervisorId).FullName
//                    ))
//                .OrderBy(t => t.Supervisor)
//                .ThenBy(t => t.Name)
//                .ToArray();

            return ret;
        }


        public int[] GetThemeYears()
        {
            return _context.Themes.Select(t => t.Created.Year).Distinct().OrderBy(y => y).ToArray();
        }
    }
}
