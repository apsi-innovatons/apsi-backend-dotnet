using apsi.backend.social.Services;
using Microsoft.Extensions.DependencyInjection;

namespace apsi.backend.social.Config
{
    public class DependencyInjection
    {
        public static void Setup(IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<ISocialGroupService, SocialGroupService>();
        }
    }
}