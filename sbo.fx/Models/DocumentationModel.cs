using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbo.fx.Models
{
    public class DocumentationModel
    {
        public IEnumerable<object> GetObjectDocumentation()
        {
            var list = new List<object>();

            foreach (var prop in this.GetType().GetProperties())
            {
                var requiredAttr = prop.GetCustomAttributes(typeof(RequiredAttribute), false).FirstOrDefault() as RequiredAttribute;
                var lengthAttr = prop.GetCustomAttributes(typeof(StringLengthAttribute), false).FirstOrDefault() as StringLengthAttribute;
                list.Add(new { FieldName = prop.Name, Type = prop.PropertyType.Name, IsRequired = requiredAttr != null, FieldLength = lengthAttr?.MaximumLength });
            }

            return list;
        }

        public virtual void GetSboModelType()
        {

        }

        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public SboTransactionType SboType { get; set; }
    }

}
