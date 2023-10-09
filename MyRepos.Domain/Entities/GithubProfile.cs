using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRepos.Domain.Entities
{
    public class GithubProfile
    {
        public string Login { get; set; }
        public string Id { get; set; }
        public string AvatarUrl { get; set; }
        public string GravatarId { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public string Blog { get; set; }
        public string Location { get; set; }
        public string Email { get; set; }
        public bool? Hireable { get; set; }
        public string Bio { get; set; }
        public int? PublicRepos { get; set; }
        public int? PublicGists { get; set; }
        public int? Followers { get; set; }
        public int? Following { get; set; }
        public string HtmlUrl { get; set; }
        public string CreatedAt { get; set; }
        public string Type { get; set; }
    }
}
