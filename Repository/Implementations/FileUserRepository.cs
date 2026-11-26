using MiniShop.Data;
using MiniShop.Domain.Entities;
using MiniShop.Repository.Contracts;
using Newtonsoft.Json;

namespace MiniShop.Repository.Implementations;

public sealed class FileUserRepository : IUserRepository
{
    private readonly string _path;
    private readonly List<User> _users;

    public FileUserRepository(string path)
    {
        _path = path;
        _users = LoadUsers(path);
    }

    public List<User> GetAll()
    {
        return _users;
    }

    public User? FindByUsername(string username)
    {
        foreach (User user in _users)
        {
            if (user.Username == username)
                return user;
        }

        return null;
    }

    public int GetNextId()
    {
        int largestId = 0;
        foreach (User user in _users)
        {
            if (user.Id > largestId)
                largestId = user.Id;
        }

        return largestId + 1;
    }

    public bool Add(User user)
    {
        _users.Add(user);
        string json = JsonConvert.SerializeObject(_users, Formatting.Indented);
        File.WriteAllText(_path, json);
        return true;
    }

    public bool Update(User user)
    {
        string json = JsonConvert.SerializeObject(_users, Formatting.Indented);
        File.WriteAllText(_path, json);
        return true;
    }

    private static List<User> LoadUsers(string path)
    {
        if (!File.Exists(path))
            return SeedData.CreateUsers();

        string json = File.ReadAllText(path);
        List<User>? users = JsonConvert.DeserializeObject<List<User>>(json);
        return users ?? new List<User>();
    }
}
