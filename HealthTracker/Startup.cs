using HealthTracker.Core.ContactService;
using HealthTracker.Data;
using HealthTracker.Data.Transfer;
using HealthTrackerAPI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthTrackerSolution
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private readonly string _policyName = "CorsPolicy";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IContactTransfer, ContactTransfer>();
            services.AddScoped<IContactService, ContactService>();

            services.AddDbContext<HealthContext>(options => options.UseInMemoryDatabase("HealthTracker"));
            services.AddControllers().ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);
            services.AddCors(options => options.AddPolicy(name: _policyName, builder =>
            {
                builder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
            }));
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

            app.UseCors(_policyName);

            app.UseAuthorization();

            // middleware to handle exception
            app.UseMiddleware<CustomExceptionHandler>();


            // Below code is commented intensionally, It is inbuild exception handler

            //app.UseExceptionHandler(appError =>
            //{
            //    appError.Run(async (context) =>
            //    {
            //        var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
            //        var contextRequest = context.Features.Get<IHttpRequestFeature>();

            //        if (contextFeature != null)
            //        {
            //            var errorString = new ErrorResponseData()
            //            {
            //                StatusCode = (int)System.Net.HttpStatusCode.InternalServerError,
            //                Message = contextFeature.Error.Message,
            //                Path = contextRequest.Path,
            //            }.ToString();

            //            await context.Response.WriteAsync(errorString);
            //        }
            //    });
            //});

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

            });
        }
    }
}
