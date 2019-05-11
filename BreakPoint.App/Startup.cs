namespace BreakPoint.App
{
    using BreakPoint.Data;
    using BreakPoint.Data.Interfaces;
    using BreakPoint.Services.Interfaces;
    using BreakPoint.Services.Services;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using System.Reflection;

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
            // DB configuration
            var connectionString = "Server=.\\SQLEXPRESS;Database=BreakPointDB;Integrated Security=True;Trusted_Connection=True;MultipleActiveResultSets=true";
            services.AddDbContext<BreakPointDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddMvc().AddApplicationPart(Assembly.Load(new AssemblyName("BreakPoint.UnitTests")));

            // Register the Swagger services
            services.AddSwaggerDocument();

            // Service Injection
            services.AddTransient<IBreakPointDbContext, BreakPointDbContext>();
            services.AddTransient<IAccountService, AccountService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            // Launch the app.Navigate to:
            //   localhost:<port>/swagger to view the Swagger UI.
            //   localhost:<port>/swagger/v1/swagger.json to view the Swagger specification.
            app.UseSwagger();
            app.UseSwaggerUi3();

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
