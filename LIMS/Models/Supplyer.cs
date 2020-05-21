using System;
using System.Collections.Generic;

namespace LIMS.Models
{
    public partial class Supplyer
    {
        public Supplyer()
        {
            Prices = new HashSet<Prices>();
            Shipment = new HashSet<Shipment>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public sbyte? Deleted { get; set; }

        public virtual ICollection<Prices> Prices { get; set; }
        public virtual ICollection<Shipment> Shipment { get; set; }
    }
}
