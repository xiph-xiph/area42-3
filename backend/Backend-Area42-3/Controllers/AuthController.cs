using Backend_Area42_3.DTO.Input;
using Backend_Area42_3.DTO.Output;
using Backend_Area42_3.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Area42_3.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(AuthService _authService) : ControllerBase
{
    private readonly AuthService authService = _authService;

    [HttpPost("register")]
    public async Task<SuccessMessageDto> Register(RegisterDto registerDto)
    {
        return await authService.Register(registerDto);
    }

    [HttpPost("login")]
    public async Task<TokenDto> Login(LoginDto loginDto)
    {
        return await authService.Login(loginDto);
    }
}
