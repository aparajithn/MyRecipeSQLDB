using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TeamProject1.Models
{
    public class Ingredients_All
    {
        [ForeignKey("Ingredient")]
        public Meat M { get; set; }
        public Vegetable V { get; set; }
        public Grain G { get; set; }
        public Ingredient Ingredient { get; set; }
    }
}
