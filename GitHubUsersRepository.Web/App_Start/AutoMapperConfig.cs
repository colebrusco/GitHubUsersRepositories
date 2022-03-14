using AutoMapper;
using GitHubUsersRepository.Model;
using GitHubUsersRepository.ViewModels;

namespace GitHubUsersRepository.App_Start
{
    public static class AutoMapperConfig
    {
        public static IMapper Mapper { get; set; }

        public static void Register()
        {
            var mapperConfig = new MapperConfiguration(
                config =>
                {
                    config.AddProfile<MappingProfile>();
                }
            );

            Mapper = mapperConfig.CreateMapper();
        }
    }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<GitHubUser, GitHubUserViewModel>().ReverseMap();
            CreateMap<GitHubRepository, GitHubRepositoryViewModel>().ReverseMap();
        }
    }
}