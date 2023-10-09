using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyRepos.Domain.DTOs;
using MyRepos.Domain.Entities;
using MyRepos.Domain.Interfaces;
using MyRepos.Presentation.Models;

namespace MyRepos.Presentation.Controllers
{
    [Authorize]
    public class RepositoriesController : Controller
    {
        private readonly IGitHubRepository _githubRepository;
        private readonly UserManager<User> _userManager;
        private readonly ITokenProvider _tokenProvider;
        private readonly IOAuthRepository _oauthRepository;

        public RepositoriesController( IGitHubRepository repository, 
            UserManager<User> userManager,
            ITokenProvider tokenProvider,
            IOAuthRepository oAuthRepository)
        {
            _githubRepository = repository;
            _userManager = userManager;
            _tokenProvider = tokenProvider;
            _oauthRepository = oAuthRepository;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                User user = await _userManager.GetUserAsync(User);
                if (!_tokenProvider.GetToken().IsNullOrEmpty() || !user.GitHubUsername.IsNullOrEmpty())
                {
                    return RedirectToAction(nameof(MyRepos));
                }
            }
            catch(Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return View();
        }

        public async Task<IActionResult> MyRepos()
        {
            try
            {
                User user = await _userManager.GetUserAsync(User);
                RepositoriesViewModel repos = new RepositoriesViewModel();
                repos.allMyRepos = await _githubRepository.GetAllRepos();
                if(user!=null && !user.GitHubUsername.IsNullOrEmpty())
                {
                    repos.starredRepos = await _githubRepository.GetStarredRepos(user.GitHubUsername);
                }
                else
                {
                    throw new Exception("There's not github username configured or user doesn't exists");
                }
                return View(repos);
            }
            catch (Exception ex)
            {

                TempData["Error"] = ex.Message;
                return View("Index");
            }
        }

        public async Task<IActionResult> starRepo(string repo)
        {
            try
            {
                User user = await _userManager.GetUserAsync (User);
                if (user == null)
                {
                    throw new Exception("We couldn't consult the user");
                }
                if(user.GitHubUsername.IsNullOrEmpty())
                {
                    throw new Exception("Your account hasn't a github user configured");
                }
                await _githubRepository.StarRepo(user.GitHubUsername, repo);
                return RedirectToAction(nameof(MyRepos));
            }
            catch(Exception ex) 
            {
                TempData["Error"] = ex.Message;
                return View("Index");
            }
        }
        public async Task<IActionResult> unstarRepo(string repo)
        {
            try
            {
                User user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    throw new Exception("We couldn't consult the user");
                }
                if (user.GitHubUsername.IsNullOrEmpty())
                {
                    throw new Exception("Your account hasn't a github user configured");
                }
                await _githubRepository.UnstarRepo(user.GitHubUsername, repo);
                return RedirectToAction(nameof(MyRepos));
            }
            catch(Exception ex) 
            {
                TempData["Error"] = ex.Message;
                return View("Index");
            }
        }

        public async Task<IActionResult> Callback(string code) 
        {
            try
            {
                GithubOauthResponse response = await _oauthRepository.GetToken(code);
                if(response.access_token == null)
                {
                    throw new Exception("We couldn't process your request, try later");
                }
                _tokenProvider.SetToken(response.access_token);
                GithubProfile githubUser = await _oauthRepository.AuthUser(response.access_token);
                await UpdateUser(githubUser, response.access_token);
                return RedirectToAction(nameof(MyRepos));
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return View("Index");
            }
        }

        private async Task UpdateUser(GithubProfile githubUser, string token)
        {
            try
            {
                User user = await _userManager.GetUserAsync(User);
                if(user == null)
                {
                    throw new Exception("Cannot update user");
                }
                user.GitHubUsername = githubUser.Login;
                user.PersonalToken = token;

                var result = await _userManager.UpdateAsync(user);
                if(!result.Succeeded)
                {
                    throw new Exception("Cannot update user");
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
