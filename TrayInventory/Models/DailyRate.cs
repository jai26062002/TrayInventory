using System.ComponentModel.DataAnnotations;

namespace TrayInventoryApp.Models
{
    public class DailyRate
    {
        [Key]
        public int RateID { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; } = DateTime.Today;

        [Required]
        [Range(0.01, 1000.00, ErrorMessage = "Rate must be between 0.01 and 1000.00")]
        public decimal EggRate { get; set; }
    }
}
