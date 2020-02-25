using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.EF;

namespace DAL.Repo
{
    public interface ISearchRepository
    {
        void Add(Search obj);
        List<Search> GetAll();
        bool Save();
    }
}
