using TrayInventoryApp.Data;
using TrayInventoryApp.Models;

namespace TrayInventoryApp.Services
{
    public class DailyRateService : IDailyRateService
    {
        private readonly AppDbContext _context;

        public DailyRateService(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<DailyRate> GetAllRates()
        {
            return _context.DailyRate.ToList();
        }
        public decimal GetPriceForToday()
        {
            // Get today's date
            var today = DateTime.Now.Date;

            // Try to get today's price from the database
            var todayPrice = _context.DailyRate
                                     .FirstOrDefault(rate => rate.Date == today);

            if (todayPrice != null)
            {
                // If today's price is available, return it
                return todayPrice.EggRate;
            }
            else
            {
                // If today's price is not available, fetch yesterday's price
                var yesterday = today.AddDays(-1);  // Get yesterday's date

                // Try to fetch yesterday's price from the database
                var yesterdayPrice = _context.DailyRate
                                              .FirstOrDefault(rate => rate.Date == yesterday);

                if (yesterdayPrice != null)
                {
                    // If yesterday's price is available, return it
                    return yesterdayPrice.EggRate;
                }
                else
                {
                    // If no price is available for both today and yesterday, return a default price
                    return 6.00M; // Default value if no record is found for today or yesterday
                }
            }
        }
            public DailyRate GetRateByDate(DateTime date)
        {
            return _context.DailyRate.FirstOrDefault(r => r.Date == date);
        }

        public void AddOrUpdateDailyRate(DailyRate dailyRate)
        {
            var existingRate = _context.DailyRate.FirstOrDefault(r => r.Date == dailyRate.Date);
            if (existingRate != null)
            {
                // Update the existing rate
                existingRate.EggRate = dailyRate.EggRate;
                _context.DailyRate.Update(existingRate);
            }
            else
            {
                // Add a new rate
                _context.DailyRate.Add(dailyRate);
            }
            _context.SaveChanges();
        }


        public void UpdateDailyRate(DailyRate dailyRate)
        {
            _context.DailyRate.Update(dailyRate);
            _context.SaveChanges();
        }
    }
}
