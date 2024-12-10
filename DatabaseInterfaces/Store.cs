using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseInterfaces
{
    public class Store
    {
        [Key]
        public int StoreID { get; set; }

        [Required]
        public string Location { get; set; }

        public string Hours { get; set; }

        [Required]
        public string Type { get; set; }
    }
}
