using Newtonsoft.Json;

namespace GitHubUsersRepository.Model
{
    public class GitHubUser
    {
        [JsonProperty("login")]
        public string Login { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("avatar_url")]
        public string AvatarUrl { get; set; }

        [JsonProperty("html_url")]
        public string HtmlUrl { get; set; }

        [JsonProperty("repos_url")]
        public string ReposUrl { get; set; }
        
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("bio")]
        public string Bio { get; set; }

        [JsonProperty("public_repos")]
        public int PublicRepos { get; set; }

        [JsonProperty("public_gists")]
        public int PublicGists { get; set; }

        [JsonProperty("followers")]
        public int Followers { get; set; }

        [JsonProperty("following")]
        public int Following { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

    }
}
