using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityModel
{
    public enum PORT_STATUS { READY, CHECKED_OUT, UNDER_MAINTENANCE};
    public class DockingPort
    {
        public int DockingPortID {get;set;}

        public Nullable<int> DockingUnitID { get; set; }
        public virtual DockingUnit DockingUnit { get; set; }

        public PORT_STATUS PortStatus { get; set; }

        public Nullable<int> CycleID { get; set; }
        public virtual Cycle Cycle { get; set; }
    }
}
