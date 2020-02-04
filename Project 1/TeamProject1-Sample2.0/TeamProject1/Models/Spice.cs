using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TeamProject1.Models
{
    public class Spice
    {
        [ForeignKey("Seasoning")]
        public int Id { get; set; }
        public Seasoning Seasoning { get; set; }
        [Range(0, 10)]
        public int Hotness { get; set; }
    }
}
