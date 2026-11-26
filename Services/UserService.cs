using MiniShop.Domain.Entities;
using MiniShop.Domain.Enums;
using MiniShop.Repository.Contracts;
using MiniShop.Services.Contracts;

namespace MiniShop.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public User? FindByUsername(string username)
    {
        return _userRepository.FindByUsername(username);
    }

    public User? Register(string username, string fullName, string password)
    {
        if (username.Length < 3 || fullName.Length == 0 || password.Length < 4)
            return null;

        if (FindByUsername(username) != null)
            return null;

        User user = new User(_userRepository.GetNextId(), username, fullName,
            password, UserRole.Customer);

        _userRepository.Add(user);
        return user;
    }

    public List<User> GetAllUsers(User? admin)
    {
        if (admin == null || admin.Role != UserRole.Admin)
            return new List<User>();

        return _userRepository.GetAll();
    }


}
