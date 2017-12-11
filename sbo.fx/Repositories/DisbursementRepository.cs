using sbo.fx.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAPbobsCOM;
using sbo.fx.Models;
using System.Data.SqlClient;
using Dapper;

namespace sbo.fx.Repositories
{
    internal class DisbursementRepository : BaseRepository, IDisbursementRepository
    {
        public int Add(oPayment obj)
        {
            Payments payment = (Payments)SboComObject.GetBusinessObject(BoObjectTypes.oVendorPayments);

            try
            {
                SboComObject.StartTransaction();

                int retCode = 0;


                if (obj.DocType != "A") payment.DocType = BoRcptTypes.rAccount;
                if (obj.DocType != "S") payment.DocType = BoRcptTypes.rSupplier;
                if (obj.DocType != "C") payment.DocType = BoRcptTypes.rCustomer;
                payment.CardCode = obj.CardCode;
                payment.CardName = obj.CardName;
                payment.Series = obj.Series == 0 ? -1 : obj.Series;//get default series
                payment.DocDate = obj.DocDate;
                payment.TaxDate = obj.TaxDate;
                payment.DueDate = obj.DocDueDate;
                payment.Remarks = obj.Comments;
                payment.JournalRemarks = obj.JournalMemo;
                payment.CounterReference = obj.ReferenceNo;
                payment.CashSum = obj.CashSum;
                payment.CashAccount = obj.CashAccount;
                payment.TransferAccount = obj.BankTransferAccount;
                payment.TransferSum = obj.BankTransferSum;
                payment.TransferReference = obj.BankTransferReference;
                payment.TransferDate = obj.BankTransferDate;
                if (GlobalInstance.Instance.IsSegmented) payment.BPLID = obj.BranchId;
                if (GlobalInstance.Instance.HasCostCenter) payment.ProjectCode = obj.ProjectId;


                if (obj.PaymentLines.Count != 0)
                {
                    int invCtr = 0;
                    foreach(oPaymentLine p in obj.PaymentLines)
                    {
                        payment.Invoices.SetCurrentLine(invCtr);
                        payment.Invoices.SumApplied = p.SumApplied;
                        payment.Invoices.AppliedFC = p.SumApplied;
                        payment.Invoices.DocEntry = p.InvoiceNo;
                        payment.Invoices.InvoiceType = BoRcptInvTypes.it_Invoice;
                        payment.Invoices.Add();
                        invCtr += 1;
                    }
                }

                if (obj.CheckPayments.Count != 0)
                {
                    int chkCtr = 0;
                    foreach (oCheckPayment chk in obj.CheckPayments)
                    {
                        payment.Checks.SetCurrentLine(chkCtr);
                        payment.Checks.CheckSum = chk.CheckSum;
                        payment.Checks.CheckNumber = chk.CheckNum;
                        payment.Checks.BankCode = chk.BankCode;
                        payment.Checks.AccounttNum = chk.AccountNo;
                        payment.Checks.CheckAccount = chk.CheckAccount;
                        payment.Checks.Trnsfrable = chk.IsTransferable == "Y" ? BoYesNoEnum.tYES: BoYesNoEnum.tNO;
                        chkCtr += 1;
                    }
                }

                if (obj.CreditPayments.Count != 0)
                {
                    int credCtr = 0;
                    foreach (oCreditPayment cr in obj.CreditPayments)
                    {
                        payment.CreditCards.SetCurrentLine(credCtr);
                        payment.CreditCards.CreditAcct = cr.CreditAccount;
                        payment.CreditCards.CreditCard = cr.CreditCard;
                        payment.CreditCards.NumOfPayments = cr.NumberOfPayments;
                        payment.CreditCards.CreditSum = cr.CreditSum;
                        payment.CreditCards.FirstPaymentDue = cr.FirstDue;
                        payment.CreditCards.FirstPaymentSum = cr.FirstPayment;
                        payment.CreditCards.VoucherNum = cr.VoucherNo;
                        payment.CreditCards.SplitPayments = cr.SplitCredit == "Y" ? BoYesNoEnum.tYES: BoYesNoEnum.tNO;
                        payment.CreditCards.Add();
                    }
                    
                }

                retCode = payment.Add();
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
                System.Runtime.InteropServices.Marshal.ReleaseComObject(payment);
                throw new Exception(GlobalInstance.Instance.SBOErrorMessage == null ? ex.Message : GlobalInstance.Instance.SBOErrorMessage);
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(payment);
            }
        }

        public int AddMultiple(List<oPayment> objs)
        {
            throw new NotImplementedException();
        }
      
        public Task<List<oPayment>> GetList(Func<oPayment, bool> fltr)
        {
            try
            {
                List<oPayment> payments = new List<oPayment>();

                if (SqlObject != null)
                {
                    var pDictionary = new Dictionary<int, oPayment>();
                    var _p = SqlObject.QueryAsync<oPayment, oPaymentLine, oCheckPayment, oCreditPayment, oPayment>("sp_getPayments", map: (p, pl, chkp, crp) => {
                        oPayment paymnt = null;

                        if(pDictionary.TryGetValue(p.DocNo, out paymnt))
                        {
                            paymnt = p;
                        }

                    }, param: null, transaction: null, buffered: true, splitOn: "DocEntry", commandTimeout: null, commandType: System.Data.CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int Update(oPayment obj)
        {
            throw new NotImplementedException();
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
