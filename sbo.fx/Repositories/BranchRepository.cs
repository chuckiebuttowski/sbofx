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
    internal class BranchRepository : BaseRepository, IRepository<oBranch>
    {
        public int Add(oBranch obj)
        {
            throw new NotImplementedException();
        }

        public int AddMultiple(List<oBranch> objs)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public async Task<List<oBranch>> GetList(Func<oBranch, bool> fltr)
        {
            try
            {
                List<oBranch> branches = new List<oBranch>();
                if (SqlObject != null)
                {
                    var _branches = await SqlObject.QueryAsync<oBranch>("sp_getBranches", param: null, transaction: null, commandTimeout: null, commandType: System.Data.CommandType.StoredProcedure);
                    if (fltr != null)
                    {
                        branches = _branches.Distinct().Where(fltr).ToList();
                    }
                    else branches = _branches.Distinct().ToList();
                }
                return branches;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int Update(oBranch obj)
        {
            throw new NotImplementedException();
        }
    }
}
