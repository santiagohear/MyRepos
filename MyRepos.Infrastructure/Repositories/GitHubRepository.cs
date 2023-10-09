using MyRepos.Domain.Dictionary;
using MyRepos.Domain.DTOs;
using MyRepos.Domain.Entities;
using MyRepos.Domain.Interfaces;
using Newtonsoft.Json;

namespace MyRepos.Infrastructure.Repositories
{
    public class GitHubRepository : IGitHubRepository
    {
        private readonly IBaseService _baseService;
        public GitHubRepository(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<List<Repository>> GetAllRepos() {
            try
            {
                List<Repository> result = new List<Repository>();
                ResponseDto? response = await _baseService.SendAsync(new RequestDto
                {
                    ApiType = Dictionary.ApiType.GET,
                    Url = Dictionary.GitHubAPIBase + "/user/repos"
                });
                if(response != null && response.IsSuccess) 
                {
                     result = JsonConvert.DeserializeObject<List<Repository>>(response.Result);
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

        public async Task<List<Repository>> GetStarredRepos(string user)
        {
            try
            {
                List<Repository> result = new List<Repository>();
                ResponseDto? response = await _baseService.SendAsync(new RequestDto
                {
                    ApiType = Dictionary.ApiType.GET,
                    Url = Dictionary.GitHubAPIBase + $"/users/{user}/starred"
                });
                if (response != null && response.IsSuccess)
                {
                    result = JsonConvert.DeserializeObject<List<Repository>>(response.Result);
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

        public async Task StarRepo(string owner, string repo)
        {
            try
            {
                List<Repository> result = new List<Repository>();
                ResponseDto? response = await _baseService.SendAsync(new RequestDto
                {
                    ApiType = Dictionary.ApiType.PUT,
                    Url = Dictionary.GitHubAPIBase + $"/user/starred/{owner}/{repo}"
                });
                if (response != null && response.IsSuccess)
                {
                    result = JsonConvert.DeserializeObject<List<Repository>>(response.Result);
                }
                else
                {
                    throw new Exception(response?.Message);
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task UnstarRepo(string owner, string repo)
        {
            try
            {
                List<Repository> result = new List<Repository>();
                ResponseDto? response = await _baseService.SendAsync(new RequestDto
                {
                    ApiType = Dictionary.ApiType.DELETE,
                    Url = Dictionary.GitHubAPIBase + $"/user/starred/{owner}/{repo}"
                });
                if (response != null && response.IsSuccess)
                {
                    result = JsonConvert.DeserializeObject<List<Repository>>(response.Result);
                }
                else
                {
                    throw new Exception(response?.Message);
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
