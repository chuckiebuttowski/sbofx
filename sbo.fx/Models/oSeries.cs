using sbo.fx.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbo.fx.Models
{
    [SBOTransactionType("SER")]
    public class oSeries: DocumentationModel
    {
        public string ObjectCode { get; set; }
        public int Series { get; set; }
        public string SeriesName { get; set; }
        public string DocSubType { get; set; }
    }
}
