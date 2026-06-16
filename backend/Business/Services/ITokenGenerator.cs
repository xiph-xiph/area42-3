using Backend_Area42_3.Enums;

namespace Backend_Area42_3.Services;

public interface ITokenGenerator
{
    string GenerateToken(int userId, UserRole role);
}
