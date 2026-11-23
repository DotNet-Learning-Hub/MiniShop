using MiniShop.Domain.Entities;

namespace MiniShop.Services.Contracts;

public interface IUserService
{
    User FindByUsername(string username);
    User Register(string username, string fullName, string password);
    List<User> GetAllUsers(User admin);
}
