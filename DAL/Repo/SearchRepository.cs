using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using DAL.EF;

namespace DAL.Repo
{
    public class SearchRepository : ISearchRepository
    {
        private readonly AlgoLoanDbContext _context;

        public SearchRepository(AlgoLoanDbContext context)
        {
            _context = context;
        }

        public void Add(Search obj)
        {
            try
            {
                _context.Searches.Add(obj);
            }
            catch (Exception e)
            {
                
            }
        }

        public List<Search> GetAll()
        {
            try
            {
                var result = _context.Searches;
                return result.ToList();
            }
            catch (Exception e)
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
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly AlgoLoanDbContext _context;

        public SubscriptionRepository(AlgoLoanDbContext context)
        {
            _context = context;
        }

        public void Add(Subscription obj)
        {
            try
            {
                _context.Searches.Add(obj);
            }
            catch (Exception e)
            {
                
            }
        }

        public List<Subscription> GetAll()
        {
            try
            {
                var result = _context.Subscriptions;
                return result.ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public bool[] CheckUserSubscription(string id)
        {
            bool[] result = new[] {false, false};
            try
            {
                var sub = _context.Subscriptions
                    .FirstOrDefault(s => s.userId.Equals(id));
                //Checking if user exist in subscription table
                if (sub == null)
                {
                    return result;
                }

                result[0] = true;
                if (sub.endDate > DateTime.Now)
                {
                    result[1] = true; 
                    return result;
                }

                return result;
            }
            catch (Exception e)
            {
                return result;
            }
        }

        public Subscription GetById(string id)
        {
            try
            {
                var result = _context.Subscriptions
                    .FirstOrDefault(s => s.userId.Equals(id));
                return result;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public void Update(Subscription obj)
        {
            _context.Subscriptions.Attach(obj);
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