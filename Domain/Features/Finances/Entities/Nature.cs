using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Features.Finances.Entities
{
    public class Nature
    {
        public int NatureID { get; private set; }
        public String Name { get; private set; }
        public String Abbre { get; private set; }


        private Nature() { } // EF

        /// <summary>
        /// create a new nature with the necessary data
        /// </summary>
        /// <param name="name"></param>
        /// <param name="abbre"></param>
        public Nature(Int32 id, String name, String abbre)
        {
            NatureID = id;
            Name = name;
            Abbre = abbre;
        }
    }
}
