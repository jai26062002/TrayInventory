using Microsoft.EntityFrameworkCore;
using TrayInventoryApp.Data;
using TrayInventoryApp.Models;

namespace TrayInventoryApp.Services
{
    public class AdminService : IAdminService
    {
        private readonly AppDbContext _context;

        public AdminService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Admin> AuthenticateAdminAsync(string username, string password)
        {
            // Implement password hashing and comparison
            return await _context.Admins
                .FirstOrDefaultAsync(a => a.Username == username && a.Password == password);
        }
    }
}
