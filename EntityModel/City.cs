using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityModel
{
    public class City
    {
        public int CityID { get; set; }
        public String Name {get;set;}

        public virtual ICollection<Zone> Zones { get; set; }
    }
}
