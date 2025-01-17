namespace TrayInventoryApp.Models
{
    public class CostCalculationViewModel
    {
        public DateTime TodayDate { get; set; } // Date for today
        public decimal TotalCostToday { get; set; } // Total cost for today
        public List<DayCost> DailyCost { get; set; } // List of total costs by day
    }

    public class DayCost
    {
        public DateTime Date { get; set; }
        public decimal TotalCost { get; set; }
    }
}
