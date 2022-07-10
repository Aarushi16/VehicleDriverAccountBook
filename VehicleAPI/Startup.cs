using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleAPI.Context;
using VehicleAPI.Interfaces;
using VehicleAPI.Models;
using VehicleAPI.Services;

namespace VehicleAPI
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
            services.AddDbContext<VehicleDbContext>(option => option.UseInMemoryDatabase(Configuration.GetConnectionString("MyDb")));
            services.AddScoped<IVehicleService, VehicleService>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "VehicleAPI", Version = "v1" });
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
                app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "VehicleAPI v1"); });
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
            var context = scope.ServiceProvider.GetService<VehicleDbContext>();
            SeedData(context);
        }

        public static void SeedData(VehicleDbContext context)
        {
            Vehicle vehicle1 = new Vehicle()
            {
                RegistrationNo = "TN01AB0101",
                ModelName = "Hyundai Xcent",
                VehicleType = VehicleTypes.Sedan,
                NumberOfSeat = 4,
                AcAvailable = ACTypes.Yes
            };

            Vehicle vehicle2 = new Vehicle()
            {
                RegistrationNo = "AP02CD0202",
                ModelName = "Chevrolet Tavera",
                VehicleType = VehicleTypes.SUV,
                NumberOfSeat = 9,
                AcAvailable = ACTypes.No
            };

            Vehicle vehicle3 = new Vehicle()
            {
                RegistrationNo = "KL03EF0303",
                ModelName = "Chevrolet Enjoy",
                VehicleType = VehicleTypes.SUV,
                NumberOfSeat = 7,
                AcAvailable = ACTypes.Yes
            };

            Vehicle vehicle4 = new Vehicle()
            {
                RegistrationNo = "KA04GH0404",
                ModelName = "Mahindra Tourister",
                VehicleType = VehicleTypes.Van,
                NumberOfSeat = 15,
                AcAvailable = ACTypes.No
            };

            Vehicle vehicle5 = new Vehicle()
            {
                RegistrationNo = "TN01AB0202",
                ModelName = "Chevrolet Tavera",
                VehicleType = VehicleTypes.SUV,
                NumberOfSeat = 9,
                AcAvailable = ACTypes.Yes
            };

            context.Vehicles.Add(vehicle1);
            context.Vehicles.Add(vehicle2);
            context.Vehicles.Add(vehicle3);
            context.Vehicles.Add(vehicle4);
            context.Vehicles.Add(vehicle5);
            context.SaveChanges();
        }
    }
}
