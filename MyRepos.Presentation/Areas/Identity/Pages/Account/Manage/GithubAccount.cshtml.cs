using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using MyRepos.Domain.Interfaces;
using MyRepos.Presentation.Models;
using System.ComponentModel.DataAnnotations;

namespace MyRepos.Presentation.Areas.Identity.Pages.Account.Manage
{
    public class GithubAccountModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenProvider _tokenProvider;

        public GithubAccountModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ITokenProvider tokenProvider)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenProvider = tokenProvider;
        }
        [TempData]
        public string StatusMessage { get; set; }
        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Display(Name = "GitHub User")]
            public string? GithubUsername { get; set; }
            [Display(Name = "Personal Access Token")]
            public string? AccessToken { get; set; }
        }

        private async Task LoadAsync(User user)
        {
            Input = new InputModel
            {
                GithubUsername = user.GitHubUsername,
                AccessToken = user.PersonalToken
            };
        }
        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            User gituser = await _userManager.GetUserAsync(User);
            if (gituser == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(gituser);
                return Page();
            }

            
            gituser.GitHubUsername = Input.GithubUsername;
            gituser.PersonalToken = Input.AccessToken;

            var response = await _userManager.UpdateAsync(gituser);

            if(!response.Succeeded)
            {
                StatusMessage = "Unexpected error when trying to save github account.";
                return RedirectToPage();
            }

            if (Input.GithubUsername.IsNullOrEmpty() || Input.AccessToken.IsNullOrEmpty())
            {
                _tokenProvider.ClearToken();
            }
            else
            {
                _tokenProvider.SetToken(Input.AccessToken);
            }

            await _signInManager.RefreshSignInAsync(gituser);
            StatusMessage = "Your github account has been updated";
            return RedirectToPage();
        }
    }
}
