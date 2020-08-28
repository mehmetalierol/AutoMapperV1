using AutoMapper;
using EntityLayer;
using FoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AutoMapperV1
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
            //Do this before mapper section to add class in current assembly
            EntityInitializer.Init();

            #region FoMapper

            //add custom profile to mapperconfiguration
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new FoMappingProfile(Configuration));
                //if you want to use custom profiles use this class
                //mc.AddProfile(new CustomMapperProfile());
            });

            //create mapper and add it to assembly in singleton mode
            services.AddSingleton(mapperConfig.CreateMapper());

            #endregion FoMapper

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}