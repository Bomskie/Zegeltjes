using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Zegeltjes
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
            //Sessies
            services.AddDistributedMemoryCache();
            services.AddSession(options => {
                //options.IdleTimeout = TimeSpan.FromMinutes(1);//You can set Time   
            });
            //einde sessies

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseSession(); // Sessies

            app.UseMvc(routes =>
            {
                //Eigen link maken:
                //eigenlijke link: http://localhost:53718/Home/Login
                //Link nu: http://localhost:53718/Inloggen
                routes.MapRoute(
                    name: "Inloggen",
                    template: "Inloggen",
                    defaults: new { Controller = "Home", Action = "Login" }
                    );
                routes.MapRoute(
                    name: "Register",
                    template: "Register",
                    defaults: new { Controller = "Home", Action = "Register" }
                    );
                routes.MapRoute(
                    name: "MijnAanbiedingen",
                    template: "Mijnaanbiedingen",
                    defaults: new { Controller = "Home", Action = "MijnAanbiedingen" }
                    );
                routes.MapRoute(
                   name: "VoegAanbiedingToe",
                   template: "VoegAanbiedingToe",
                   defaults: new { Controller = "Home", Action = "VoegAanbiedingToe" }
                   );
                routes.MapRoute(
                   name: "Aanbieding",
                   template: "Aanbieding/{*id}",
                   defaults: new { Controller = "Home", Action = "Aanbieding" }
                   );
                routes.MapRoute(
                   name: "VerwijderAanbieding",
                   template: "VerwijderAanbieding/{*id}",
                   defaults: new { Controller = "Home", Action = "VerwijderAanbieding" }
                   );
                routes.MapRoute(
                   name: "ClaimAanbieding",
                   template: "Claim/{*id}",
                   defaults: new { Controller = "Home", Action = "Claim" }
                   );
                routes.MapRoute(
                   name: "ClaimToekennen",
                   template: "ClaimToekennen/{*id}",
                   defaults: new { Controller = "Home", Action = "ClaimToekennen" }
                   );
                routes.MapRoute(
                   name: "Zegels",
                   template: "Zegels/{*Plaats}",
                   defaults: new { Controller = "Home", Action = "Zegels" }
                   );
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
