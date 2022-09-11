namespace Kodlama.io.Devs.Application.Features.GithubProfiles.Dtos;

public class HardDeletedGithubProfileDto
{
    public int Id { get; set; }
    public string GithubProfileUrl { get; set; }
    public int UserId { get; set; }
}