using sbo.fx.Interfaces;
using sbo.fx.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace sbo.fx.Repositories
{
    internal class WarehouseRepository : BaseRepository, IRepository<oWarehouse>
    {
        public int Add(oWarehouse obj)
        {
            throw new NotImplementedException();
        }

        public int AddMultiple(List<oWarehouse> objs)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public async Task<List<oWarehouse>> GetList(Func<oWarehouse, bool> fltr)
        {
            try
            {
                List<oWarehouse> wareHouses = new List<oWarehouse>();
                if (SqlObject != null)
                {
                    var _warehouses = await SqlObject.QueryAsync<oWarehouse>("sp_getWarehouses", param: null, transaction: null, commandTimeout: null, commandType: System.Data.CommandType.StoredProcedure);
                    if (fltr != null)
                    {
                        wareHouses = _warehouses.Distinct().Where(fltr).ToList();
                    }
                    else wareHouses = _warehouses.Distinct().ToList();
                }
                return wareHouses;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int Update(oWarehouse obj)
        {
            throw new NotImplementedException();
        }
    }
}
