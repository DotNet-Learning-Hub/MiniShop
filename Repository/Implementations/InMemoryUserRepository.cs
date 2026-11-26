using MiniShop.Data;
using MiniShop.Domain.Entities;
using MiniShop.Repository.Contracts;

namespace MiniShop.Repository.Implementations;

public class InMemoryUserRepository : IUserRepository
{
    protected readonly List<User> Users;

    public InMemoryUserRepository()
    {
        Users = SeedData.CreateUsers();
    }

    protected InMemoryUserRepository(List<User> users)
    {
        Users = users;
    }

    public List<User> GetAll()
    {
        return Users;
    }

    public User? FindByUsername(string username)
    {
        foreach (User user in Users)
        {
            if (user.Username == username)
                return user;
        }

        return null;
    }

    public int GetNextId()
    {
        int largestId = 0;
        foreach (User user in Users)
        {
            if (user.Id > largestId)
                largestId = user.Id;
        }

        return largestId + 1;
    }

    public bool Add(User user)
    {
        Users.Add(user);
        return true;
    }

    public bool Update(User user)
    {
        return true;
    }
}
