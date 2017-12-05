using sbo.fx.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbo.fx.Models
{
    [SBOTransactionType("BRNCH")]
    public class oBranch: DocumentationModel
    {
        public int BranchCode { get; set; }
        public string BranchName { get; set; }
    }
}
