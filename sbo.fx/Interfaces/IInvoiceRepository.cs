using sbo.fx.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbo.fx.Interfaces
{
    public interface IInvoiceRepository: IRepository<oInvoice>
    {
        Task<List<oInvoice>> GetTransactionList(Func<oInventoryTransaction, bool> fltr, InvoiceType type);
        Task<oInvoice> GetTransactionByDocNo(int DocNo, InventoryType type);
        Task<List<oInvoice>> GetTransactionByDateRange(DateTime from, DateTime to, InvoiceType type);
        Task<List<oInvoice>> GetTransactionByCardCode(string cardCode, InvoiceType type);
    }
}
