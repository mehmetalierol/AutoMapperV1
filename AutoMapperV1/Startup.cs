using AutoMapper;
using AutoMapperV1.MapperProfile;
using EntityLayer.Base;
using FoMapper;
using FoMapper.Config;
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
            #region FoMapper
            //get configuration from appsettings.json
            var foConfig = Configuration.GetSection("FoConfiguration").Get<FoConfiguration>();

            //Create custom profile instance
            var foProfile = new FoMappingProfile();

            //Set postfix list from appsettings (this settings will clear postfix after domain name like UserDto, UserEntity, UserVm)
            foProfile.PostFixList = foConfig.PostFixList;
            //foProfile.PrefixList = foConfig.PrefixList; if you are gonna use prefix use this line 
            //(in this version you have to choose you cant use both at the same time)

            //automatically create mapping profile between entity and dto classes
            foProfile.UseEntitytoDto(foConfig.EntityBase, foConfig.DtoBase, true, typeof(FoEntity), typeof(FoDto));

            //automatically create mapping profile between dto and vm classes
            foProfile.UseDtotoVM(foConfig.DtoBase, foConfig.VMBase, true, typeof(FoDto), typeof(FoVM));

            //add custom profile to mapperconfiguration
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(foProfile);
                //if you want to use custom profiles use this class
                mc.AddProfile(new CustomMapperProfile());
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}