using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LIMS.Models
{
    public partial class Shipdoc
    {
        public int id { get; set; }
        [Required]
        public int Shipmentid { get; set; }
        [Required]
        public int Nomenclid { get; set; }
        public decimal Price { get; set; }
        [Required]
        public int Quantity { get; set; }

        public virtual Nomencl Nomencl { get; set; }
        public virtual Shipment Shipment { get; set; }
    }
}
