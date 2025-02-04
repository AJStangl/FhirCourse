﻿using FhirCourse.Services;
using FhirCourse.Services.FhirServices;
using FhirCourse.Services.MSAuthenticationService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FhirCourse
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration config)
        {
            _configuration = config;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.Configure<AzureAd>(_configuration.GetSection("AzureAd"));
            services.AddSingleton<IFhirServices, FhirServices>();
            services.AddTransient<IClient, Client>();
            services.AddTransient<IMsalAuthenticator, MsalAuthenticationHandler>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc();
        }
    }
}