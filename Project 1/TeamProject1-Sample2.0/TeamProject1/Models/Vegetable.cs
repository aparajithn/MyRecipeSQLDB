using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TeamProject1.Models
{
    public class Vegetable
    {
        [ForeignKey("Ingredient")]
        public int Id { get; set; }
        public decimal Fiber { get; set; }
        public Ingredient Ingredient { get; set; }
    }
}
