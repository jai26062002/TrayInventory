namespace TrayInventoryApp.Models
{
    public class TrayInventorys
    {
        public int TrayInventoryID { get; set; }
       // public DateTime Date { get; set; }
        public int TraysReceived { get; set; }
        public int TraysIssued { get; set; }
        public int AdminID { get; set; }

        public Admin Admin { get; set; }
    }
}
