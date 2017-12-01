using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using sbo.fx.Interfaces;
using sbo.fx.Factories;
using sbo.fx.Models;
using System.Collections.Generic;

namespace sbo.fx.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            GlobalInstance.Instance.DatabaseServerType = DBType.MSSQL2012;
            GlobalInstance.Instance.Server = "CHUCKIE";
            GlobalInstance.Instance.DBName = "ALPALAND_NEW_DB";
            GlobalInstance.Instance.DBUName = "sa";
            GlobalInstance.Instance.DBPword = "jsi@111";
            GlobalInstance.Instance.UName = "manager";
            GlobalInstance.Instance.Pword = "5555";

            GlobalInstance.Instance.InitializeSboComObject();
            GlobalInstance.Instance.InitializeSqlObject();

            //IItemRepository repo = new RepositoryFactory().ItemRepository();

            //repo.InitRepository(GlobalInstance.Instance.SqlObject);
            //repo.InitRepository(GlobalInstance.Instance.SboComObject);


            //oItem itm = new oItem
            //{
            //    ItemCode = "SEQL001-DUP",
            //    SellPrice = 600,
            //    ItemCost = 900,
            //    Barcode = "84938439938",
            //    Description = "HYUNDAI GRAND STAREX GLS CRDI VGT (10S) ABN 2617",
            //    ItemGroup = 114,
            //    UoMGroup = 3,
            //    Series = 3,
            //    InventoryItem = "Y",
            //    SalesItem = "Y",
            //    PurchaseItem = "Y"
            //};

            //repo.Add(itm);


            //IBusinessPartnerRepository repo = new RepositoryFactory().BusinessPartnerRepository();

            //repo.InitRepository(GlobalInstance.Instance.SqlObject);
            //repo.InitRepository(GlobalInstance.Instance.SboComObject);

            //oBusinessPartner bp = new oBusinessPartner
            //{
            //    Series = 1,
            //    CardCode = "SUP-001",
            //    CardName = "John Macasero",
            //    GroupCode = 101,
            //    CardType = "S",
            //    LicTradNum = "11928923"
            //};

            //repo.Add(bp);

            //IJournalRepository repo = new RepositoryFactory().JournalRepository();

            //repo.InitRepository(GlobalInstance.Instance.SqlObject);
            //repo.InitRepository(GlobalInstance.Instance.SboComObject);

            //List<oJournalLine> lines = new List<oJournalLine>();
            //lines.Add(new oJournalLine {
            //    GLCode = "SDORM000002",
            //    LineType = "C",
            //    LineMemo = "from api",
            //    Segment = 3,
            //    Debit = 0,
            //    Credit = 8000
            //});

            //lines.Add(new oJournalLine
            //{
            //    GLCode = "_SYS00000000045",
            //    LineType = "D",
            //    LineMemo = "from api",
            //    Segment = 3,
            //    Debit = 6500,
            //    Credit = 0
            //});

            //lines.Add(new oJournalLine
            //{
            //    GLCode = "_SYS00000000157",
            //    LineType = "D",
            //    LineMemo = "from api",
            //    Segment = 3,
            //    Debit = 1500,
            //    Credit = 0
            //});

            //oJournal j = new oJournal {
            //    JdtNumber = 20002968,//supply if update
            //    Series = 102,
            //    DocDate = DateTime.Now,
            //    DocDueDate = DateTime.Now,
            //    CreateDate = DateTime.Now,
            //    JournalMemo = "TEst",
            //    Reference1 = "from api",
            //    Reference2 = "Test",
            //    Reference3 = "Test",
            //    JournalLines = lines
            //};

            //repo.Update(j);


            IInventoryTransactionRepository repo = new RepositoryFactory().InventoryTransactionRepository();
            repo.InitRepository(GlobalInstance.Instance.SboComObject);

            List<oInventoryTransactionLine> invLines = new List<oInventoryTransactionLine>();
            invLines.Add(new oInventoryTransactionLine {
                ItemCode = "CIPITB020",
                GLAccountCode = "_SYS00000001044",
                WarehouseId = "01",
                Quantity = 5,
                Price = 200
            });

            oInventoryTransaction inv = new oInventoryTransaction();
            inv.BranchId = 1;
            //inv.Series = 141;
            inv.Series = 149;
            inv.DocDate = DateTime.Now;
            inv.TaxDate = DateTime.Now;
            inv.ReferenceNo = "Test";
            inv.Remarks = "Test from sbo.fx";
            inv.JournalRemarks = "Test";
            inv.TransactionLines = invLines;
            inv.InventoryTransactionType = InventoryType.Out;

            repo.Add(inv);

        }
    }
}
