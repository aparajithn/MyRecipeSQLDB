using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TeamProject1.Models
{
    public class Seasoning_All
    {
        [ForeignKey("Ingredient")]
        public Herb H { get; set; }
        public Spice S { get; set; }
        public Seasoning Seasoning { get; set; }
    }
}
