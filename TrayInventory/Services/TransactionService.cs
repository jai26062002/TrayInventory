
using TrayInventoryApp.Data;
using TrayInventoryApp.Models;
namespace TrayInventoryApp.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly AppDbContext _context;

        public TransactionService(AppDbContext context)
        {
            _context = context;
        }

        public List<Transaction> GetTransactionsByShopId(int shopId)
        {
            return _context.Transaction
                           .Where(dt => dt.ShopID == shopId)
                           .ToList();
        }

        public void AddTransaction(Transaction transaction)
        {
            _context.Transaction.Add(transaction);
            _context.SaveChanges();
        }
    }
}
