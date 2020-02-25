using System.Collections.Generic;
using DAL.EF;

namespace DAL.Repo
{
    public interface IVisitRepository
    {
        void Add(Visit obj);
        List<Visit> GetAll();
        bool IsUniqueVisit(string id, int providerId);
        bool Save();
    }
}