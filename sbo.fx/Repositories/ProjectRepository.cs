using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using sbo.fx.Interfaces;
using sbo.fx.Models;

namespace sbo.fx.Repositories
{
    internal class ProjectRepository : BaseRepository, IRepository<oProject>
    {
        public int Add(oProject obj)
        {
            throw new NotImplementedException();
        }

        public int AddMultiple(List<oProject> objs)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public async Task<List<oProject>> GetList(Func<oProject, bool> fltr)
        {
            try
            {
                List<oProject> projects = new List<oProject>();
                if (SqlObject != null)
                {
                    var _projects = await SqlObject.QueryAsync<oProject>("sp_getProjects", param: null, transaction: null, commandTimeout: null, commandType: System.Data.CommandType.StoredProcedure);
                    if (fltr != null)
                    {
                        projects = _projects.Distinct().Where(fltr).ToList();
                    }
                    else projects = _projects.Distinct().ToList();
                }
                return projects;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int Update(oProject obj)
        {
            throw new NotImplementedException();
        }
    }
}
