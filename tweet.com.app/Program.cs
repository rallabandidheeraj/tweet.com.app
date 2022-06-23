using com.tweetapp.DAO;
using com.tweetapp.Service;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace tweet.com.app
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceCollection services = new ServiceCollection();
            Configure(services);
            IServiceProvider provider = services.BuildServiceProvider();
            var service = provider.GetService<IUserIntro>();
            service.IntroPageNonLoggedUser();
        }
        private static void Configure(IServiceCollection service)
        {
            service.AddSingleton<IRepository, Repository>();
            service.AddScoped<IUserIntro, UserIntro>();

        }
    }
}
