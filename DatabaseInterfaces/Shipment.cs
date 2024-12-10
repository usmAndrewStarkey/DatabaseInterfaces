using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseInterfaces
{
    public class Shipment
    {
        public int ShipmentID { get; set; }
        public string VendorName { get; set; }
        public DateTime ArrivalDate { get; set; }
        public string ProductUPC { get; set; }
        public int QuantityReceived { get; set; }
    }
}
