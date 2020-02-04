using System.ComponentModel.DataAnnotations.Schema;

namespace TeamProject1.Models
{
    public class Meat
    {
        [ForeignKey("Ingredient")]
        public int Id { get; set; }
        public decimal Protein { get; set; }
        public Ingredient Ingredient { get; set; }
    }
}
