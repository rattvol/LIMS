using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LIMS.Models
{
    public partial class Prices
    {
        public int Id { get; set; }
        [Required]
        public int Supplyerid { get; set; }
        [Required]
        public int Nomenclid { get; set; }
        [Required] 
        [Range (typeof(decimal), "0,01", "1000000,00", ErrorMessage = "В качестве разделителя дробной и целой части используется запятая")]
        public decimal Price { get; set; }

        public virtual Nomencl Nomencl { get; set; }
        public virtual Supplyer Supplyer { get; set; }
    }
}
