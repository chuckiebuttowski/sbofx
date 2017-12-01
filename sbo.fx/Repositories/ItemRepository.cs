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
    internal class ItemRepository : BaseRepository, IItemRepository
    {
        public int Add(oItem obj)
        {
            Items itm = (Items)SboComObject.GetBusinessObject(BoObjectTypes.oItems);

            try
            {
                SboComObject.StartTransaction();

                int retCode = 0;
                itm.ItemCode = obj.ItemCode;
                itm.ItemName = obj.Description;
                itm.Series = obj.Series;
                itm.ItemsGroupCode = obj.ItemGroup;
                itm.InventoryItem = obj.InventoryItem == "Y" ? BoYesNoEnum.tYES : BoYesNoEnum.tNO;
                itm.PurchaseItem = obj.PurchaseItem == "Y" ? BoYesNoEnum.tYES : BoYesNoEnum.tNO;
                itm.SalesItem = obj.SalesItem == "Y" ? BoYesNoEnum.tYES : BoYesNoEnum.tNO;
                itm.UoMGroupEntry = obj.UoMGroup;
                itm.BarCode = obj.Barcode;
                itm.PriceList.SetCurrentLine(0);
                itm.PriceList.Price = obj.SellPrice;
                itm.AvgStdPrice = 100;

                retCode = itm.Add();

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
            catch
            {
                throw new Exception(GlobalInstance.Instance.SBOErrorMessage);
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(itm);
            }
        }

        public int AddMultiple(List<oItem> objs)
        {
            Items itm = null;

            try
            {
                SboComObject.StartTransaction();

                int retCode = 0;

                foreach (oItem obj in objs)
                {
                    itm = (Items)SboComObject.GetBusinessObject(BoObjectTypes.oItems);

                    itm.ItemCode = obj.ItemCode;
                    itm.ItemName = obj.Description;
                    itm.Series = obj.Series;
                    itm.ItemsGroupCode = obj.ItemGroup;
                    itm.InventoryItem = obj.InventoryItem == "Y" ? BoYesNoEnum.tYES : BoYesNoEnum.tNO;
                    itm.PurchaseItem = obj.PurchaseItem == "Y" ? BoYesNoEnum.tYES : BoYesNoEnum.tNO;
                    itm.SalesItem = obj.SalesItem == "Y" ? BoYesNoEnum.tYES : BoYesNoEnum.tNO;
                    itm.UoMGroupEntry = obj.UoMGroup;
                    itm.BarCode = obj.Barcode;

                    retCode = itm.Add();

                    if (retCode > 0) break;
                }

                if (retCode > 0)
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
            catch
            {
                throw new Exception(GlobalInstance.Instance.SBOErrorMessage);
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(itm);
            }
        }

        public int Update(oItem obj)
        {
            Items itm = (Items)SboComObject.GetBusinessObject(BoObjectTypes.oItems);

            try
            {
                SboComObject.StartTransaction();

                int retCode = 0;

                itm.GetByKey(obj.ItemCode);

                itm.ItemName = obj.Description;
                itm.Series = obj.Series;
                itm.ItemsGroupCode = obj.ItemGroup;
                itm.InventoryItem = obj.InventoryItem == "Y" ? BoYesNoEnum.tYES : BoYesNoEnum.tNO;
                itm.PurchaseItem = obj.PurchaseItem == "Y" ? BoYesNoEnum.tYES : BoYesNoEnum.tNO;
                itm.SalesItem = obj.SalesItem == "Y" ? BoYesNoEnum.tYES : BoYesNoEnum.tNO;
                itm.UoMGroupEntry = obj.UoMGroup;
                itm.BarCode = obj.Barcode;
                itm.PriceList.SetCurrentLine(0);
                itm.PriceList.Price = obj.SellPrice;

                retCode = itm.Update();

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
            catch
            {
                throw new Exception(GlobalInstance.Instance.SBOErrorMessage);
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(itm);
            }
        }

        public async Task<List<oItem>> GetByItemGroup(int groupCode)
        {
            try
            {
                return await GetList(x => x.ItemGroup == groupCode);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<oItem>> GetByItemSeries(int series)
        {
            try
            {
                return await GetList(x => x.Series == series);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<oItem> GetItemByItemCode(string itemCode)
        {
            try
            {
                var items = await GetList(x => x.ItemCode == itemCode);
                return items.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<oItem>> GetList(Func<oItem, bool> fltr)
        {
            try
            {
                List<oItem> itms = new List<oItem>();

                if (SqlObject != null)
                {
                    //var itmCache = new Dictionary<string, oItem>();
                    //var _itms = await SqlObject.QueryAsync<oItem, oItemSerial, oItem>("sp_getItems",
                    //    map: (i, s) => {
                    //        oItem _i = null;
                    //        if (!itmCache.TryGetValue(i.ItemCode, out _i))
                    //        {
                    //            _i = i;

                    //            if (!string.IsNullOrEmpty(s.ItemCode)) _i.Serials = new List<oItemSerial>();

                    //            itmCache.Add(_i.ItemCode, _i);
                    //        }

                    //        if (!string.IsNullOrEmpty(s.ItemCode)) _i.Serials.Add(s);

                    //        return _i;
                    //    }, param: null, transaction: null, commandTimeout: null, commandType: System.Data.CommandType.StoredProcedure);

                    var _itms = await SqlObject.QueryAsync<oItem>("sp_getItems", param: null, transaction: null, commandTimeout: null, commandType: System.Data.CommandType.StoredProcedure);

                    if (fltr != null)
                    {
                        itms = _itms.Distinct().Where(fltr).ToList();
                    }
                    else itms = _itms.Distinct().ToList();
                }

                return itms;
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
