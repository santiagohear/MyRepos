using MyRepos.Domain.Entities;

namespace MyRepos.Presentation.Models
{
    public class RepositoriesViewModel
    {
        public List<Repository> allMyRepos {  get; set; }
        public List<Repository> starredRepos { get; set; }
    }
}
