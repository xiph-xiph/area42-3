using Backend_Area42_3.DTO.Input;
using Backend_Area42_3.DTO.Output;
using Backend_Area42_3.Enums;
using Backend_Area42_3.Models;
using Backend_Area42_3.Repositories;

namespace Backend_Area42_3.Services;

public class AuthService(
    IUserRepository userRepository,
    IPasswordHasher passwordHasher,
    ITokenGenerator tokenGenerator
)
{
    private readonly IUserRepository userRepository = userRepository;
    private readonly IPasswordHasher passwordHasher = passwordHasher;
    private readonly ITokenGenerator tokenGenerator = tokenGenerator;

    public async Task<SuccessMessageDto> Register(RegisterDto registerDto)
    {
        User? existingUser = await userRepository.GetUserByEmail(registerDto.Email);
        if (existingUser != null)
        {
            return new SuccessMessageDto
            {
                Success = false,
                Message = "Gebruiker met dit e-mailadres bestaat al.",
            };
        }

        if (PasswordIsWeak(registerDto.Password))
        {
            return new SuccessMessageDto
            {
                Success = false,
                Message = "Wachtwoord voldoet niet aan de vereisten.",
            };
        }

        string hashedPassword = passwordHasher.HashPassword(registerDto.Password);

        await userRepository.CreateUser(
            new User
            {
                Phone = registerDto.Phone,
                Name = registerDto.Name,
                Role = UserRole.Customer,
                Email = registerDto.Email,
                PasswordHash = hashedPassword,
            }
        );

        return new SuccessMessageDto { Success = true, Message = "Registratie geslaagd." };
    }

    private static bool PasswordIsWeak(string password)
    {
        // Check if the password is at least 8 characters long
        if (password.Length < 8)
        {
            return true;
        }

        // Check if the password contains at least one uppercase letter
        if (!password.Any(char.IsUpper))
        {
            return true;
        }

        // Check if the password contains at least one lowercase letter
        if (!password.Any(char.IsLower))
        {
            return true;
        }

        // Check if the password contains at least one digit
        if (!password.Any(char.IsDigit))
        {
            return true;
        }

        // Check if the password contains at least one special character
        if (!password.Any(ch => !char.IsLetterOrDigit(ch)))
        {
            return true;
        }

        return false;
    }

    public async Task<TokenDto> Login(LoginDto loginDto)
    {
        User? user = await userRepository.GetUserByEmail(loginDto.Email);
        if (user == null)
        {
            return new TokenDto
            {
                Token = null,
                ValidUntil = null,
                Message = "Ongeldig e-mailadres of wachtwoord.",
                Success = false,
            };
        }

        bool passwordValid = passwordHasher.VerifyPassword(loginDto.Password, user.PasswordHash);
        if (!passwordValid)
        {
            return new TokenDto
            {
                Token = null,
                ValidUntil = null,
                Message = "Ongeldig e-mailadres of wachtwoord.",
                Success = false,
            };
        }

        string token = tokenGenerator.GenerateToken(user.Id, user.Role);
        return new TokenDto
        {
            Token = token,
            ValidUntil = DateTime.UtcNow.AddHours(1).ToString("yyyy-MM-ddTHH:mm:ssZ"),
            Message = "Inloggen geslaagd.",
            Success = true,
        };
    }
}
