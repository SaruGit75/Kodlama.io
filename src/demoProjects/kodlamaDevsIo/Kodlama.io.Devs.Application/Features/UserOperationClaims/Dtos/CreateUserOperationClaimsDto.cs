namespace Kodlama.io.Devs.Application.Features.UserOperationClaims.Dtos;

public class CreateUserOperationClaimsDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int OperationClaimId { get; set; }
}