using Backend_Area42_3.DTO.Input;
using Backend_Area42_3.DTO.Output;
using Backend_Area42_3.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Area42_3.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(AuthService authService) : ControllerBase
{
    private readonly AuthService authService = authService;

    [HttpPost("register")]
    public async Task<ActionResult<SuccessMessageDto>> Register(RegisterDto registerDto)
    {
        var result = await authService.Register(registerDto);

        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<ActionResult<TokenDto>> Login(LoginDto loginDto)
    {
        var result = await authService.Login(loginDto);

        if (!result.Success)
        {
            return Unauthorized();
        }

        return Ok(result);
    }
}
