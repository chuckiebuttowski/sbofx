using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbo.fx.Models
{
    public class oBusinessPartner: DocumentationModel
    {
        public int Series { get; set; }
        public string CardType { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "String length must not be greater than 15")]
        public string CardCode { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "String length must not be greater than 100")]
        public string CardName { get; set; }

        public int GroupCode { get; set; }

        [StringLength(32, ErrorMessage = "String length must not be greater than 32")]
        public string LicTradNum { get; set; }

        [StringLength(20, ErrorMessage = "String length must not be greater than 20")]
        public string Phone1 { get; set; }
        [StringLength(20, ErrorMessage = "String length must not be greater than 20")]
        public string Phone2 { get; set; }
        [StringLength(50, ErrorMessage = "String length must not be greater than 50")]
        public string Cellular { get; set; }
        [StringLength(20, ErrorMessage = "String length must not be greater than 20")]
        public string Fax { get; set; }
        [StringLength(100, ErrorMessage = "String length must not be greater than 100")]
        public string Email { get; set; }
        [StringLength(50, ErrorMessage = "String length must not be greater than 50")]
        public string CntctPerson { get; set; }
        [StringLength(100, ErrorMessage = "String length must not be greater than 100")]
        public string Address { get; set; }
        public string Frozen { get; set; }
        [StringLength(211, ErrorMessage = "String length must not be greater than 211")]
        public string DebPayAcct { get; set; }

        public double Balance { get; set; }

        public override void GetSboModelType()
        {
            SboType = SboTransactionType.BP;
        }
    }
}
