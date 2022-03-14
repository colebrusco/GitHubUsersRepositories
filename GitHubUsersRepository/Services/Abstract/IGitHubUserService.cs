using GitHubUsersRepository.ViewModels;
using System.Threading.Tasks;

namespace GitHubUsersRepository.Services.Abstract
{
    public interface IGitHubUserService
    {
        Task<GitHubUserViewModel> GetGitHubUser(string url, string userName);
    }
}
