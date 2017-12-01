using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbo.fx.Models
{
    public class oInventoryTransaction : DocumentationModel
    {
        public oInventoryTransaction()
        {
            TransactionLines = new List<oInventoryTransactionLine>();
        }

        [Key]
        public int DocNum { get; set; }
        public int DocEntry { get; set; }
        public int Series { get; set; }
        public int PriceList { get; set; }
        public DateTime DocDate { get; set; }
        public DateTime TaxDate { get; set; }
        [StringLength(11, ErrorMessage = "String length must not be greater than 11")]
        public string ReferenceNo { get; set; }
        public InventoryType InventoryTransactionType { get; set; }
        public int BranchId { get; set; }
        [StringLength(254, ErrorMessage = "String length must not be greater than 254")]
        public string Remarks { get; set; }
        [StringLength(50, ErrorMessage = "String length must not be greater than 50")]
        public string JournalRemarks { get; set; }
        public string Canceled { get; set; }

        public List<oInventoryTransactionLine> TransactionLines { get; set; }

    }
}
