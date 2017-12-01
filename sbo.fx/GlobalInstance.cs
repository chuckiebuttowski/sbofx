using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAPbobsCOM;
using System.Data.SqlClient;

namespace sbo.fx
{
    public class GlobalInstance
    {
        private static GlobalInstance instance = new GlobalInstance();

        private GlobalInstance()
        {

        }

        public static GlobalInstance Instance
        {
            get
            {
                return instance;
            }
        }

        public Company SboComObject;
        public SqlConnection SqlObject;

        public int SBOErrorCode { get; set; }
        public string SBOErrorMessage { get; set; }
        public string Server { get; set; }
        public string DBName { get; set; }
        public string DBUName { get; set; }
        public string DBPword { get; set; }
        public string UName { get; set; }
        public string Pword { get; set; }
        public DBType DatabaseServerType { get; set; }

        public bool IsConnected { get; set; }

        public void InitializeSboComObject()
        {
            SboComObject = new Company();

            switch (DatabaseServerType)
            {
                case DBType.MSSQL2005:
                    SboComObject.DbServerType = BoDataServerTypes.dst_MSSQL2005;
                    break;
                case DBType.MSSQL2008:
                    SboComObject.DbServerType = BoDataServerTypes.dst_MSSQL2008;
                    break;
                case DBType.MSSQL2012:
                    SboComObject.DbServerType = BoDataServerTypes.dst_MSSQL2012;
                    break;
                case DBType.MSSQL2014:
                    SboComObject.DbServerType = BoDataServerTypes.dst_MSSQL2014;
                    break;
            }

            SboComObject.Server = Server;
            SboComObject.CompanyDB = DBName;
            SboComObject.DbUserName = DBUName;
            SboComObject.DbPassword = DBPword;
            SboComObject.UserName = UName;
            SboComObject.Password = Pword;
            SboComObject.UseTrusted = false;
            SboComObject.language = BoSuppLangs.ln_English;

            if (SboComObject.Connect() != 0)
            {
                int errCode;
                string errMsg;

                SboComObject.GetLastError(out errCode, out errMsg);
                SBOErrorCode = errCode;
                SBOErrorMessage = errMsg;

                IsConnected = SboComObject.Connected;
            }
            else
            {
                IsConnected = SboComObject.Connected;
            }
        }

        public void InitializeSqlObject()
        {
            SqlObject = new SqlConnection();
            SqlObject.ConnectionString = ConnectionString;
            SqlObject.Open();
        }

        public void DisposeSboComObject()
        {
            SboComObject.Disconnect();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(SboComObject);
            SboComObject = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        public void DisposeSqlObject()
        {
            SqlObject.Close();
            SqlObject.Dispose();
        }

        public string ConnectionString
        {
            get
            {
                return string.Format(@"Data Source={0};User ID={1};Password={2};Initial Catalog={3};MultipleActiveResultSets=True;", Server, DBUName, DBPword, DBName);
            }
        }
        
    }
}
