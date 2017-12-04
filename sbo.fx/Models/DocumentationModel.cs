using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbo.fx.Models
{
    public class DocumentationModel
    {
        public Dictionary<string, string> GetObjectDocumentation()
        {
            var dict = new Dictionary<string, string>();

            foreach (var prop in this.GetType().GetProperties())
            {
                dict.Add(prop.Name, prop.PropertyType.Name);
            }

            return dict;
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
