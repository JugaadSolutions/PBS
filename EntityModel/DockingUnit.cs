using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace EntityModel
{
    public class DockingUnit
    {
        public int DockingUnitID { get; set; }

        [Index(IsUnique = true)]
        public int No { get; set; }


        public Nullable<int> DockingStationID { get; set; }
        public virtual DockingStation DockingStation { get; set; }

        public virtual ICollection<DockingPort> Ports { get; set; }

        public String ToString()
        {
            return "City:" + DockingStation.Zone.City.Name + "-" +
                    "Zone:" + DockingStation.Zone.Name + "-" +
                    "Stn:" + DockingStation.Name + "-";
        }
            
    }
}
