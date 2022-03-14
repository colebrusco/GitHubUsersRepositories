using GitHubUsersRepository.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GitHubUsersRepository.Repositories.Abstract
{
    public interface IGitHubUserRepository
    {
        Task<GitHubUser> GetGitHubUser(string url, string userName);
        Task<List<GitHubRepository>> GetGitHubUserRepositories(string url);
    }
}
