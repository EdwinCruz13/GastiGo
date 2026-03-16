using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Features.Finances.Entities
{
    public class Nature : AuditableEntity
    {
        public Guid NatureId => Id;
        public String Name { get; private set; }
        public String Abbre { get; private set; }


        private Nature() { } // EF

        /// <summary>
        /// create a new nature with the necessary data
        /// </summary>
        /// <param name="name"></param>
        /// <param name="abbre"></param>
        public Nature(String name, String abbre)
        {
            Name = name;
            Abbre = abbre;
        }
    }
}
