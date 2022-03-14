using FizzWare.NBuilder;
using GitHubUsersRepository.Controllers;
using GitHubUsersRepository.Services.Abstract;
using GitHubUsersRepository.ViewModels;
using Moq;
using NUnit.Framework;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using FluentAssertions;

namespace GitHubUsersRepository.Tests.Controllers.GitHubUser
{
    [TestFixture]
    public class SearchTests
    {

        private GitHubUserController _gitHubUserController;
        private Mock<IGitHubUserService> _mockGitHubUserService;


        [SetUp]
        public void SetUp()
        {
            _mockGitHubUserService = new Mock<IGitHubUserService>();
            _gitHubUserController = new GitHubUserController(_mockGitHubUserService.Object);
        }


        [Test]
        public async Task Call_SearchAction_And_Return_To_View()
        {
            Task<GitHubUserViewModel> result = Task.FromResult(Builder<GitHubUserViewModel>.CreateNew().With(x => x.Repositories = Builder<GitHubRepositoryViewModel>.CreateListOfSize(100).Build().ToList()).With(x => x.Success = true).Build());
            _mockGitHubUserService.Setup(x => x.GetGitHubUser(It.IsAny<string>(), It.IsAny<string>())).Returns(result);

            var awaitedResult = await result;
            var controllerResult = await _gitHubUserController.Search(new GitHubUserViewModel { Login = awaitedResult.Login }) as ViewResult;
            var model = controllerResult?.Model as GitHubUserViewModel;

            if (model == null)
            {
                Assert.Fail();
            }
            model.Success.Should().BeTrue();
            model.Login.Should().Be(awaitedResult.Login);
            model.Location.Should().Be(awaitedResult.Location);
            model.AvatarUrl.Should().Be(awaitedResult.AvatarUrl);
            model.Repositories.Should().HaveCount(awaitedResult.Repositories.Count);
        }

        [Test]
        public async Task Call_SearchAction_With_Empty_Value()
        {            
            var controllerResult = await _gitHubUserController.Search(new GitHubUserViewModel()) as ViewResult;
            var model = controllerResult?.Model as GitHubUserViewModel;

            if (model != null)
            {
                Assert.Fail();
            }

            model.Should().BeNull();
        }

        [Test]
        public async Task Call_SearchAction_With_NonExistent_User()
        {
            Task<GitHubUserViewModel> result = Task.FromResult(Builder<GitHubUserViewModel>.CreateNew().With(x => x.Repositories = new List<GitHubRepositoryViewModel>()).With(x=> x.Success = false).Build());
            _mockGitHubUserService.Setup(x => x.GetGitHubUser(It.IsAny<string>(), It.IsAny<string>())).Returns(result);

            var awaitedResult = await result;
            var controllerResult = await _gitHubUserController.Search(new GitHubUserViewModel { Login = "non_existent_user_Lucas_Colebrusco" }) as ViewResult;
            var model = controllerResult?.Model as GitHubUserViewModel;

            if (model == null)
            {
                Assert.Fail();
            }
            model.Success.Should().BeFalse();
            model.Repositories.Should().HaveCount(0);
        }
    }
}
