using MyRepos.Domain.Dictionary;
using MyRepos.Domain.DTOs;
using MyRepos.Domain.Entities;
using MyRepos.Domain.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRepos.Infrastructure.Repositories
{
    public class OAuthRepository : IOAuthRepository
    {
        private readonly IBaseService _baseService;
        public OAuthRepository(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<GithubProfile> AuthUser(string token)
        {
            try
            {
                GithubProfile result = new GithubProfile();
                ResponseDto? response = await _baseService.SendAsync(new RequestDto
                {
                    ApiType = Dictionary.ApiType.GET,
                    Url = Dictionary.GitHubAPIBase + "/user?access_token=" + token
                });
                if (response != null && response.IsSuccess)
                {
                    result = JsonConvert.DeserializeObject<GithubProfile>(response.Result);
                }
                else
                {
                    throw new Exception(response?.Message);
                }
                return result;
            }
            catch 
            {
                throw;
            }
        }

        public async Task<GithubOauthResponse> GetToken(string code)
        {
            try
            {
                GithubOauthResponse result = new GithubOauthResponse();
                var data = new
                {
                    client_id = Dictionary.ClientId,
                    client_secret = Dictionary.ClientSecret,
                    code,
                };
                ResponseDto? response = await _baseService.SendAsync(new RequestDto
                {
                    ApiType = Dictionary.ApiType.POST,
                    Url = Dictionary.OAuthURL + "/login/oauth/access_token",
                    Data = data
                }, false);
                if (response != null && response.IsSuccess)
                {
                    result = JsonConvert.DeserializeObject<GithubOauthResponse>(response.Result);
                }
                else
                {
                    throw new Exception(response?.Message);
                }
                return result;
            }
            catch
            {
                throw;
            }
        }
    }
}
