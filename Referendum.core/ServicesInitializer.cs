using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Referendum.core;
using Referendum.core.Entities;

namespace Referendum
{
    public static partial class ServicesInitializer
    {
        public static void ConfigureDbContext(IConfiguration configuration, IServiceCollection services)
        {
           

            services.AddDbContextPool<ReferendumContext>(
              options => options.UseSqlServer(configuration.GetConnectionString("ReferendumDbConnection")));


            //services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //    .AddRoles<IdentityRole>()
            //    .AddEntityFrameworkStores<PecMembersDbContext>();


        }

        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient(typeof(IRepositories<>), typeof(Repositories<>));
            services.AddTransient<ICitizenRepasitory, CitizenRepasitory>();
            services.AddTransient<IReferendumRepasitory, ReferendumRepasitory>();
            services.AddTransient<ICommunityRepasitory, CommunityRepasitory>();
            services.AddHttpClient<IWebService, WebService>();
            services.AddTransient<IWebService, WebService>();

        }
    }
}