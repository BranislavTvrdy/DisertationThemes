using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace DissertationThemes.ServiceApp
{
    [DataContract]
    public class ThemeDto
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Supervisor { get; set; }

        public ThemeDto(int id, string name, string supervisor)
        {
            Id = id;
            Name = name;
            Supervisor = supervisor;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
