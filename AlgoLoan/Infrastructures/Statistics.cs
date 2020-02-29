using System;
using System.Collections.Generic;
using DAL.EF;

namespace AlgoLoan.Infrastructures
{
    public class Statistics
    {
        public Statistics()
        {
            Min = Decimal.MaxValue;
            Max = Decimal.MinValue;
            DurationCounts = new int[13];
            DurationCounts[0] = -1;
            TypesCount = new Dictionary<string, int>()
            {
                ["studentloan"] = 0,
                ["individualloan"] = 0,
                ["businessloan"] = 0
            };
        }

        public Statistics Accumulate(Search search)
        {
            Count += 1;
            Total += search.amount;
            Max = Math.Max(Max, search.amount);
            Min = Math.Min(Min, search.amount);
            DurationCounts[search.duration] += 1;
            TypesCount[$"{search.type}"] += 1;
            return this;
        }

        public Statistics Compute()
        {
            Average = Total / Count;
            return this;
        }
        public decimal Min { get; set; }
        public decimal Max { get; set; }
        public decimal Average { get; set; }
        public int Count { get; set; }
        public decimal Total { get; set; }
        public int[] DurationCounts { get; set; }
        public Dictionary<string, int> TypesCount { get; set; }

    }
}