using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace EntityModel
{
    public class Cycle
    {
        public int CycleID { get; set; }

        [Index(IsUnique=true)]
        public int No { get; set; }

        [MaxLength(32)]
         [Index(IsUnique = true)]
        public String Tag { get; set; }
    }
}
