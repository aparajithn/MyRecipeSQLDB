using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TeamProject1.Models
{
    public class Grain
    {
        [ForeignKey("Ingredient")]
        public int Id { get; set; }
        public decimal Carbohydrate { get; set; }
        public Ingredient Ingredient { get; set; }
    }
}
