using sbo.fx.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbo.fx.Models
{
    [SBOTransactionType("WHS")]
    public class oWarehouse: DocumentationModel
    {
        public string WarehouseCode { get; set; }
        public string WarehouseName { get; set; }
    }
}
