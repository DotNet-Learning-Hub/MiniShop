using MiniShop.Domain.Entities;

namespace MiniShop.Repository.Contracts;

public interface IUserRepository
{
    List<User> GetAll();
    User? FindByUsername(string username);
    int GetNextId();
    bool Add(User user);
    bool Update(User user);
}
