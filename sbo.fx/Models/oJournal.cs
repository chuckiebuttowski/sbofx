using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbo.fx.Models
{
    public class oJournal : DocumentationModel
    {
        public oJournal()
        {
            JournalLines = new List<oJournalLine>();
        }

        [Key]
        public int JdtNumber { get; set; }
        public int TransId { get; set; }
        public int Series { get; set; }
        [Required(AllowEmptyStrings = false)]
        public DateTime DocDate { get; set; }
        [Required(AllowEmptyStrings = false)]
        public DateTime DocDueDate { get; set; }
        [Required(AllowEmptyStrings = false)]
        public DateTime TaxDate { get; set; }

        [StringLength(100, ErrorMessage = "String length must not be greater than 100")]
        [Required(AllowEmptyStrings = false)]
        public string Reference1 { get; set; }

        [StringLength(100, ErrorMessage = "String length must not be greater than 100")]
        [Required(AllowEmptyStrings = true)]
        public string Reference2 { get; set; }

        [StringLength(27, ErrorMessage = "String length must not be greater than 27")]
        [Required(AllowEmptyStrings = true)]
        public string Reference3 { get; set; }

        [StringLength(50, ErrorMessage = "String length must not be greater than 50")]
        [Required(AllowEmptyStrings = true)]
        public string JournalMemo { get; set; }
        [Required(AllowEmptyStrings = true)]
        [StringLength(20, ErrorMessage = "String length must not be greater than 20")]
        public string Project { get; set; }

        public List<oJournalLine> JournalLines { get; set; }
    }
}
