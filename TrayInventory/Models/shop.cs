using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace TrayInventoryApp.Models
{
    public class Shop
    {
        [Key]
        public int ShopID { get; set; }

        [Required]
        public string ShopName { get; set; }

        public decimal PreviousPending { get; set; } = 0.00M;

        [ValidateNever]
        public ICollection<Transaction>? Transactions { get; set; }
    }
}

