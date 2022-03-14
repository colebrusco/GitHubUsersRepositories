using GitHubUsersRepository.Repositories.Abstract;
using GitHubUsersRepository.Repositories.Repositories;
using GitHubUsersRepository.Services.Abstract;
using GitHubUsersRepository.Services.Implementation;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using System.Reflection;
using System.Web.Mvc;

namespace GitHubUsersRepository
{
    public class SimpleInjectorConfig
    {
        public static void Register()
        {
            var container = new Container();

            container.Options.ResolveUnregisteredConcreteTypes = true;
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            container.Register<IGitHubUserRepository, GitHubUserRepository>(Lifestyle.Scoped);
            container.Register<IGitHubUserService, GitHubUserService>(Lifestyle.Scoped);

            container.Verify();

            DependencyResolver.SetResolver(
               new SimpleInjectorDependencyResolver(container));
        }
    }
}