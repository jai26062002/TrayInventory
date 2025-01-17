namespace TrayInventoryApp.Models
{
    public class Transaction
    {
        public int TransactionID { get; set; }
        public DateTime Date { get; set; }
        public int ShopID { get; set; }
        public int TrayCount { get; set; }
        public decimal Cost { get; set; }
        public decimal AmountReceived { get; set; }
        public decimal PendingAmount { get; set; }

        // Navigation property for related shop
        public Shop Shop { get; set; }
    }
}
