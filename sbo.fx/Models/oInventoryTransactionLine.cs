using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbo.fx.Models
{
    public class oInventoryTransactionLine : DocumentationModel
    {
        public int DocEntry { get; set; }
        public int LineNo { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "String length must not be greater than 15")]
        public string ItemCode { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "String length must not be greater than 15")]
        public string GLAccountCode { get; set; }

        public int Quantity { get; set; }
        public double Price { get; set; }
        [Required]
        [StringLength(11, ErrorMessage = "String length must not be greater than 11")]
        public string WarehouseId { get; set; }
        public double LineTotal { get; set; }

        [ForeignKey("DocEntry")]
        public oInventoryTransaction InventoryTransaction { get; set; }
    }
}
