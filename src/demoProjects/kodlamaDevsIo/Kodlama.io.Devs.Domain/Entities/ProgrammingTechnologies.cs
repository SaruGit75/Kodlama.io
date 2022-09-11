using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Persistence.Repositories;

namespace Kodlama.io.Devs.Domain.Entities
{
    public class ProgrammingTechnology : Entity
    {
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }


        public int ProgrammingLanguageId { get; set; }
        public virtual ProgrammingLanguage? ProgrammingLanguage { get; set; }

        public ProgrammingTechnology()
        {
        }

        public ProgrammingTechnology(int id, string name, int programmingLanguageId) : this()
        {
            Id = id;
            Name = name;
            ProgrammingLanguageId = programmingLanguageId;
        }
    }
}
