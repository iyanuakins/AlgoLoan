using System.Collections.Generic;
using DAL.EF;

namespace DAL.Repo
{
    public interface IProviderRepository
    {
        void Add(Provider obj);
        List<Provider> GetProviders(int amount, int duration, string type);
        List<Provider> GetAllProviders();
        Provider GetById(int id);
        User GetUserById(string id);
        bool Save();
    }
}