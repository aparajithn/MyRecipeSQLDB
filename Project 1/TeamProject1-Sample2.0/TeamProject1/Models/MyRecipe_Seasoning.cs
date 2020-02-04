using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamProject1.Models
{
    public class MyRecipe_Seasoning
    {
        public int Id { get; set; }
        [ForeignKey("MyRecipe")]
        [Display(Name = "Recipe")]
        public int R_id { get; set; }
        public decimal Weight { get; set; }
        [ForeignKey("Seasoning")]
        [Display(Name = "Seasoning")]
        public int S_id { get; set; }

        public Seasoning Seasoning { get; set; }
        [Display(Name = "My Recipe")]
        public MyRecipe MyRecipe { get; set; }
    }
}
