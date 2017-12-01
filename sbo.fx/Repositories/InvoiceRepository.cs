using sbo.fx.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sbo.fx.Models;
using SAPbobsCOM;
using Dapper;

namespace sbo.fx.Repositories
{
    internal class InvoiceRepository : BaseRepository, IInvoiceRepository
    {
        public int Add(oInvoice obj)
        {
            throw new NotImplementedException();
        }

        public int AddMultiple(List<oInvoice> objs)
        {
            throw new NotImplementedException();
        }

        public int Update(oInvoice obj)
        {
            throw new NotImplementedException();
        }

        public Task<List<oInvoice>> GetList(Func<oInvoice, bool> fltr)
        {
            throw new NotImplementedException();
        }

        public Task<List<oInvoice>> GetTransactionByCardCode(string cardCode, InvoiceType type)
        {
            throw new NotImplementedException();
        }

        public Task<List<oInvoice>> GetTransactionByDateRange(DateTime from, DateTime to, InvoiceType type)
        {
            throw new NotImplementedException();
        }

        public Task<oInvoice> GetTransactionByDocNo(int DocNo, InventoryType type)
        {
            throw new NotImplementedException();
        }

        public Task<List<oInvoice>> GetTransactionList(Func<oInventoryTransaction, bool> fltr, InvoiceType type)
        {
            throw new NotImplementedException();
        }

        //private int AR(oInvoice inv)
        //{
           
        //}

        //private int AP(oInvoice inv)
        //{

        //}

        public void Dispose()
        {
            SboComObject.Disconnect();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(SboComObject);
            SboComObject = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }
    }
}
