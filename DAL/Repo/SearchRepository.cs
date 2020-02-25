using System;
using System.Collections.Generic;
using System.Linq;
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
}