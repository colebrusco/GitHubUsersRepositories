using GitHubUsersRepository.Model;
using GitHubUsersRepository.Repositories.Abstract;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace GitHubUsersRepository.Repositories.Repositories
{
    public class GitHubUserRepository : IGitHubUserRepository
    {
        private const string USER_AGENT = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/70.0.3538.77 Safari/537.36";

        public async Task<GitHubUser> GetGitHubUser(string url, string userName)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var gitHubUserUrl = string.Concat(url, userName);
                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", USER_AGENT);
                    string resultado = await httpClient.GetStringAsync(gitHubUserUrl);
                    var gitHubUser = JsonConvert.DeserializeObject<GitHubUser>(resultado);
                    
                    return gitHubUser;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<List<GitHubRepository>> GetGitHubUserRepositories(string url)
        {
            var gitHubRepository = new List<GitHubRepository>();
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", USER_AGENT);
                    string resultado = await httpClient.GetStringAsync(url);
                    gitHubRepository = JsonConvert.DeserializeObject<List<GitHubRepository>>(resultado);
                  
                    return gitHubRepository;
                }
            }
            catch (Exception e)
            {
                return gitHubRepository;
            }

        }
    }
}
