namespace Tests;

public class Helpers
{
    public static string GenerateRandomPhoneNumber()
    {
        Random random = new();
        return $"06{random.Next(10000000, 99999999)}";
    }

    public static string GenerateRandomName()
    {
        return $"Test User {Guid.NewGuid()}";
    }

    public static string GenerateRandomPassword()
    {
        return $"Password{Guid.NewGuid()}";
    }
}