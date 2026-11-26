using MiniShop.Domain.Entities;
using MiniShop.Repository.Contracts;
using MiniShop.Services.Contracts;

namespace MiniShop.Services;

public class AuthService : IAuthService
{
    private readonly IUserService _userService;
    private readonly IUserRepository _userRepository;
    
    public AuthService(IUserService userService, IUserRepository userRepository)
    {
        _userService = userService;
        _userRepository = userRepository;
    }

    public User? Login(string username, string password)
    {
        User? user = _userService.FindByUsername(username);

        if (user == null)
            return null;

        if (!user.CheckPassword(password))
            return null;

        return user;
    }

    public User? Register(string username, string fullName, string password)
    {
        return _userService.Register(username, fullName, password);
    }

    public bool ChangePassword(User? user, string oldPassword, string newPassword)
    {
        if (user == null)
            return false;

        if (!user.ChangePassword(oldPassword, newPassword))
            return false;

        return _userRepository.Update(user);
    }
}
