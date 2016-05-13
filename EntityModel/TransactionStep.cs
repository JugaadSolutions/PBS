using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace EntityModel
{
    public enum TRANSACTION_TYPE_INDEX { CHECK_OUT = 1, CHECK_IN }
    public class TransactionStep
    {

       

        public int TransactionStepID { get; set; }

        public DateTime Timestamp { get; set; }

        public String UserTag { get; set; }
        public String AssetTag { get; set; }

        public String Type { get; set; }

        public Nullable<int> DockingUnitID { get; set; }
        public virtual DockingUnit DockingUnit { get; set; }

        public int DockingPort { get; set; }




    }
}
