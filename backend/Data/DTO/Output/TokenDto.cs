namespace Backend_Area42_3.DTO.Output;

public class TokenDto : SuccessMessageDto
{
    public required string? Token { get; set; }

    public required string? ValidUntil { get; set; }
}
