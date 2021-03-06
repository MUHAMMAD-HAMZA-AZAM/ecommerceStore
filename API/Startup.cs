using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using InfraStructure.Data;
using Core.Interfaces;
using AutoMapper;
using API.Errors;
using StackExchange.Redis;
using InfraStructure.IdentityData;
using Core.PocoEntities.IdentityModels;

namespace API
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IProductRepository,ProductRepository>();
            services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
            services.AddTransient<IBasketRepository,BasketRepository>();
            services.AddAutoMapper(typeof(Startup));//Also Write this services.AddAutoMapper(typeof(MappingProfiles));
            services.AddRazorPages();
            services.AddControllers();
            services.AddDbContext<StoreContext>(options => options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(_configuration.GetConnectionString("IdentityConnection")));

            services.AddSingleton<ConnectionMultiplexer>(c => {
                var configuration = ConfigurationOptions.Parse(_configuration.GetConnectionString("Redis"),true);

                return ConnectionMultiplexer.Connect(configuration);
            
            });


            services.Configure<ApiBehaviorOptions>(options =>
                {
                    options.InvalidModelStateResponseFactory = actionContext =>
                     {
                         var errors = actionContext.ModelState
                         .Where(x => x.Value.Errors.Count > 0)
                         .SelectMany(x => x.Value.Errors)
                         .Select(x => x.ErrorMessage).ToArray();

                         var errorResponse = new ApiValidationErrorResponse
                         {
                             Errors = errors
                         };
                         return new BadRequestObjectResult(errorResponse);
                     };

                });
            services.AddIdentityCore<AppUser>()
          .AddEntityFrameworkStores<AppIdentityDbContext>();

            services.AddMvc();
            services.AddControllersWithViews();
            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                 {
                     policy.AllowAnyOrigin().AllowAnyMethod().WithOrigins("https://localhost:4200").SetIsOriginAllowed((host) => true).AllowCredentials();
                 });
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
            }
            app.UseStatusCodePagesWithReExecute("/errors/{0}");
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "api/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
