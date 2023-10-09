using Microsoft.AspNetCore.Identity;

namespace MyRepos.Presentation.Models
{
    public class User : IdentityUser
    {
        public string? GitHubUsername { get; set; }
        public string? PersonalToken { get; set; }
    }
}
