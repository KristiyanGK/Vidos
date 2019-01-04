using System;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stripe;
using Vidos.Data;
using Vidos.Data.Common;
using Vidos.Data.Models;
using Vidos.Services.DataServices;
using Vidos.Services.DataServices.Contracts;
using Vidos.Services.Mapping;
using Vidos.Services.Models.Order.ViewModels;
using Vidos.Services.Models.Product.ViewModels;
using Vidos.Web.Common;
using Vidos.Web.Common.Constants;
using Vidos.Web.Common.Seeder;
using Vidos.Web.Filters.AuthorizationFilters;
using OrderService = Vidos.Services.DataServices.OrderService;

namespace Vidos.Web
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
            AutoMapperConfig.RegisterMappings(
                typeof(AllProductsViewModel).Assembly,
                typeof(ProductDetailsViewModel).Assembly,
                typeof(ProductsCreateViewModel).Assembly,
                typeof(OrderCheckoutViewModel).Assembly,
                typeof(MyOrdersViewModel).Assembly
            );

            StripeConfiguration.SetApiKey(Configuration.GetSection("Stripe")["SecretKey"]);

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<VidosContext>(options =>
                options.UseSqlServer(
                    this.Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<VidosUser, IdentityRole>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 3;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                })
                .AddEntityFrameworkStores<VidosContext>()
                .AddDefaultTokenProviders()
                .AddDefaultUI(UIFramework.Bootstrap4);

            services.AddAutoMapper();
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = Constants.SessionIdleTimeoutTimespan;
                options.Cookie.HttpOnly = true; // XSS Security
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            
            services.AddScoped(SessionCartService.GetCart);
            services.AddScoped(typeof(IRepository<>), typeof(DbRepository<>));
            services.AddScoped<IProductsService, ProductsService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IBrandService, BrandService>();

            services.AddTransient<Seeder>();

            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(EnsureLoggedInFilter));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, Seeder seeder)
        {
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();
            app.UseAuthentication();
            app.UseStatusCodePagesWithReExecute("/error/{0}");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();

                seeder.SeedProducts();
                seeder.SeedRoles();
                seeder.SeedAdmin();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "shopping",
                    template: "{area:exists}/{controller=Products}/{action=All}/{id?}"
                );

                routes.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller}/{action}/{id?}"
                );

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}"
                );
            });
        }
    }
}
