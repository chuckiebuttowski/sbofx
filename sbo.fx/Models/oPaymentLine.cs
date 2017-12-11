using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbo.fx.Models
{
    public class oPaymentLine: DocumentationModel
    {
        [Key]
        public int PaymentLineId { get; set; }
        public int DocNo { get; set; }
        public int LineNo { get; set; }
        public int InvoiceNo { get; set; }
        [StringLength(50)]
        public string AccountCode { get; set; }
        [StringLength(250)]
        public string Description { get; set; }
        public string VATGroup { get; set; }
        public double VATPercent { get; set; }
        public double VATAmount { get; set; }
        public double SumApplied { get; set; }
        public double GrossAmount { get; set; }
        public int InvoiceType { get; set; }

        [ForeignKey("DocNo")]
        public oPayment Payment { get; set; }
    }
}
