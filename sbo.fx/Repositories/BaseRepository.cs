using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbo.fx.Repositories
{
    internal class BaseRepository
    {
        public Company SboComObject;
        public SqlConnection SqlObject;

        public void InitRepository(Company sboComObject)
        {
            SboComObject = sboComObject;
        }

        public void InitRepository(SqlConnection sqlObject)
        {
            SqlObject = sqlObject;
        }

        public void InitRepository(Company sboComObject, SqlConnection sqlObject)
        {
            SboComObject = sboComObject;
            SqlObject = sqlObject;
        }
    }
}
