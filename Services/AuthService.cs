using MiniShop.Domain.Entities;
using MiniShop.Services.Contracts;

namespace MiniShop.Services;

public class AuthService : IAuthService
{
    private readonly IUserService _userService;

    // این Service به جای ساخت UserService، آن را از Program دریافت می کند.
    public AuthService(IUserService userService)
    {
        _userService = userService;
    }

    public User Login(string username, string password)
    {
        User user = _userService.FindByUsername(username);

        if (user == null)
            return null;

        if (!user.CheckPassword(password))
            return null;

        return user;
    }

    public User Register(string username, string fullName, string password)
    {
        return _userService.Register(username, fullName, password);
    }

    public bool ChangePassword(User user, string oldPassword, string newPassword)
    {
        if (user == null)
            return false;

        return user.ChangePassword(oldPassword, newPassword);
    }
}
