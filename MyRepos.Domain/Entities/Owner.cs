using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRepos.Domain.Entities
{
    public class Owner
    {
        public string login {  get; set; }
        public int id {  get; set; }
        public string avatar_url {  get; set; }
        public string url {  get; set; }
        public string html_url {  get; set; }
        public string starred_url {  get; set; }
        public string type {  get; set; }
        public bool site_admin {  get; set; }
    }
}
