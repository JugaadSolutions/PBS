using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityModel
{
    public class DockingStation
    {
        public int DockingStationID { get; set; }

        public String Name { get; set; }

        public Nullable<int> ZoneID { get; set; }
        public virtual Zone Zone { get; set; }

        public virtual ICollection<DockingUnit> DockingUnits { get; set; }

    }
}
