using System;
using System.Collections.Generic;

namespace LIMS.Models
{
    public partial class Shipment
    {
        public int Id { get; set; }
        public int? Supplyerid { get; set; }
        public double? Suppdate { get; set; }
        public sbyte? Deleted { get; set; }

        public virtual Supplyer Supplyer { get; set; }
        public virtual Shipdoc Shipdoc { get; set; }
    }
}
