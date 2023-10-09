using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRepos.Domain.DTOs
{
    public class GithubOauthResponse
    {
        public string token_Type { get; set; }
        public string access_token { get; set; }
    }
}
