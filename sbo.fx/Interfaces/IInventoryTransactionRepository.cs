using sbo.fx.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbo.fx.Interfaces
{
    public interface IInventoryTransactionRepository: IRepository<oInventoryTransaction>
    {
        Task<List<oInventoryTransaction>> GetTransactionList(Func<oInventoryTransaction, bool> fltr, InventoryType type);
        Task<oInventoryTransaction> GetTransactionByDocNo(int DocNo, InventoryType type);
        Task<List<oInventoryTransaction>> GetTransactionByDateRange(DateTime from, DateTime to, InventoryType type);
    }
}
