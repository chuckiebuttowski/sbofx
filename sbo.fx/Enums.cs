using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbo.fx
{
    public enum DBType
    {
        MSSQL2005 = 2005,
        MSSQL2008 = 2008,
        MSSQL2012 = 2012,
        MSSQL2014 = 2014,
        MSSQL2016 = 2016
    }

    public enum InvoiceType
    {
       AccountsPayable = 0,
       AccountsReceivable = 1
        
    }

    public enum InventoryType
    {
        In = 0,
        Out = 1
    }

    public enum SboTransactionType
    {
        JE = 30,
        APINV = 18,
        ARINV = 13,
        OP = 46,
        IP = 24,
        GI = 60,
        GR = 59,
        GRPO = 20,
        ITM = 4,
        BP = 2,
        GL = 1
    }
}
