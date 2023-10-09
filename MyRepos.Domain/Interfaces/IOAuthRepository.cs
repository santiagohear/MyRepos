using MyRepos.Domain.DTOs;
using MyRepos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRepos.Domain.Interfaces
{
    public interface IOAuthRepository
    {
        Task<GithubOauthResponse> GetToken(string code);
        Task<GithubProfile> AuthUser(string token);
    }
}
