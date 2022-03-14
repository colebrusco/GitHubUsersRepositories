using AutoMapper;
using GitHubUsersRepository.App_Start;
using GitHubUsersRepository.Repositories.Abstract;
using GitHubUsersRepository.Services.Abstract;
using GitHubUsersRepository.ViewModels;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace GitHubUsersRepository.Services.Implementation
{
    public class GitHubUserService : IGitHubUserService
    {
        private readonly IGitHubUserRepository _gitHubUserRepository;
        private readonly IMapper _mapper;

        public GitHubUserService(IGitHubUserRepository gitHubUserRepository)
        {
            _gitHubUserRepository = gitHubUserRepository;
            _mapper = AutoMapperConfig.Mapper;
        }

        public async Task<GitHubUserViewModel> GetGitHubUser(string url, string userName)
        {
            var gitHubUserViewModel = new GitHubUserViewModel();
            gitHubUserViewModel.Login = userName;
            var user = await _gitHubUserRepository.GetGitHubUser(url, userName);
            if (user != null)
            {
                gitHubUserViewModel = _mapper.Map<GitHubUserViewModel>(user);
                gitHubUserViewModel.Success = true;
                if (string.IsNullOrEmpty(user.ReposUrl))
                    gitHubUserViewModel.Repositories = new List<GitHubRepositoryViewModel>();
                else
                {
                    var repositories = await _gitHubUserRepository.GetGitHubUserRepositories(user.ReposUrl);

                    var maxStar = int.Parse(ConfigurationManager.AppSettings["GitHubRepoMaxStar"]);

                    repositories = repositories.OrderByDescending(x => x.StargazersCount).Take(maxStar).ToList();

                    var repositoriesViewModel = _mapper.Map<List<GitHubRepositoryViewModel>>(repositories);
                    gitHubUserViewModel.Repositories = repositoriesViewModel;
                }
            }
            else
                gitHubUserViewModel.Success = false;

            return gitHubUserViewModel;
        }
    }
}