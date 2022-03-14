using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GitHubUsersRepository.ViewModels
{
    public class GitHubUserViewModel
    {
        [Required(ErrorMessage = "UserName Required")]
        public string Login { get; set; }
        public int Id { get; set; }
        public string AvatarUrl { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
        public int PublicRepos { get; set; }
        public int PublicGists { get; set; }
        public int Followers { get; set; }
        public int Following { get; set; }
        public string Location { get; set; }
        public bool Success { get; set; }

        public List<GitHubRepositoryViewModel> Repositories { get; set; }
    }
}