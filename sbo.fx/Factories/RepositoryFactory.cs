using sbo.fx.Interfaces;
using sbo.fx.Models;
using sbo.fx.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbo.fx.Factories
{
    public class RepositoryFactory
    {
        public IJournalRepository JournalRepository()
        {
            return new JournalRepository();
        }

        public IItemRepository ItemRepository()
        {
            return new ItemRepository();
        }

        public IGlAccountRepository GlAccountRepository()
        {
            return new GlAccountRepository();
        }

        public IBusinessPartnerRepository BusinessPartnerRepository()
        {
            return new BusinessPartnerRepository();
        }

        public IInvoiceRepository InvoiceRepository()
        {
            return new InvoiceRepository();
        }

        public IInventoryTransactionRepository InventoryTransactionRepository()
        {
            return new InventoryTransactionRepository();
        }

        public IRepository<oBranch> BranchRepository()
        {
            return new BranchRepository();
        }

        public IRepository<oProject> ProjectRepository()
        {
            return new ProjectRepository();
        }

        public IRepository<oSeries> SeriesRepository()
        {
            return new SeriesRepository();
        }

        public IRepository<oTax> TaxRepository()
        {
            return new TaxRepository();
        }

        public IRepository<oWarehouse> WarehouseRepository()
        {
            return new WarehouseRepository();
        }

        public IRepository<oBusinessPartnerGroup> BusinessPartnerGroupRepository()
        {
            return new BusinessPartnerGroupRepository();
        }

        public IRepository<oItemGroup> ItemGroupRepository()
        {
            return new ItemGroupRepository();
        }
    }
}
