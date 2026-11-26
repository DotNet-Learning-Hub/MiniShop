using MiniShop.Domain.Entities;

namespace MiniShop.Services.Contracts;

public interface IAuthService
{
    User? Login(string username, string password);
    User? Register(string username, string fullName, string password);
    bool ChangePassword(User? user, string oldPassword, string newPassword);
}
