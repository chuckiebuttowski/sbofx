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
    internal class GlAccountRepository : BaseRepository, IGlAccountRepository
    {
        public int Add(oGlAccount obj)
        {
            ChartOfAccounts coa = (ChartOfAccounts)SboComObject.GetBusinessObject(BoObjectTypes.oChartOfAccounts);

            try
            {
               SboComObject.StartTransaction();

                int retcode = 0;

                coa.Code = obj.AccntCode;
                coa.Name = obj.AccntName;
                coa.BPLID = obj.BPLId;
                coa.FormatCode = obj.FormatCode;

                retcode = coa.Add();

                if (retcode > 0)
                {
                    int errCode = 0;
                    string errMessage = "";
                    SboComObject.GetLastError(out errCode, out errMessage);
                    GlobalInstance.Instance.SBOErrorCode = errCode;
                    GlobalInstance.Instance.SBOErrorMessage = errMessage;

                    SboComObject.EndTransaction(BoWfTransOpt.wf_RollBack);
                }
                else SboComObject.EndTransaction(BoWfTransOpt.wf_Commit);


                return retcode;
            }
            catch
            {
                throw new Exception(GlobalInstance.Instance.SBOErrorMessage);
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(coa);
            }
        }

        public int AddMultiple(List<oGlAccount> objs)
        {
            ChartOfAccounts coa = null;

            try
            {
                SboComObject.StartTransaction();

                int retcode = 0;

                foreach (oGlAccount obj in objs)
                {
                    coa = (ChartOfAccounts)SboComObject.GetBusinessObject(BoObjectTypes.oChartOfAccounts);

                    coa.Code = obj.AccntCode;
                    coa.Name = obj.AccntName;
                    coa.BPLID = obj.BPLId;
                    coa.FormatCode = obj.FormatCode;

                    retcode = coa.Add();

                    if (retcode > 0) break;
                }

                if (retcode > 0)
                {
                    int errCode = 0;
                    string errMessage = "";
                    SboComObject.GetLastError(out errCode, out errMessage);
                    GlobalInstance.Instance.SBOErrorCode = errCode;
                    GlobalInstance.Instance.SBOErrorMessage = errMessage;

                    SboComObject.EndTransaction(BoWfTransOpt.wf_RollBack);
                }
                else SboComObject.EndTransaction(BoWfTransOpt.wf_Commit);


                return retcode;
            }
            catch
            {
                throw new Exception(GlobalInstance.Instance.SBOErrorMessage);
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(coa);
            }
        }

        public int Update(oGlAccount obj)
        {
            ChartOfAccounts coa = (ChartOfAccounts)SboComObject.GetBusinessObject(BoObjectTypes.oChartOfAccounts);

            try
            {
                SboComObject.StartTransaction();

                int retcode = 0;

                if (obj.IsSegmented)
                {
                    coa.LoadingFactorCode = obj.Segment_0;
                    coa.LoadingFactorCode2 = obj.Segment_1;
                }
                else coa.Code = obj.AccntCode;

                coa.Name = obj.AccntName;
                coa.BPLID = obj.BPLId;
                coa.LockManualTransaction = obj.IsControlAccount == "Y" ? BoYesNoEnum.tYES : BoYesNoEnum.tNO;
                coa.ActiveAccount = obj.IsPostable == "Y" ? BoYesNoEnum.tYES : BoYesNoEnum.tNO;

                retcode = coa.Update();

                if (retcode > 0)
                {
                    int errCode = 0;
                    string errMessage = "";
                    SboComObject.GetLastError(out errCode, out errMessage);
                    GlobalInstance.Instance.SBOErrorCode = errCode;
                    GlobalInstance.Instance.SBOErrorMessage = errMessage;

                    SboComObject.EndTransaction(BoWfTransOpt.wf_RollBack);
                }
                else SboComObject.EndTransaction(BoWfTransOpt.wf_Commit);


                return retcode;
            }
            catch
            {
                throw new Exception(GlobalInstance.Instance.SBOErrorMessage);
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(coa);
            }
        }

        public async Task<oGlAccount> GetByAccountCode(string accountCode)
        {
            try
            {
                var glAccounts = await GetList(x => x.AccntCode == accountCode);
                return glAccounts.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<oGlAccount>> GetBySegment(string segment)
        {
            try
            {
                return await GetList(x => x.Segment_1 == segment);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<oGlAccount>> GetList(Func<oGlAccount, bool> fltr)
        {
            try
            {
                List<oGlAccount> gl_list = new List<oGlAccount>();

                if (SqlObject != null)
                {
                    var _gl_list = await SqlObject.QueryAsync<oGlAccount>("sp_getCoa", null, null, null, System.Data.CommandType.StoredProcedure);

                    if (fltr != null) gl_list = _gl_list.Distinct().Where(fltr).ToList();
                    else gl_list = _gl_list.Distinct().ToList();
                }

                return gl_list;
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
