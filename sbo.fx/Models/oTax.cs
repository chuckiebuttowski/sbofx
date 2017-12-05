using sbo.fx.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbo.fx.Models
{
    [SBOTransactionType("TAX")]
    public class oTax: DocumentationModel
    {
        public string TaxCode { get; set; }
        public string TaxName { get; set; }
        public decimal Rate { get; set; }
        public string Inactive { get; set; }
        public string Category { get; set; }
        public string AccountCode { get; set; }
    }
}
