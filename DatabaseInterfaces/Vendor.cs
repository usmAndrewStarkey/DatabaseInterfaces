using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseInterfaces
{
    public class Vendor
    {
        [Key]
        public int VendorID { get; set; }

        [Required]
        public string VendorName { get; set; }

        public string ContactInfo { get; set; }
    }
}
