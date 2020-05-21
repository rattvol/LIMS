using System;
using System.Collections.Generic;

namespace LIMS.Models
{
    public partial class Prices
    {
        public int Id { get; set; }
        public int? Supplyerid { get; set; }
        public int? Nomenclid { get; set; }
        public float? Price { get; set; }

        public virtual Nomencl Nomencl { get; set; }
        public virtual Supplyer Supplyer { get; set; }
    }
}
