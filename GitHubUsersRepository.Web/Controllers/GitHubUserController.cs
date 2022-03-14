using GitHubUsersRepository.Services.Abstract;
using GitHubUsersRepository.ViewModels;
using System.Configuration;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GitHubUsersRepository.Controllers
{
    public class GitHubUserController : Controller
    {

        private readonly IGitHubUserService _gitHubUserService;

        public GitHubUserController(IGitHubUserService gitHubUserService)
        {
            _gitHubUserService = gitHubUserService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Search(GitHubUserViewModel gitHubUserViewModel)
        {
            if (ModelState.IsValid)
            {
                var url = ConfigurationManager.AppSettings["GitHubUserURL"];
                var value = await _gitHubUserService.GetGitHubUser(url, gitHubUserViewModel.Login);
                return View("Index", value);
            }

            return View("Index", null);
        }
        
    }
}