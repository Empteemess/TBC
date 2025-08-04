namespace Web.api;

public class Program
{
    public static void Main(string[] args)
    {
        Env.Load();
        var builder = WebApplication.CreateBuilder(args);

        builder.Configuration.AddEnvironmentVariables();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddFluentValidationAutoValidation();

        builder.Services.AddValidatorsFromAssembly(typeof(ApplicationUserDtoValidator).Assembly);

        builder.Services.AddSwaggerGen();

        builder.Services
            .AddDbConfigurations(builder.Configuration)
            .AddServiceConfigurations()
            .AddSwaggerConfigs();

        var app = builder.Build();

        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseMiddleware<ErrorHandlingMiddleware>();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}