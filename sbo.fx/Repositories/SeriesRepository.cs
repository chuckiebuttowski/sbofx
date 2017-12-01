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
    internal class SeriesRepository : BaseRepository, IRepository<oSeries>
    {
        public int Add(oSeries obj)
        {
            throw new NotImplementedException();
        }

        public int AddMultiple(List<oSeries> objs)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public async Task<List<oSeries>> GetList(Func<oSeries, bool> fltr)
        {
            try
            {
                List<oSeries> series = new List<oSeries>();
                if (SqlObject != null)
                {
                    var _series = await SqlObject.QueryAsync<oSeries>("sp_getSeries", param: null, transaction: null, commandTimeout: null, commandType: System.Data.CommandType.StoredProcedure);
                    if (fltr != null)
                    {
                        series = _series.Distinct().Where(fltr).ToList();
                    }
                    else series = _series.Distinct().ToList();
                }
                return series;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int Update(oSeries obj)
        {
            throw new NotImplementedException();
        }
    }
}
