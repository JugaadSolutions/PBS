using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace EntityModel
{
    public enum TRANSACTION_STATUS_INDEX { CHECK_OUT_OPEN = 1, CHECK_IN_CLOSED, CHECK_IN_OPEN , CHECK_OUT_CLOSED};
    public class Transaction
    {
        public int TransactionID { get; set; }
        public TransactionStep CheckIn { get; set; }
        public TransactionStep CheckOut { get; set; }
        public String Status { get; set; }

        public DateTime Created { get; set; }

        public Transaction()
        {
            Created = DateTime.Now;
        }

    }
}
