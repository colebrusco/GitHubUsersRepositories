using FizzWare.NBuilder;
using GitHubUsersRepository.Services.Abstract;
using Moq;
using NUnit.Framework;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using GitHubUsersRepository.Services.Implementation;
using GitHubUsersRepository.Repositories.Abstract;
using System.Configuration;
using GitHubUsersRepository.App_Start;

namespace GitHubUsersRepository.Tests.Controllers.GitHubUser
{
    [TestFixture]
    public class GitHubUserServiceTest
    {

        private Mock<IGitHubUserRepository> _mockGitHubUserRepository;
        private IGitHubUserService _gitHubUserService;

        [SetUp]
        public void SetUp()
        {
            AutoMapperConfig.Register();
            _mockGitHubUserRepository = new Mock<IGitHubUserRepository>();
            _gitHubUserService = new GitHubUserService(_mockGitHubUserRepository.Object);    
        }


        [Test]
        public async Task Call_GetGitHubUser_With_Existent_User()
        {
            Task<Model.GitHubUser> result = Task.FromResult(Builder<Model.GitHubUser>.CreateNew().Build());
            _mockGitHubUserRepository.Setup(x => x.GetGitHubUser(It.IsAny<string>(), It.IsAny<string>())).Returns(result);

            Task<List<Model.GitHubRepository>> resultGitHubRepositories = Task.FromResult(Builder<Model.GitHubRepository>.CreateListOfSize(100).Build().ToList());
            _mockGitHubUserRepository.Setup(x => x.GetGitHubUserRepositories(It.IsAny<string>())).Returns(resultGitHubRepositories);


            var awaitedResult = await result;
            var url = ConfigurationManager.AppSettings["GitHubUserURL"];
            var maxStarCount = int.Parse(ConfigurationManager.AppSettings["GitHubRepoMaxStar"]);
            var gitHubUserViewModel = await _gitHubUserService.GetGitHubUser(url, "colebrusco");

            if (!gitHubUserViewModel.Success)
            {
                Assert.Fail();
            }
            gitHubUserViewModel.Success.Should().BeTrue();
            gitHubUserViewModel.Login.Should().Be(awaitedResult.Login);
            gitHubUserViewModel.Location.Should().Be(awaitedResult.Location);
            gitHubUserViewModel.AvatarUrl.Should().Be(awaitedResult.AvatarUrl);
            gitHubUserViewModel.Repositories.Should().HaveCount(maxStarCount);
        }

        [Test]
        public async Task Call_GetGitHubUser_With_NonExistent_User()
        {
            _mockGitHubUserRepository.Setup(x => x.GetGitHubUser(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult<Model.GitHubUser>(null));
            var url = ConfigurationManager.AppSettings["GitHubUserURL"];
            var gitHubUserViewModel = await _gitHubUserService.GetGitHubUser(url, "non_existent_user_Lucas_Colebrusco");

            if (gitHubUserViewModel.Success)
            {
                Assert.Fail();
            }
            gitHubUserViewModel.Success.Should().BeFalse();
        }

     
    }
}
