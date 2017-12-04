using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbo.fx.Models
{
    public class oItem : DocumentationModel
    {
        public int Series { get; set; }

        [Key]
        [StringLength(50, ErrorMessage = "String length must not be greater than 50")]
        [Required(AllowEmptyStrings = false)]
        public string ItemCode { get; set; }

        [StringLength(254)]
        [Required(AllowEmptyStrings = true)]
        public string Barcode { get; set; }

        [StringLength(100, ErrorMessage = "String length must not be greater than 100")]
        [Required(AllowEmptyStrings = true)]
        public string Description { get; set; }

        public int ItemGroup { get; set; }
        public int UoMGroup { get; set; }
        public double ItemCost { get; set; }
        public double SellPrice { get; set; }

        [StringLength(2, ErrorMessage = "String length must not be greater than 2")]
        public string InventoryItem { get; set; }
        [StringLength(2, ErrorMessage = "String length must not be greater than 2")]
        public string SalesItem { get; set; }
        [StringLength(2, ErrorMessage = "String length must not be greater than 2")]
        public string PurchaseItem { get; set; }

        public override void GetSboModelType()
        {
            SboType = SboTransactionType.ITM;
        }
    }
}
