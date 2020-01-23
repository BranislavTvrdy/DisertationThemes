using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DissertationThemes.ServiceApp
{
    [DataContract]
    public class StProgramDto
    {
        [DataMember]
        public string FullName { get; set; }
        [DataMember]
        public int Id { get; set; }

        public StProgramDto(string fullName, int id)
        {
            FullName = fullName;
            Id = id;
        }

        public override string ToString()
        {
            return this.FullName;
        }
    }
}
