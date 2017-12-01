using sbo.fx.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbo.fx.Interfaces
{
    public interface IBusinessPartnerRepository: IRepository<oBusinessPartner>
    {
        Task<oBusinessPartner> GetByCardCode(string cardCode);
        Task<List<oBusinessPartner>> GetBySeries(int series);
        Task<List<oBusinessPartner>> GetByCardType(string cardType);
        Task<List<oBusinessPartner>> GetByGroupCode(int groupCode);
        Task<List<oBusinessPartner>> GetByStatus(string status);
    }
}
