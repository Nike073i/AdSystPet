using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AdSyst.AuthService.Application.Users.Queries.GetPersonalData;
using AdSyst.AuthService.Application.Users.Queries.GetUserData;
using AdSyst.AuthService.Application.Users.Queries.GetUserInfoDetails;
using AdSyst.AuthService.Application.Users.Queries.GetUserInfoList;
using AdSyst.AuthService.Application.Users.Queries.IsEmailFree;
using AdSyst.AuthService.Application.Users.Queries.IsUsernameFree;
using AdSyst.AuthService.Domain;
using AdSyst.AuthService.EfContext.UserData.Contexts;
using AdSyst.AuthService.EfContext.UserData.Users;
using AdSyst.AuthService.EfContext.UserData.Users.Services;

namespace AdSyst.AuthService.SqlServerMigrations
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IUserManager, UserManagerAdapter>();
            AddServices(services);
            return services;
        }

        private static void AddServices(IServiceCollection services)
        {
            services.AddScoped<IGetPersonalDataService, GetPersonalDataService>();
            services.AddScoped<IGetUserDataService, GetUserDataService>();
            services.AddScoped<IGetUserInfoDetailsService, GetUserInfoDetailsService>();
            services.AddScoped<IGetUserInfoListService, GetUserInfoListService>();
            services.AddScoped<IIsEmailFreeService, IsEmailFreeService>();
            services.AddScoped<IIsUsernameFreeService, IsUsernameFreeService>();
            return;
        }

        public static IIdentityServerBuilder AddIdentityPersistedGrantContext(
            this IIdentityServerBuilder builder,
            string dbConnectionString
        )
        {
            builder.AddOperationalStore(
                options =>
                    options.ConfigureDbContext = builder =>
                        builder.UseSqlServer(
                            dbConnectionString,
                            x => x.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName)
                        )
            );
            return builder;
        }

        public static IIdentityServerBuilder AddIdentityConfigurationContext(
            this IIdentityServerBuilder builder,
            string dbConnectionString
        )
        {
            builder.AddConfigurationStore(
                options =>
                    options.ConfigureDbContext = builder =>
                        builder.UseSqlServer(
                            dbConnectionString,
                            x => x.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName)
                        )
            );
            return builder;
        }

        public static IServiceCollection AddIdentityUserData(
            this IServiceCollection services,
            string dbConnectionString
        )
        {
            services.AddDbContext<UserDataDbContext>(
                options =>
                    options.UseSqlServer(
                        dbConnectionString,
                        x => x.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName)
                    )
            );
            services.AddDbContext<UserDataReadDbContext>(
                options =>
                    options.UseSqlServer(
                        dbConnectionString,
                        x => x.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName)
                    )
            );

            return services;
        }
    }
}
