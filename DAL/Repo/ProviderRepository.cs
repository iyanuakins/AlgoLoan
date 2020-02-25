using System.Collections.Generic;
using System.Linq;
using DAL.EF;

namespace DAL.Repo
{
    public class ProviderRepository : IProviderRepository
    {
        private readonly AlgoLoanDbContext _context;

        public ProviderRepository(AlgoLoanDbContext context)
        {
            _context = context;
        }

        public List<Provider> GetProviders(int amount, int duration, string type)
        {
            try
            {
                var result = _context.Providers.Where(p =>
                        p.minAmount <= amount &&
                        p.maxAmount >= amount &&
                        p.minDuration <= duration &&
                        p.maxDuration >= duration)
                    .Select(p => p).ToList();
                if (result.Count < 1)
                {
                    return null;
                }
                return result;
            }
            catch
            {
                return null;
            }

        }

        public Provider GetProviderByLink(string link)
        {
            try
            {
                var query = _context.Providers.Where(p => p.link == link)
                    .Select(p => p).FirstOrDefault();
                return query;
            }
            catch
            {
                return null;
            }

        }

        public User GetUserById(string id)
        {
            try
            {
                var loggedInUser = _context.Users.Find(id);
                return loggedInUser;
            }
            catch
            {
                return null;
            }
        }

        public bool Save()
        {
            try
            {
                int affectedRow = _context.SaveChanges();
                return affectedRow > 0;
            }
            catch
            {
                return false;
            }

        }
    }
}
