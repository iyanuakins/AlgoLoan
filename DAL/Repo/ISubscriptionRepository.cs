using System.Collections.Generic;
using DAL.EF;

namespace DAL.Repo
{
    public interface ISubscriptionRepository
    {
        void Add(Subscription obj);
        List<Subscription> GetAll();
        bool[] CheckUserSubscription(string id);
        Subscription GetById(string id);
        void Update(Subscription obj);
        bool Save();
    }
}