using System;
using System.Collections.Generic;

namespace LIMS.Models
{
    public partial class Shipdoc
    {
        public int Shipmentid { get; set; }
        public int? Nomenclid { get; set; }
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }

        public virtual Nomencl Nomencl { get; set; }
        public virtual Shipment Shipment { get; set; }
    }
}
