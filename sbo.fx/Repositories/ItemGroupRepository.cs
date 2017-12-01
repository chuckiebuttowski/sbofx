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
    internal class ItemGroupRepository : BaseRepository, IRepository<oItemGroup>
    {
        public int Add(oItemGroup obj)
        {
            throw new NotImplementedException();
        }

        public int AddMultiple(List<oItemGroup> objs)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public async Task<List<oItemGroup>> GetList(Func<oItemGroup, bool> fltr)
        {
            try
            {
                List<oItemGroup> itemGroups = new List<oItemGroup>();
                if (SqlObject != null)
                {
                    var _itemGroups = await SqlObject.QueryAsync<oItemGroup>("sp_getItemGroups", param: null, transaction: null, commandTimeout: null, commandType: System.Data.CommandType.StoredProcedure);
                    if (fltr != null)
                    {
                        itemGroups = _itemGroups.Distinct().Where(fltr).ToList();
                    }
                    else itemGroups = _itemGroups.Distinct().ToList();
                }
                return itemGroups;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int Update(oItemGroup obj)
        {
            throw new NotImplementedException();
        }
    }
}
