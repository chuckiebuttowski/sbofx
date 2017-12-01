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
    internal class JournalRepository : BaseRepository, IJournalRepository
    {
        public int Add(oJournal obj)
        {
            JournalEntries jrnls = (JournalEntries)SboComObject.GetBusinessObject(BoObjectTypes.oJournalEntries);

            try
            {
                SboComObject.StartTransaction();

                int retCode = 0;

                jrnls.DueDate = obj.DocDueDate;
                jrnls.TaxDate = obj.TaxDate;
                jrnls.ReferenceDate = obj.DocDate;
                jrnls.Memo = obj.JournalMemo;
                jrnls.ProjectCode = obj.Project;

                if (obj.JournalLines.Count > 0)
                {
                    foreach (oJournalLine jrnlLine in obj.JournalLines)
                    {
                        jrnls.Lines.DueDate = obj.DocDueDate;
                        jrnls.Lines.TaxDate = obj.TaxDate;
                        jrnls.Lines.ReferenceDate1 = obj.DocDate;
                        jrnls.Lines.ShortName = jrnlLine.GLCode;
                        jrnls.Lines.BPLID = jrnlLine.Segment;
                        jrnls.Lines.Debit = jrnlLine.Debit;
                        jrnls.Lines.Credit = jrnlLine.Credit;                        
                        jrnls.Lines.Add();
                    }
                }

                retCode = jrnls.Add();
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
            catch(Exception ex)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(jrnls);
                throw new Exception(GlobalInstance.Instance.SBOErrorMessage == null? ex.Message: GlobalInstance.Instance.SBOErrorMessage);
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(jrnls);
            }
        }

        public int AddMultiple(List<oJournal> objs)
        {
            JournalEntries jrnl = null;

            try
            {
                SboComObject.StartTransaction();

                int retCode = 0;

                foreach (oJournal obj in objs)
                {
                    jrnl = (JournalEntries)SboComObject.GetBusinessObject(BoObjectTypes.oJournalEntries);

                    jrnl.DueDate = obj.DocDueDate;
                    jrnl.TaxDate = obj.TaxDate;
                    jrnl.ReferenceDate = obj.DocDate;
                    jrnl.Memo = obj.JournalMemo;
                    jrnl.ProjectCode = obj.Project;

                    if (obj.JournalLines.Count > 0)
                    {
                        foreach (oJournalLine jrnlLine in obj.JournalLines)
                        {
                            jrnl.Lines.DueDate = obj.DocDueDate;
                            jrnl.Lines.TaxDate = obj.TaxDate;
                            jrnl.Lines.ReferenceDate1 = obj.DocDate;
                            jrnl.Lines.AccountCode = jrnlLine.GLCode;
                            jrnl.Lines.BPLID = jrnlLine.Segment;
                            jrnl.Lines.Debit = jrnlLine.Debit;
                            jrnl.Lines.Credit = jrnlLine.Credit;
                            jrnl.Lines.Add();
                        }
                    }

                    retCode = jrnl.Add();

                    if (retCode > 0) break;
                }

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
                System.Runtime.InteropServices.Marshal.ReleaseComObject(jrnl);
            }
        }

        public int Update(oJournal obj)
        {
            JournalEntries jrnls = (JournalEntries)SboComObject.GetBusinessObject(BoObjectTypes.oJournalEntries);

            try
            {
                SboComObject.StartTransaction();

                int retCode = 0;

                jrnls.GetByKey(obj.JdtNumber);
                
                jrnls.DueDate = obj.DocDueDate;
                jrnls.TaxDate = obj.TaxDate;
                jrnls.ReferenceDate = obj.DocDate;
                jrnls.Memo = obj.JournalMemo;
                jrnls.ProjectCode = obj.Project;
                jrnls.Series = obj.Series;
                jrnls.ExposedTransNumber = jrnls.ExposedTransNumber;

                if (obj.JournalLines.Count > 0)
                {
                    foreach (oJournalLine jrnlLine in obj.JournalLines)
                    {
                        jrnls.Lines.ExposedTransNumber = obj.TransId;
                        jrnls.Lines.DueDate = obj.DocDueDate;
                        jrnls.Lines.TaxDate = obj.TaxDate;
                        jrnls.Lines.ReferenceDate1 = obj.DocDate;
                        jrnls.Lines.ShortName = jrnlLine.GLCode;
                        jrnls.Lines.BPLID = jrnlLine.Segment;
                        jrnls.Lines.Debit = jrnlLine.Debit;
                        jrnls.Lines.Credit = jrnlLine.Credit;
                        jrnls.Lines.Add();
                    }
                }

                retCode = jrnls.Update();
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
                System.Runtime.InteropServices.Marshal.ReleaseComObject(jrnls);
                throw new Exception(GlobalInstance.Instance.SBOErrorMessage == null ? ex.Message : GlobalInstance.Instance.SBOErrorMessage);
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(jrnls);
            }
        }

        public async Task<List<oJournal>> GetByDateRange(DateTime strtDate, DateTime endDate)
        {
            try
            {
                return await GetList(x => x.DocDate >= strtDate && x.DocDate <= endDate);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<oJournal>> GetByProject(string projectCode)
        {
            try
            {
                return await GetList(x => x.Project == projectCode);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<oJournal> GetByTransId(int TransId)
        {
            try
            {
                var jrnls = await GetList(x => x.TransId == TransId);
                return jrnls.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<oJournal>> GetList(Func<oJournal, bool> fltr)
        {
            try
            {
                List<oJournal> jrnals = new List<oJournal>();

                if (SqlObject != null)
                {
                    //var param = new DynamicParameters();
                    //param.Add("@TransId", dbType: System.Data.DbType.Int32, value: 0, direction: System.Data.ParameterDirection.Input);

                    var jDictionary = new Dictionary<int, oJournal>();
                    var _j = await SqlObject.QueryAsync<oJournal, oJournalLine, oJournal>("sp_getjournals", map: (j, jl) =>
                    {
                        oJournal jrnl = null;
                        if (!jDictionary.TryGetValue(j.TransId, out jrnl))
                        {
                            jrnl = j;
                            jrnl.JournalLines = new List<oJournalLine>();
                            jDictionary.Add(jrnl.TransId, jrnl);
                        }

                        jrnl.JournalLines.Add(jl);

                        return jrnl;
                    }, param: null, transaction: null, buffered: true, splitOn: "JournalLinesId", commandTimeout: null, commandType: System.Data.CommandType.StoredProcedure);

                    if (fltr != null)
                    {
                        jrnals = _j.Distinct().Where(fltr).ToList();
                    }
                    else jrnals = _j.Distinct().ToList();
                }

                return jrnals;
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
