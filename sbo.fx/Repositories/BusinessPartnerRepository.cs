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
    internal class BusinessPartnerRepository : BaseRepository, IBusinessPartnerRepository
    {
        public int Add(oBusinessPartner obj)
        {
            BusinessPartners _bp = (BusinessPartners)SboComObject.GetBusinessObject(BoObjectTypes.oBusinessPartners);
            SeriesRepository s = new SeriesRepository();
            s.InitRepository(GlobalInstance.Instance.SqlObject);
            var tempList = s.GetList(null);
            oSeries _s = new oSeries();
            _s = tempList.Result.FirstOrDefault(x => x.ObjectCode == ((int)SboTransactionType.BP).ToString() && x.Series == obj.Series && x.DocSubType == obj.CardType);

            try
            {
                SboComObject.StartTransaction();

                int retCode = 0;

                if (_s != null) if (_s.SeriesName.ToLower() == "manual") _bp.CardCode = obj.CardCode;

                _bp.Series = obj.Series;
                _bp.GroupCode = obj.GroupCode;
                _bp.CardName = obj.CardName;
                _bp.Address = obj.Address;
                _bp.CardType = obj.CardType == "C" ? BoCardTypes.cCustomer : BoCardTypes.cSupplier;
                _bp.ContactPerson = obj.CntctPerson;
                _bp.Cellular = obj.Cellular;
                _bp.Phone1 = obj.Phone1;
                _bp.Phone2 = obj.Phone2;
                _bp.EmailAddress = obj.Email;

                retCode = _bp.Add();

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
                System.Runtime.InteropServices.Marshal.ReleaseComObject(_bp);
                throw new Exception(GlobalInstance.Instance.SBOErrorMessage == null ? ex.Message : GlobalInstance.Instance.SBOErrorMessage);
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(_bp);
            }
        }

        public int AddMultiple(List<oBusinessPartner> objs)
        {
            BusinessPartners _bp = null;

            try
            {
                SboComObject.StartTransaction();

                int retCode = 0;

                foreach (oBusinessPartner obj in objs)
                {
                    _bp.CardCode = obj.CardCode;
                    _bp.CardName = obj.CardName;
                    _bp.Address = obj.Address;
                    _bp.CardType = obj.CardType == "C" ? BoCardTypes.cCustomer : BoCardTypes.cSupplier;
                    _bp.ContactPerson = obj.CntctPerson;
                    _bp.Cellular = obj.Cellular;
                    _bp.Phone1 = obj.Phone1;
                    _bp.Phone2 = obj.Phone2;
                    _bp.EmailAddress = obj.Email;
                    _bp.DebitorAccount = obj.DebPayAcct;

                    retCode = _bp.Add();

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
                System.Runtime.InteropServices.Marshal.ReleaseComObject(_bp);
            }
        }

        public int Update(oBusinessPartner obj)
        {
            BusinessPartners _bp = (BusinessPartners)SboComObject.GetBusinessObject(BoObjectTypes.oBusinessPartners);

            try
            {
                SboComObject.StartTransaction();

                int retCode = 0;

                _bp.GetByKey(obj.CardCode);

                _bp.CardName = obj.CardName;
                _bp.Address = obj.Address;
                _bp.ContactPerson = obj.CntctPerson;
                _bp.Cellular = obj.Cellular;
                _bp.Phone1 = obj.Phone1;
                _bp.Phone2 = obj.Phone2;
                _bp.EmailAddress = obj.Email;

                retCode = _bp.Update();

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
                System.Runtime.InteropServices.Marshal.ReleaseComObject(_bp);
                throw new Exception(GlobalInstance.Instance.SBOErrorMessage == null ? ex.Message : GlobalInstance.Instance.SBOErrorMessage);
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(_bp);
            }
        }

        public async Task<oBusinessPartner> GetByCardCode(string cardCode)
        {
            try
            {
                var list = await GetList(x => x.CardCode == cardCode);
                return list.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<oBusinessPartner>> GetByCardType(string cardType)
        {
            try
            {
                return await GetList(x => x.CardType == cardType);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<oBusinessPartner>> GetByGroupCode(int groupCode)
        {
            try
            {
                return await GetList(x => x.GroupCode == groupCode);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<oBusinessPartner>> GetBySeries(int series)
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

        public async Task<List<oBusinessPartner>> GetByStatus(string status)
        {
            try
            {
                return await GetList(x => x.Frozen == status);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<oBusinessPartner>> GetList(Func<oBusinessPartner, bool> fltr)
        {
            try
            {
                List<oBusinessPartner> bp_list = new List<oBusinessPartner>();
                if (SqlObject != null)
                {
                    var _bp_list = await SqlObject.QueryAsync<oBusinessPartner>("sp_getBusinessPartners", null, null, null, System.Data.CommandType.StoredProcedure);

                    if (fltr != null) bp_list = _bp_list.Distinct().Where(fltr).ToList();
                    else bp_list = _bp_list.Distinct().ToList();
                }

                return bp_list;
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
