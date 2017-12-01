using sbo.fx.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbo.fx.Interfaces
{
    public interface IItemRepository: IRepository<oItem>
    {
        Task<oItem> GetItemByItemCode(string itemCode);
        Task<List<oItem>> GetByItemGroup(int groupCode);
        Task<List<oItem>> GetByItemSeries(int series);
    }
}
