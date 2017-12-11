using sbo.fx.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbo.fx.Models
{
    [SBOTransactionTypeAttribute("DSBURSMNT")]
    public class oPayment: DocumentationModel
    {
        public int DocEntry { get; set; }

        [Key]
        public int DocNo { get; set; }
        public int TransId { get; set; }
        public DateTime DocDate { get; set; }
        public DateTime DocDueDate { get; set; }
        public DateTime TaxDate { get; set; }
        public string DocType { get; set; }
        public int Series { get; set; }
        [StringLength(8)]
        public string ReferenceNo { get; set; }
        [StringLength(20)]
        public string WTCode { get; set; }
        public int BranchId { get; set; }
        public string ProjectId { get; set; }

        public bool IsOnAccount { get; set; } = false;
        public double OnAccountSum { get; set; }

        [Required]
        [StringLength(15)]
        public string CardCode { get; set; }
        [StringLength(100)]
        public string CardName { get; set; }

        [StringLength(254)]
        public string Comments { get; set; }
        [StringLength(50)]
        public string JournalMemo { get; set; }

        [StringLength(210)]
        public string CashAccount { get; set; }
        public double CashSum { get; set; }
        [StringLength(210)]
        public string CheckAccount { get; set; }
        public double CheckSum { get; set; }
        [StringLength(210)]
        public string CreditAccount { get; set; }
        public double CreditSum { get; set; }
        [StringLength(210)]
        public string BankTransferAccount { get; set; }
        public double BankTransferSum { get; set; }
        public DateTime BankTransferDate { get; set; }
        [StringLength(27)]
        public string BankTransferReference { get; set; }
        public double WTSum { get; set; }

        public double OpenBalance { get; set; }
        public double TotalAmountDue { get; set; }

        public List<oPaymentLine> PaymentLines { get; set; }
        public List<oCheckPayment> CheckPayments { get; set; }
        public List<oCreditPayment> CreditPayments { get; set; }

    }
}
