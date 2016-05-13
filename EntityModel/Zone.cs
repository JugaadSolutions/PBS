using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityModel
{
    public class Zone
    {
        public int ZoneID { get; set; }
        public String Name { get; set; }

        public Nullable<int> CityID { get; set; }
        public virtual City City { get; set; }

        public virtual ICollection<DockingStation> DockingStations { get; set; }
    }
}
