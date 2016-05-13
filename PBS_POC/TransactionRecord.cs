using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBS_POC
{
    public class TransactionRecord
    {

        
        public String CheckOutBy { get; set; }
        public String CheckOutof { get; set; }
        public String CheckOutOn { get; set; }
        public String CheckOutFrom { get; set; }


        public String CheckInof { get; set; }
        public String CheckInOn { get; set; }
        public String CheckInAt { get; set; }
    }
}
