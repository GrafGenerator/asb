using ASB.Abstractions;
using ASB.Common.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ASB.Microservices.APIService
{
    public class Startup
    {
        private readonly ApiMicroservice _microservice;
        private readonly MicroserviceOptions<ApiMicroserviceOptions> _microserviceOptions;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _microserviceOptions = new MicroserviceOptions<ApiMicroserviceOptions>()
            {
                ServiceId = "web-1",
            };
            _microservice = new ApiMicroservice(_microserviceOptions);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddHandlers();
            services.AddSingleton<IMicroserviceCommandsRegistry, CommandsRegistry>();
            services.Add(new ServiceDescriptor(typeof(IMessageSource), _microservice.MessageSource));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            // late bound to commands registry
            _microserviceOptions.CommandsRegistry =
                app.ApplicationServices.GetRequiredService<IMicroserviceCommandsRegistry>();

            _microservice.Start();
        }
    }
}