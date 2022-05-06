using FirelessApi.Domain;
using FirelessApi.Domain.Repository;
using FirelessApi.Domain.Services;
using FirelessApi.Persistence;
using FirelessApi.Services;
using FirelessApi.Swagger;
using Microsoft.EntityFrameworkCore;

namespace FirelessApi;

public class Startup
{
    public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //     .AddMicrosoftIdentityWebApi(Configuration.GetSection("AzureAdB2C"));

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                        .AllowAnyMethod()
                        .AllowCredentials()
                        .SetIsOriginAllowed((host) => true)
                        .AllowAnyHeader());
            });

            services.AddControllers();
            
            services.AddDbContext<FirelessDbContext>(options =>
            {
                options.UseInMemoryDatabase("fireless-api-in-memory");
            });

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddScoped<IAlertsService, AlertsService>();
            services.AddScoped<IDataService, DataService>();
            services.AddScoped<IRegionsService, RegionsService>();
            services.AddScoped<IUsersService, UsersService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddAutoMapper(typeof(Startup));
            services.AddCustomSwagger();
        }

        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseAuthentication();
            
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.UseCustomSwagger();
        }
}