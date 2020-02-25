using System;
using System.Collections.Generic;
using System.Linq;
using DAL.EF;

namespace DAL.Repo
{
    public class VisitRepository : IVisitRepository
    {
        private readonly AlgoLoanDbContext _context;

        public VisitRepository(AlgoLoanDbContext context)
        {
            _context = context;
        }


        public void Add(Visit obj)
        {
            try
            {
                _context.Visits.Add(obj);
            }
            catch (Exception e)
            {

            }
        }

        public List<Visit> GetAll()
        {
            try
            {
                return _context.Visits.ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public bool IsUniqueVisit(string userId, int providerId)
        {
            try
            {
                bool isNotUnique = _context.Visits
                    .Any(v => v.userId.Equals(userId) &&
                              v.providerId == providerId);
                return !isNotUnique;
            }
            catch (Exception e)
            {
                return false;
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