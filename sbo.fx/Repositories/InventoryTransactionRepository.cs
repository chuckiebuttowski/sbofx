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
    internal class InventoryTransactionRepository : BaseRepository, IInventoryTransactionRepository
    {
        public int Add(oInventoryTransaction obj)
        {
            int retCode = 0;

            if (obj.InventoryTransactionType == InventoryType.In)
            {
                retCode = InventoryIn(obj);
            }
            else
            {
                retCode = InventoryOut(obj);
            }

            return retCode;
        }

        public int AddMultiple(List<oInventoryTransaction> objs)
        {
            throw new NotImplementedException();
        }

        public int Update(oInventoryTransaction obj)
        {
            throw new NotImplementedException();
        }

        public Task<List<oInventoryTransaction>> GetList(Func<oInventoryTransaction, bool> fltr)
        {
            throw new NotImplementedException();
        }

        public async Task<List<oInventoryTransaction>> GetTransactionByDateRange(DateTime from, DateTime to, InventoryType type)
        {
            try
            {
                return await GetTransactionList(x => x.DocDate >= from && x.DocDate <= to, type);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<oInventoryTransaction> GetTransactionByDocNo(int DocNo, InventoryType type)
        {
            try
            {
                var transaction = await GetTransactionList(x => x.DocNum == DocNo, type);
                return transaction.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<oInventoryTransaction>> GetTransactionList(Func<oInventoryTransaction, bool> fltr, InventoryType type)
        {
            try
            {
                List<oInventoryTransaction> invntrys = new List<oInventoryTransaction>();

                if (type == InventoryType.In)
                {
                    invntrys = await GetInventoryIns(fltr);
                }
                else
                {
                    invntrys = await GetInventoryOuts(fltr);
                }
                return invntrys;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private int InventoryIn(oInventoryTransaction invTransaction)
        {
            Documents invIn = SboComObject.GetBusinessObject(BoObjectTypes.oInventoryGenEntry);
        
            try
            {
                SboComObject.StartTransaction();

                int retCode = 0;

                invIn.DocDate = invTransaction.DocDate;
                invIn.TaxDate = invTransaction.TaxDate;
                invIn.Reference2 = invTransaction.ReferenceNo;
                invIn.Series = invTransaction.Series;
                invIn.Comments = invTransaction.Remarks;
                invIn.JournalMemo = invTransaction.JournalRemarks;
                invIn.BPL_IDAssignedToInvoice = invTransaction.BranchId;

                if (invTransaction.TransactionLines.Count > 0)
                {
                    foreach (oInventoryTransactionLine line in invTransaction.TransactionLines)
                    {  
                        invIn.Lines.SetCurrentLine(line.LineNo);
                        invIn.Lines.ItemCode = line.ItemCode;
                        invIn.Lines.Quantity = line.Quantity;
                        invIn.Lines.Price = line.Price;
                        invIn.Lines.WarehouseCode = line.WarehouseId;
                        invIn.Lines.AccountCode = line.GLAccountCode;
                        

                        invIn.Lines.Add();

                    }
                }

                retCode = invIn.Add();
                if (retCode != 0)
                {
                    int errCode = 0;
                    string errMessage = "";
                    SboComObject.GetLastError(out errCode, out errMessage);
                    GlobalInstance.Instance.SBOErrorCode = errCode;
                    GlobalInstance.Instance.SBOErrorMessage = errMessage;

                    SboComObject.EndTransaction(BoWfTransOpt.wf_RollBack);
                }
                else SboComObject.EndTransaction(BoWfTransOpt.wf_Commit);

                return retCode;
            }
            catch (Exception ex)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(invIn);
                throw new Exception(GlobalInstance.Instance.SBOErrorMessage == null ? ex.Message : GlobalInstance.Instance.SBOErrorMessage);
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(invIn);
            }
        }

        private int InventoryOut(oInventoryTransaction invTransaction)
        {
            Documents invOut = SboComObject.GetBusinessObject(BoObjectTypes.oInventoryGenExit);

            try
            {
                SboComObject.StartTransaction();

                int retCode = 0;

                invOut.DocDate = invTransaction.DocDate;
                invOut.TaxDate = invTransaction.TaxDate;
                invOut.Reference2 = invTransaction.ReferenceNo;
                invOut.Series = invTransaction.Series;
                invOut.Comments = invTransaction.Remarks;
                invOut.JournalMemo = invTransaction.JournalRemarks;
                invOut.BPL_IDAssignedToInvoice = invTransaction.BranchId;

                if (invTransaction.TransactionLines.Count > 0)
                {
                    foreach (oInventoryTransactionLine line in invTransaction.TransactionLines)
                    {
                        invOut.Lines.SetCurrentLine(line.LineNo);
                        invOut.Lines.ItemCode = line.ItemCode;
                        invOut.Lines.Quantity = line.Quantity;
                        invOut.Lines.Price = line.Price;
                        invOut.Lines.WarehouseCode = line.WarehouseId;
                        invOut.Lines.AccountCode = line.GLAccountCode;
                        invOut.Lines.Add();

                    }
                }

                retCode = invOut.Add();
                if (retCode != 0)
                {
                    int errCode = 0;
                    string errMessage = "";
                    SboComObject.GetLastError(out errCode, out errMessage);
                    GlobalInstance.Instance.SBOErrorCode = errCode;
                    GlobalInstance.Instance.SBOErrorMessage = errMessage;

                    SboComObject.EndTransaction(BoWfTransOpt.wf_RollBack);
                }
                else SboComObject.EndTransaction(BoWfTransOpt.wf_Commit);

                return retCode;
            }
            catch (Exception ex)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(invOut);
                throw new Exception(GlobalInstance.Instance.SBOErrorMessage == null ? ex.Message : GlobalInstance.Instance.SBOErrorMessage);
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(invOut);
            }
        }

        private async Task<List<oInventoryTransaction>> GetInventoryIns(Func<oInventoryTransaction, bool> fltr)
        {
            try
            {
                List<oInventoryTransaction> invtries = new List<oInventoryTransaction>();

                if (SqlObject != null)
                {
                    var invntryCache = new Dictionary<int, oInventoryTransaction>();
                    var invnty = await SqlObject.QueryAsync<oInventoryTransaction, oInventoryTransactionLine, oInventoryTransaction>("sp_getGoodsRecepits", (invntyHdr, invntyLine) =>
                    {
                        oInventoryTransaction _invnty = null;
                        if (!invntryCache.TryGetValue(invntyHdr.DocNum, out _invnty))
                        {
                            _invnty = invntyHdr;
                            _invnty.TransactionLines = new List<oInventoryTransactionLine>();
                            invntryCache.Add(_invnty.DocNum, _invnty);
                        }

                        _invnty.TransactionLines.Add(invntyLine);

                        return _invnty;

                    }, param: null, transaction: null, buffered: true, splitOn: "Id", commandTimeout: null, commandType: System.Data.CommandType.StoredProcedure);
                    if (fltr != null)
                    {
                        invtries = invnty.Distinct().Where(fltr).ToList();
                    }
                    else invtries = invnty.Distinct().ToList();
                }

                return invtries;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private async Task<List<oInventoryTransaction>> GetInventoryOuts(Func<oInventoryTransaction, bool> fltr)
        {
            try
            {
                List<oInventoryTransaction> invtries = new List<oInventoryTransaction>();

                if (SqlObject != null)
                {
                    var invntryCache = new Dictionary<int, oInventoryTransaction>();
                    var invnty = await SqlObject.QueryAsync<oInventoryTransaction, oInventoryTransactionLine, oInventoryTransaction>("sp_getIssues", (invntyHdr, invntyLine) =>
                    {
                        oInventoryTransaction _invnty = null;
                        if (!invntryCache.TryGetValue(invntyHdr.DocNum, out _invnty))
                        {
                            _invnty = invntyHdr;
                            _invnty.TransactionLines = new List<oInventoryTransactionLine>();
                            invntryCache.Add(_invnty.DocNum, _invnty);
                        }

                        _invnty.TransactionLines.Add(invntyLine);

                        return _invnty;

                    }, param: null, transaction: null, buffered: true, splitOn: "Id", commandTimeout: null, commandType: System.Data.CommandType.StoredProcedure);
                    if (fltr != null)
                    {
                        invtries = invnty.Distinct().Where(fltr).ToList();
                    }
                    else invtries = invnty.Distinct().ToList();
                }

                return invtries;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

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
