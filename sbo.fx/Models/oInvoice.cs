using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbo.fx.Models
{
    public class oInvoice
    {
        public oInvoice()
        {
            InvoiceLines = new List<oInvoiceLine>();
        }

        public int DocEntry { get; set; }

        [Key]
        public int DocNo { get; set; }
        public int TransId { get; set; }
        public DateTime DocDate { get; set; }
        public DateTime DocDueDate { get; set; }
        public DateTime TaxDate { get; set; }
        public InvoiceType InvoiceTransactionType { get; set; }

        [StringLength(15)]
        public string CardCode { get; set; }

        [StringLength(254)]
        public string Comments { get; set; }

        public double DiscountPercent { get; set; }
        public double DiscountAmount { get; set; }
        public double VatSum { get; set; }
        public string VatGroup { get; set; }
        public double WTSum { get; set; }

        public double TotalBeforeDiscount { get; set; }
        public double DocTotal { get; set; }
        public double Balance { get; set; }
        public double AmountPaid { get; set; }
        public int ReceiptNum { get; set; } = 0;

        public List<oInvoiceLine> InvoiceLines { get; set; }

    }
}
