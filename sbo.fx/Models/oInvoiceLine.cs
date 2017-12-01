using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbo.fx.Models
{
    public class oInvoiceLine
    {
        public int DocEntry{ get; set; }
        public int LineNo { get; set; }

        [StringLength(15)]
        public string ItemCode { get; set; }

        [StringLength(15)]
        public string GLAccountCode { get; set; }

        public int Quantity { get; set; }
        public double Price { get; set; }
        public string TaxCode { get; set; }
        public double TaxPercent { get; set; }
        public double TaxSum { get; set; }
        public double LineTotal { get; set; }

        [ForeignKey("DocEntry")]
        public oInvoice InvoiceHdr { get; set; }
    }
}
