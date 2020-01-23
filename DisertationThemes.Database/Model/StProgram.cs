using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DissertationThemes.Database.Model
{
    public class StProgram
    {
        /// <summary>
        /// Náazov studijneho odboru
        /// </summary>
        public string FieldOfStudy { get; set; }
        /// <summary>
        /// Id studijneho programu
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nazov studijneho programu
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Zoznam tem studijneho programu
        /// </summary>
        public List<Theme> Themes { get; set; }

        public StProgram()
        {
            this.Themes = new List<Theme>();
        }

        public StProgram(string fieldOfStudy, int id, string name, List<Theme> themes)
        {
            FieldOfStudy = fieldOfStudy;
            Id = id;
            Name = name;
            Themes = themes;
        }



    }
}
