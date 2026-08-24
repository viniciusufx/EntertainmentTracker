using EntertainmentTracker.Application.Abstractions.Security;
using EntertainmentTracker.Infrastructure.Security;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

ConfigurePasswordSecurity(builder);
ConfigureServices(builder);

var app = builder.Build();

ConfigureMiddleware(app);

app.Run();

static void ConfigurePasswordSecurity(WebApplicationBuilder builder)
{
    builder.Services.AddOptions<PasswordSecuritySettings>().Bind(builder.Configuration.GetSection("Security:Password")).ValidateOnStart();

    builder.Services.AddSingleton<IValidateOptions<PasswordSecuritySettings>, PasswordSecuritySettingsValidator>();

    builder.Services.AddScoped<IPasswordHasher, Argon2PasswordHasher>();

    builder.Services.AddScoped<IRefreshTokenGenerator, RefreshTokenGenerator>();

    builder.Services.AddOptions<JwtSecuritySettings>().Bind(builder.Configuration.GetSection("JwtSecurity")).ValidateOnStart();

    builder.Services.AddOptions<RsaSecuritySettings>().Bind(builder.Configuration.GetSection("RsaSecurity")).ValidateOnStart();

    builder.Services.AddSingleton<RsaKeyProvider>();

    builder.Services.AddScoped<IAccessTokenGenerator, JwtAccessTokenGenerator>();
}

static void ConfigureServices(WebApplicationBuilder builder)
{
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}

static void ConfigureMiddleware(WebApplication app)
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
}