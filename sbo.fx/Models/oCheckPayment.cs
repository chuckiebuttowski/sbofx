using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbo.fx.Models
{
    public class oCheckPayment: DocumentationModel
    {
        [Key]
        public int CheckPaymentId { get; set; }
        public int DocNo { get; set; }
        public int LineNo { get; set; }
        public DateTime DueDate { get; set; }
        public int CheckNum { get; set; }
        [StringLength(30)]
        public string BankCode { get; set; }
        [StringLength(50)]
        public string AccountNo { get; set; }
        public string IsTransferable { get; set; } = "N";
        public double CheckSum { get; set; }
        [StringLength(15)]
        public string CheckAccount { get; set; }
        public string IsManualCheck { get; set; } = "Y";

        [ForeignKey("DocNo")]
        public oPayment Payment { get; set; }
    }
}
