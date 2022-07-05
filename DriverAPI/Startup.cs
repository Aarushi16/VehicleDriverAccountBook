using DriversAPI.Context;
using DriversAPI.Interfaces;
using DriversAPI.Models;
using DriversAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.Text;

namespace DriversAPI
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
            services.AddControllers();
            services.AddDbContext<DriverDbContext>(option => option.UseInMemoryDatabase(Configuration.GetConnectionString("MyDb")));
            services.AddScoped<IDriverServices, DriverServices>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DriverAPI", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement(){
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "DriverAPI v1"); });
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetService<DriverDbContext>();
            SeedData(context);
        }

        public static void SeedData(DriverDbContext context)
        {
            Driver driver1 = new Driver()
            {
                LicenseNumber = "TN01X20210000123",
                Name = "Sabheer Ahmed",
                Age = 25,
                VehicleType = VehicleTypes.Sedan
            };

            Driver driver2 = new Driver()
            {
                LicenseNumber = "AP01Z20020000321",
                Name = "Gurumahendra Rao",
                Age = 34,
                VehicleType = VehicleTypes.Van
            };

            Driver driver3 = new Driver()
            {
                LicenseNumber = "KL04Y20080000213",
                Name = "Michael Daniel",
                Age = 30,
                VehicleType = VehicleTypes.SUV
            };

            Driver driver4 = new Driver()
            {
                LicenseNumber = "KA02W20100000231",
                Name = "Yogeesh",
                Age = 28,
                VehicleType = VehicleTypes.SUV
            };

            context.Drivers.Add(driver1);
            context.Drivers.Add(driver2);
            context.Drivers.Add(driver3);
            context.Drivers.Add(driver4);
            context.SaveChanges();
        }
    }
}
