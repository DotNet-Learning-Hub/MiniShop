using MiniShop.Domain.Enums;

namespace MiniShop.Domain.Entities;

public class User
{
    private string _password;
    public int Id { get; set; }
    public string  UserName { get; set; }
    public string FullName { get; set; }
    public UserRole UserRole { get; set; }

    public User(int id, string userName, string fullName, string password, UserRole role)
    {
        Id = id;
        UserName = userName;
        FullName = fullName;
        _password = password;
        UserRole = role;

    }

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