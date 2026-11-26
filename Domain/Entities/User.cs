using MiniShop.Domain.Enums;

namespace MiniShop.Domain.Entities;

public class User
{
    public string Password { get; private set; }

    public int Id { get; }
    public string Username { get; }
    public string FullName { get; }
    public UserRole Role { get; }

    public User(int id, string username, string fullName, string password, UserRole role)
    {
        Id = id;
        Username = username;
        FullName = fullName;
        Password = password;
        Role = role;
    }

    // رفتار مربوط به رمز همین کاربر، داخل خود Entity قرار گرفته.
    public bool CheckPassword(string password)
    {
        return Password == password;
    }

    public bool ChangePassword(string oldPassword, string newPassword)
    {
        if (!CheckPassword(oldPassword))
            return false;

        if (newPassword.Length < 4)
            return false;

        Password = newPassword;
        return true;
    }
}
