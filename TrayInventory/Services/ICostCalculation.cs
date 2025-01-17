namespace TrayInventoryApp.Services
{
    public interface ICostCalculation
    {
        decimal TotalAmountReceivedToday();

        // Method to calculate the total amount received grouped by day
        Dictionary<DateTime, decimal> TotalAmountReceivedByDay();
    }
}
