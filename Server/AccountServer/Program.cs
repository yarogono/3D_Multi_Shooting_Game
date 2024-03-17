using Microsoft.AspNetCore.HttpOverrides;
using AccountServer.Utils;
using AccountServer.Service.Contract;
using AccountServer.Service;
using AccountServer.Repository.Contract;
using AccountServer.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using ZLogger;
using SqlKata.Execution;
using MySqlConnector;
using SqlKata.Compilers;
using System.Data;

namespace AccountServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var app = BuilderSetting(builder);

            WebApplicationSetting(app);

            app.Run();
        }

        private static WebApplication BuilderSetting(WebApplicationBuilder builder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                                                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                                                .AddJsonFile("config.json")
                                                .Build();

            string connectionString = configuration.GetConnectionString("MyConnection");

            ConfigureServices(builder.Services, connectionString);

            Authentication(builder.Services, builder.Configuration);

            builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                       .AddEnvironmentVariables();

            ConfigureLogging(builder, configuration);


            return builder.Build();
        }

        private static void ConfigureLogging(WebApplicationBuilder builder, IConfigurationRoot configuration)
        {
            builder.Host.ConfigureLogging(logging =>
            {
                logging.ClearProviders();

                logging.AddConfiguration(configuration.GetSection("Logging"));

                logging.AddZLoggerConsole();
                logging.AddZLoggerFile("zlogFile.log");
            });
        }

        private static void ConfigureServices(IServiceCollection services, string connectionString)
        {

            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
                options.JsonSerializerOptions.DictionaryKeyPolicy = null;
            });

            services.AddTransient<QueryFactory>((e) =>
            {
                var connection = new MySqlConnection(connectionString);
                var compiler = new MySqlCompiler();
                return new QueryFactory(connection, compiler);
            });

            services.AddSwaggerGen();

            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IOauthService, OauthService>();
            services.AddScoped<IOauthRepository, OauthRepository>();
            services.AddSingleton<PasswordEncryptor>();

            services.AddControllersWithViews();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        private static void Authentication(IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
            })
                .AddCookie()
                .AddGoogle(googleOptions =>
                {
                    googleOptions.ClientId = configuration["Authentication:Google:ClientId"];
                    googleOptions.ClientSecret = configuration["Authentication:Google:ClientSecret"];
                    googleOptions.SaveTokens = true;
                }
            );
        }

        private static void WebApplicationSetting(WebApplication app)
        {
            if (!app.Environment.IsDevelopment())
            {
                app.UseHttpsRedirection();
            }

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            UseSwagger(app);

            app.UseForwardedHeaders();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
        }

        private static void UseSwagger(WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
            });
        }
    }
}