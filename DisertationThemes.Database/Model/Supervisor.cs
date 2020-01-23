using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DissertationThemes.Database.Model
{
    public class Supervisor
    {
        /// <summary>
        /// Cele meno veduceho prace spolu s titulom
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// Id veduceho prace
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// List tem ktorych je veduci
        /// </summary>
        public List<Theme> Themes { get; set; }

        public Supervisor()
        {
            this.Themes = new List<Theme>();
        }

        public Supervisor(string fullName, int id, List<Theme> themes)
        {
            FullName = fullName;
            Id = id;
            Themes = themes;
        }



    }
}
