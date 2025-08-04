namespace Web.api.Configurations;

public static class ServiceConfigurations
{
    public static IServiceCollection AddServiceConfigurations(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<UserManager<ApplicationUser>>();

        services.AddScoped<IUserRelationshipsRepository, UserRelationshipsRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserRelationshipsService, UserRelationshipsService>();
        services.AddScoped<IAwsService, AwsService>();

        services.AddScoped<ErrorHandlingMiddleware>();

        services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

        return services;
    }
}