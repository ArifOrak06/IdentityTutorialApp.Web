using IdentityAppTutorial.Core.Services;
using IdentityTutorialApp.Service.Services;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityTutorialApp.Service.Extensions.Microsoft
{
    public static class DependencyResolvers
    {
        public static void AddServiceLayerDependencies(this IServiceCollection services)
        {
            services.AddScoped<IEmailService, EmailService>();
        }
    }
}
