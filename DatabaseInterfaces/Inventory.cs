using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseInterfaces
{
    public class Inventory
    {
        [Key, Column(Order = 0)]
        public int StoreID { get; set; } //part of the composite primary key

        [Key, Column(Order = 1)]
        public string UPC { get; set; } //part of the composite primary key

        public int StockQuantity { get; set; } //maps to StockQuantity in the table

        //navigation properties
        [ForeignKey("StoreID")]
        public Store Store { get; set; }

        [ForeignKey("UPC")]
        public Product Product { get; set; }
    }
}
