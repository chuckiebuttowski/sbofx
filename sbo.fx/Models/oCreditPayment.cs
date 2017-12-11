using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbo.fx.Models
{
    public class oCreditPayment: DocumentationModel
    {
        [Key]
        public int CreditPaymentId { get; set; }
        public int DocNo { get; set; }
        public int CreditCard { get; set; }
        [StringLength(15)]
        public string CreditAccount { get; set; }
        [StringLength(20)]
        public string VoucherNo { get; set; }
        public double CreditSum { get; set; }
        public DateTime FirstDue { get; set; }
        public double FirstPayment { get; set; }
        public string SplitCredit { get; set; }
        public int NumberOfPayments { get; set; }
        
        [ForeignKey("DocNo")]
        public oPayment Payment { get; set; }
    }
}
