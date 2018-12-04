using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Vidos.Web.Areas.Identity.Data;
using Vidos.Web.Models;

[assembly: HostingStartup(typeof(Vidos.Web.Areas.Identity.IdentityHostingStartup))]
namespace Vidos.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<VidosContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("VidosContextConnection")));

                services.AddDefaultIdentity<VidosUser>()
                    .AddEntityFrameworkStores<VidosContext>();
            });
        }
    }
}