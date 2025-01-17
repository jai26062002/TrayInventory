using TrayInventoryApp.Models;
namespace TrayInventoryApp.Services
{
    public interface ITransactionService
    {
        List<Transaction> GetTransactionsByShopId(int shopId);
        void AddTransaction(Transaction transaction);
    }
}
