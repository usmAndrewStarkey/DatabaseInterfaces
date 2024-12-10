using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DatabaseInterfaces
{
    public class Transaction
    {
        [Key]
        public int TransactionID { get; set; }
        public int CustomerID { get; set; }
        public int StoreID { get; set; }
        public DateTime Timestamp { get; set; }

        //navigation property for the customer
        public Customer Customer { get; set; }

        //navigation property for the store
        public Store Store { get; set; }

        //navigation property for the transaction items
        public List<TransactionItem> TransactionItems { get; set; }
    }
}
