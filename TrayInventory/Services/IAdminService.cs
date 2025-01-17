using TrayInventoryApp.Models;

namespace TrayInventoryApp.Services
{
    public interface IAdminService
    {
        Task<Admin> AuthenticateAdminAsync(string username, string password);
    }
}
