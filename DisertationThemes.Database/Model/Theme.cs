using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DissertationThemes.Database.Model
{
    public enum ResearchType
    {
        BasicResearch,
        AppliedResearch,
        AppliedResearchExpDevelopment
    }
    public class Theme
    {
        /// <summary>
        /// Datum vytvorenia temy
        /// </summary>
        public DateTime Created { get; set; }
        /// <summary>
        /// Popis temy
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Id temy
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Externe studium
        /// </summary>
        public bool IsExternalStudy { get; set; }
        /// <summary>
        /// Dene studium
        /// </summary>
        public bool IsFullTimeStudy { get; set; }
        /// <summary>
        /// Nazov temy
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Typ vyskumu
        /// </summary>
        public ResearchType? ResearchType { get; set; }
        /// <summary>
        /// Studijny program
        /// </summary>
        public StProgram StProgram { get; set; }
        /// <summary>
        /// Id studijneho programu (cudzi kluc)
        /// </summary>
        public int StProgramId { get; set; }
        /// <summary>
        /// Skolitel prace
        /// </summary>
        public Supervisor Supervisor { get; set; }
        /// <summary>
        /// Id skolitela prace (chudzi kluc)
        /// </summary>
        public int SupervisorId { get; set; }

        public Theme()
        {
        }

        public Theme(DateTime created, string description, int id, bool isExternalStudy, bool isFullTimeStudy,
            string name, ResearchType researchType, StProgram stProgram, int stProgramId, Supervisor supervisor,
            int supervisorId)
        {
            Created = created;
            Description = description;
            Id = id;
            IsExternalStudy = isExternalStudy;
            IsFullTimeStudy = isFullTimeStudy;
            Name = name;
            ResearchType = researchType;
            StProgram = stProgram;
            StProgramId = stProgramId;
            Supervisor = supervisor;
            SupervisorId = supervisorId;
        }



    }
}
