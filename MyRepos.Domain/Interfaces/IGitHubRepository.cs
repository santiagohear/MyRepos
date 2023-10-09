using MyRepos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRepos.Domain.Interfaces
{
    public interface IGitHubRepository
    {
        Task<List<Repository>> GetAllRepos();
        Task<List<Repository>> GetStarredRepos(string user);
        Task StarRepo(string owner, string repo);
        Task UnstarRepo(string user, string repo);
    }
}
