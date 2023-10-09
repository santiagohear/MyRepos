using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRepos.Domain.Dictionary
{
    public class Dictionary
    {
        public static string GitHubAPIBase { get; set; }
        public static string ClientId {  get; set; }
        public static string OAuthURL {  get; set; }
        public static string ClientSecret {  get; set; }
        public static string TokenCookie = "GitHubToken";
        public enum ApiType
        {
            GET,
            POST,
            PUT,
            DELETE
        }
    }
}
