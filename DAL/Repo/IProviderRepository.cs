using System.Collections.Generic;
using DAL.EF;

namespace DAL.Repo
{
    public interface IProviderRepository
    {
        List<Provider> GetProviders(int amount, int duration, string type);
        Provider GetById(int id);
        User GetUserById(string id);
        bool Save();
    }
}