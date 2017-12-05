using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbo.fx.Attributes
{
    public class SBOTransactionTypeAttribute : Attribute
    {
        public string SBOType { get; set; }
        public string Description { get; set; }
        public Boolean IsDetail { get; set; }

        public SBOTransactionTypeAttribute(string type, string description = "", bool isDetail = false)
        {
            SBOType = type;
            Description = description;
            isDetail = false;
        }
    }
}
