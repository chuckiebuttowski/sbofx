using sbo.fx.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbo.fx.Interfaces
{
    public interface IJournalRepository: IRepository<oJournal>
    {
        Task<oJournal> GetByTransId(int TransId);
        Task<List<oJournal>> GetByDateRange(DateTime strtDate, DateTime endDate);
        Task<List<oJournal>> GetByProject(string projectCode);
    }
}
