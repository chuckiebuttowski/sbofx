using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbo.fx.Models
{
    public class oItemSerial
    {
        [StringLength(50)]
        public string ItemCode { get; set; }

        [StringLength(254)]
        public string SerialNo { get; set; }

        [ForeignKey("ItemCode")]
        public oItem Item { get; set; }
    }
}
