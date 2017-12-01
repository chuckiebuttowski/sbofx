using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbo.fx.Interfaces
{
    public interface IRepository<T> : IDisposable where T : class
    {
        int Add(T obj);
        int AddMultiple(List<T> objs);
        int Update(T obj);
        Task<List<T>> GetList(Func<T, bool> fltr);
        void InitRepository(Company sboComObject);
        void InitRepository(SqlConnection sqlObject);
        void InitRepository(Company sboComObject, SqlConnection sqlObject);
    }
}
