using sbo.fx.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbo.fx.Models
{
    [SBOTransactionType("PRJ")]
    public class oProject: DocumentationModel
    {
        public string ProjectCode { get; set; }
        public string ProjectName { get; set; }
    }
}
