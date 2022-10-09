using Core.Security.Entities;
using Kodlama.io.Devs.Application.Features.UserOperationClaims.Dtos;

namespace Kodlama.io.Devs.Application.Features.UserOperationClaims.Models;

public class GetUserOperationClaimListModel
{
    public IList<UserOperationClaimDto> Items { get; set; }
}