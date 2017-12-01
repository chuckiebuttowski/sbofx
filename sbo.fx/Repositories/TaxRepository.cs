using sbo.fx.Interfaces;
using sbo.fx.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAPbobsCOM;
using System.Data.SqlClient;
using Dapper;

namespace sbo.fx.Repositories
{
    internal class TaxRepository :BaseRepository, IRepository<oTax>
    {
        public int Add(oTax obj)
        {
            throw new NotImplementedException();
        }

        public int AddMultiple(List<oTax> objs)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public async Task<List<oTax>> GetList(Func<oTax, bool> fltr)
        {
            try
            {
                List<oTax> taxes = new List<oTax>();
                if (SqlObject != null)
                {
                    var _taxes = await SqlObject.QueryAsync<oTax>("sp_getTaxes", param: null, transaction: null, commandTimeout: null, commandType: System.Data.CommandType.StoredProcedure);
                    if (fltr != null)
                    {
                        taxes = _taxes.Distinct().Where(fltr).ToList();
                    }
                    else taxes = _taxes.Distinct().ToList();
                }
                return taxes;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        public int Update(oTax obj)
        {
            throw new NotImplementedException();
        }
    }
}
