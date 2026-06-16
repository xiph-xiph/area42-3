using Backend_Area42_3.DTO.Input;
using Backend_Area42_3.DTO.Output;
using Backend_Area42_3.Enums;
using Backend_Area42_3.Models;
using Backend_Area42_3.Repositories;

namespace Backend_Area42_3.Services;

public class AuthService(IUserRepository userRepository)
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<SuccessMessageDto> Register(
        RegisterDto registerDto,
        IPasswordHasher passwordHasher
    )
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

        string hashedPassword = passwordHasher.HashPassword(registerDto.Password);

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
        throw new NotImplementedException();
    }
}
