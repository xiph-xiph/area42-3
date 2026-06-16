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
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IPasswordHasher _passwordHasher = passwordHasher;
    private readonly ITokenGenerator _tokenGenerator = tokenGenerator;

    public async Task<SuccessMessageDto> Register(RegisterDto registerDto)
    {
        User? existingUser = await _userRepository.GetUserByEmail(registerDto.Email);
        if (existingUser != null)
        {
            return new SuccessMessageDto
            {
                Success = false,
                Message = "Gebruiker met dit e-mailadres bestaat al.",
            };
        }

        string hashedPassword = _passwordHasher.HashPassword(registerDto.Password);

        await _userRepository.CreateUser(
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

    public async Task<TokenDto> Login(LoginDto loginDto)
    {
        User? user = await _userRepository.GetUserByEmail(loginDto.Email);
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

        bool passwordValid = _passwordHasher.VerifyPassword(loginDto.Password, user.PasswordHash);
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

        string token = _tokenGenerator.GenerateToken(user.Id, user.Role);
        return new TokenDto
        {
            Token = token,
            ValidUntil = DateTime.UtcNow.AddHours(1).ToString("yyyy-MM-ddTHH:mm:ssZ"),
            Message = "Inloggen geslaagd.",
            Success = true,
        };
    }
}
