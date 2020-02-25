using System.Collections.Generic;
using DAL.EF;

namespace DAL.Repo
{
    public interface IProviderRepository
    {
        List<Provider> GetProviders(int amount, int duration, string type);
        Provider GetProviderByLink(string link);
        User GetUserById(string id);
        bool Save();
    }
}