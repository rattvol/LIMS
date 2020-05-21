using System;
using System.Collections.Generic;

namespace LIMS.Models
{
    public partial class Groupnom
    {
        public Groupnom()
        {
            Nomencl = new HashSet<Nomencl>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public sbyte? Deleted { get; set; }

        public virtual ICollection<Nomencl> Nomencl { get; set; }
    }
}
