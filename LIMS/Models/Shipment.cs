using System;
using System.Collections.Generic;

namespace LIMS.Models
{
    public partial class Shipment
    {
        public Shipment()
        {
            Shipdocs = new HashSet<Shipdoc>();
        }
        public int Id { get; set; }
        public int Supplyerid { get; set; }
        public double Suppdate { get; set; }
        public bool Deleted { get; set; }

        public virtual Supplyer Supplyer { get; set; }
        public virtual ICollection<Shipdoc> Shipdocs { get; set; }
    }
}
