using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Talabat.Helper;
using TalabatCore.Repositories;
using TalabatRepository.Context;
using TalabatRepository.GenericRepository;
using AutoMapper;
using Microsoft.AspNetCore.Diagnostics;
using Talabat.Extensions;
using StackExchange.Redis;
using TalabatRepository.Identity;
using TalabatService.Services;
using TalabatCore.OrderService;
using TalabatRepository;

namespace Talabat
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

            services.AddSwaggerService();
            
            services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));

            });
            services.AddDbContext<AppIdentityDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection"));
            });
            services.AddApplicationServices();
            
            services.AddSingleton<IConnectionMultiplexer>(S => {
                var connection = ConfigurationOptions.Parse(Configuration.GetConnectionString("Redis"));
                return ConnectionMultiplexer.Connect(connection);
            });
            services.AddTransient<IBusketRep, BasketRepository>();
            services.AddIdentity(Configuration);
            services.AddScoped<IOrderService,OrderService >();
            services.AddScoped<IUnitofwork,UnitOfWork>();
            services.AddCors(
                option => option.AddPolicy("corspolicy", option =>
                {
                    option.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
                })
                );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ExceptionMiddleware>();

            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Talabat v1"));
            }

            app.UseHttpsRedirection();
            app.UseCors("corspolicy");

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseStaticFiles();
        }
    }
}
