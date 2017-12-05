using sbo.fx.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbo.fx.Models
{
    [SBOTransactionType("BPGRP")]
    public class oBusinessPartnerGroup : DocumentationModel
    {
        public int GroupCode { get; set; }
        public string GroupName { get; set; }
        public string GroupType { get; set; }
    }
}
