using TrayInventoryApp.Models;

namespace TrayInventoryApp.Services
{
    public interface IDailyRateService
    {
        IEnumerable<DailyRate> GetAllRates();
        DailyRate GetRateByDate(DateTime date);
        void AddOrUpdateDailyRate(DailyRate dailyRate);
        void UpdateDailyRate(DailyRate dailyRate);
        decimal GetPriceForToday();
    }
}
