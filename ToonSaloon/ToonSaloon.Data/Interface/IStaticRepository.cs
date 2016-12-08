using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToonSaloon.Models;

namespace ToonSaloon.Data.Interface
{
    public interface IStaticRepository
    {
        StaticPage GetPageByID(int id);

        List<StaticPage> GetAllPages();

        void AddStaticPage(StaticPage pageToAdd);

        void EditStaticPage(StaticPage pageToEdit);

        void RemoveStaticPage(StaticPage pageToRemove);

        void DeleteTagStaticBridgeTable(StaticPage id);

        void InsertTagStaticBridgeTable(int tagId, int pageId);
    }
}
