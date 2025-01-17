namespace TrayInventoryApp.Models
{
    public class Admin
    {
        public int AdminID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; } // Store hashed password, not plain text
    }
}
