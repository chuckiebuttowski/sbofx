using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbo.fx.Models
{
    public class oJournalLine : DocumentationModel
    {
        public int JournalLinesId { get; set; }

        public int LineID { get; set; }

        [StringLength(2, ErrorMessage = "String length must not be greater than 2")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Line type is required")]
        public string LineType { get; set; }

        public int Segment { get; set; }

        [StringLength(50, ErrorMessage = "String length must not be greater than 50")]
        [Required(AllowEmptyStrings = true)]
        public string LineMemo { get; set; }

        [StringLength(15, ErrorMessage = "String length must not be greater than 15")]
        [Required(ErrorMessage = "GL Account is required", AllowEmptyStrings = false)]
        public string GLCode { get; set; }

        public double Debit { get; set; }
        public double Credit { get; set; }

        [ForeignKey("TransId")]
        public oJournal Journal { get; set; }
    }
}
