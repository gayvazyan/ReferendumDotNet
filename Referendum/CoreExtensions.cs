using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Referendum.Model;
using Referendum.Repositories;

namespace Referendum
{
    public static partial class CoreExtensions
    {
        public static void ConfigureDbContext(IConfiguration configuration, IServiceCollection services)
        {
            services.AddDbContext<ReferendumContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("ReferendumDbConnection")));

           
         
            //services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //    .AddRoles<IdentityRole>()
            //    .AddEntityFrameworkStores<PecMembersDbContext>();


        }
        
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient(typeof(IRepositories<>), typeof(Repositories<>));
            services.AddTransient<ICitizenRepasitory, CitizenRepasitory>();

        }
    }
}