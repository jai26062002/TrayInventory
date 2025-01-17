using TrayInventoryApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TrayInventoryApp.Services
{
    public class CostCalculation : ICostCalculation
    {
        private readonly AppDbContext _context;

        public CostCalculation(AppDbContext context)
        {
            _context = context;
        }

        public decimal TotalAmountReceivedToday()
        {
            var today = DateTime.Today;
            var totalReceivedToday = _context.Transaction
                .Where(t => t.Date.Date == today)
                .Sum(t => t.AmountReceived);

            return totalReceivedToday;
        }

        public Dictionary<DateTime, decimal> TotalAmountReceivedByDay()
        {
            var dailyAmount = _context.Transaction
                .GroupBy(t => t.Date.Date)
                .Select(g => new
                {
                    Date = g.Key,
                    TotalAmountReceived = g.Sum(t => t.AmountReceived)
                })
                .ToDictionary(g => g.Date, g => g.TotalAmountReceived);

            return dailyAmount;
        }
    }
}
