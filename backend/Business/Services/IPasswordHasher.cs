namespace Backend_Area42_3.Services;

public interface IPasswordHasher
{
    string HashPassword(string password);

    bool VerifyPassword(string password, string stored);
}
