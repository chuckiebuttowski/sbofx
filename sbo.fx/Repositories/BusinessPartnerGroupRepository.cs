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
    internal class BusinessPartnerGroupRepository : BaseRepository, IRepository<oBusinessPartnerGroup>
    {
        public int Add(oBusinessPartnerGroup obj)
        {
            throw new NotImplementedException();
        }

        public int AddMultiple(List<oBusinessPartnerGroup> objs)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public async Task<List<oBusinessPartnerGroup>> GetList(Func<oBusinessPartnerGroup, bool> fltr)
        {
            try
            {
                List<oBusinessPartnerGroup> bpGroups = new List<oBusinessPartnerGroup>();
                if (SqlObject != null)
                {
                    var _bpGroups = await SqlObject.QueryAsync<oBusinessPartnerGroup>("sp_getBpGroups", param: null, transaction: null, commandTimeout: null, commandType: System.Data.CommandType.StoredProcedure);
                    if (fltr != null)
                    {
                        bpGroups = _bpGroups.Distinct().Where(fltr).ToList();
                    }
                    else bpGroups = _bpGroups.Distinct().ToList();
                }
                return bpGroups;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int Update(oBusinessPartnerGroup obj)
        {
            throw new NotImplementedException();
        }
    }
}
