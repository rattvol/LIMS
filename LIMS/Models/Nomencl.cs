using System;
using System.Collections.Generic;

namespace LIMS.Models
{
    public partial class Nomencl
    {
        public Nomencl()
        {
            Prices = new HashSet<Prices>();
            Shipdoc = new HashSet<Shipdoc>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? Groupnomid { get; set; }
        public sbyte? Deleted { get; set; }

        public virtual Groupnom Groupnom { get; set; }
        public virtual ICollection<Prices> Prices { get; set; }
        public virtual ICollection<Shipdoc> Shipdoc { get; set; }
    }
}
