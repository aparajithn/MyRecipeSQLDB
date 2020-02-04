using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TeamProject1.Models
{
    public class Herb
    {
        [ForeignKey("Seasoning")]
        public int Id { get; set; }
        public Seasoning Seasoning { get; set; }
    }
}
