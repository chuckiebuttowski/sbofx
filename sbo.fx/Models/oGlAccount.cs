using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbo.fx.Models
{
    public class oGlAccount : DocumentationModel
    {
        public bool IsSegmented { get; set; }
        [StringLength(15, ErrorMessage = "String length must not be greater than 15")]
        public string AccntCode { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "String length must not be greater than 100")]
        public string AccntName { get; set; }
        public string FormatCode { get; set; }
        [StringLength(20, ErrorMessage = "String length must not be greater than 20")]
        [Required]
        public string Segment_0 { get; set; }
        [StringLength(20, ErrorMessage = "String length must not be greater than 20")]
        [Required]
        public string Segment_1 { get; set; }
        public int BPLId { get; set; }
        public string BPLName { get; set; }
        [StringLength(1, ErrorMessage = "String length must not be greater than 1")]
        [Required]
        public string IsPostable { get; set; }
        [StringLength(1, ErrorMessage = "String length must not be greater than 1")]
        [Required]
        public string IsControlAccount { get; set; }
    }
}
