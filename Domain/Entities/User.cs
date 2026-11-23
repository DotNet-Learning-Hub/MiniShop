using MiniShop.Domain.Enums;

namespace MiniShop.Domain.Entities;

public class User
{
    private string _password;

    public int Id { get; }
    public string Username { get; }
    public string FullName { get; }
    public UserRole Role { get; }

    public User(int id, string username, string fullName, string password, UserRole role)
    {
        Id = id;
        Username = username;
        FullName = fullName;
        _password = password;
        Role = role;
    }

    // رفتار مربوط به رمز همین کاربر، داخل خود Entity قرار گرفته.
    public bool CheckPassword(string password)
    {
        return _password == password;
    }

    public bool ChangePassword(string oldPassword, string newPassword)
    {
        if (!CheckPassword(oldPassword))
            return false;

        if (newPassword.Length < 4)
            return false;

        _password = newPassword;
        return true;
    }
}
