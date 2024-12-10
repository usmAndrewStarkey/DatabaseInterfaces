using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseInterfaces
{
    public class TransactionItem
    {
        [Key]
        public int TransactionID { get; set; }
        [Key]
        public string UPC { get; set; }
        public int Quantity { get; set; }
        public decimal PriceAtPurchase { get; set; }

        //navigation property for the transaction
        public Transaction Transaction { get; set; }

        //navigation property for the product
        public Product Product { get; set; }
    }
}

