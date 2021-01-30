using CarRent.Car.Application;
using CarRent.Car.Domain;
using CarRent.Car.Infrastructure;
using CarRent.Common.Infrastructure;
using CarRent.Reservation.Infrastructure;
using CarRent.User.Application;
using CarRent.User.Domain;
using CarRent.User.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CarRent
{
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
            //services.AddDbContext<CarDbContext>(opt => opt.UseInMemoryDatabase("Test"));
            services.AddDbContext<BaseDbContext>(opt => opt.UseMySql(Configuration.GetConnectionString("CarRentDatabase"),
                    ServerVersion.AutoDetect(Configuration.GetConnectionString("CarRentDatabase")))
                .EnableSensitiveDataLogging());

            services.AddDbContext<CarDbContext>(opt => opt.UseMySql(Configuration.GetConnectionString("CarRentDatabase"),
                        ServerVersion.AutoDetect(Configuration.GetConnectionString("CarRentDatabase")))
                        .EnableSensitiveDataLogging());
            services.AddScoped<ICarRepository, CarRepository>();
            services.AddSingleton<ICarClassFactory, CarClassFactory>();
            services.AddScoped<ICarService, CarService>();

            services.AddDbContext<UserDbContext>(opt =>
                opt.UseMySql(Configuration.GetConnectionString("CarRentDatabase"),
                        ServerVersion.AutoDetect(Configuration.GetConnectionString("CarRentDatabase")))
                        .EnableSensitiveDataLogging());
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();

            services.AddDbContext<ReservationDbContext>(opt =>
                opt.UseMySql(Configuration.GetConnectionString("CarRentDatabase"),
                        ServerVersion.AutoDetect(Configuration.GetConnectionString("CarRentDatabase")))
                    .EnableSensitiveDataLogging());

            services.AddControllers()
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //db.Database.EnsureCreated();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
