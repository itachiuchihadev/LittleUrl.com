using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.FileProviders;

namespace LittleUrl
{
    public class Startup
    {
        private IWebHostEnvironment CurrentEnvironment{ get; set; } 
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            CurrentEnvironment = env;
        }
        public static IConfiguration Configuration { get; private set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSharedInfrastructure(Configuration);
            services.AddControllers();
            services.AddJwtTokenAuthentication(Configuration);  
            services.AddSwaggerGen();
            services.AddHealthChecks();
            services.AddHttpContextAccessor();
            services.AddScoped<IServiceUserContext, ServiceUserContext>();
            // services.AddAuthorization();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors();          
            // app.UseSwaggerSetup();
            app.UseRouting();

            // FileSaver.CreateUploadDirectory("");
            // app.UseSpaStaticFiles();
            // app.UseStaticFiles(new StaticFileOptions
            // {
            //     FileProvider = new PhysicalFileProvider(
            //         Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wwwroot", "Uploads")),
            //     RequestPath = "/StaticFiles"
            // });
            

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<ErrorHandlerMiddleware>();
            app.UseMiddleware<AuthenticationMiddleware>();
            app.UseHealthChecks("/health");


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}