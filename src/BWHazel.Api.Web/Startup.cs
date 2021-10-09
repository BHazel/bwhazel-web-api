﻿using System;
using System.IO;
using System.Net;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace BWHazel.Api.Web
{
    /// <summary>
    /// Configures the application on startup.
    /// </summary>
    public class Startup
    {
        private const string KubernetesRootIpKey = "Kubernetes:RootIp";
        private const string KubernetesClusterIpKey = "Kubernetes:ClusterIp";
        private const string ApiTitleKey = "Api:Title";
        private const string ApiVersionKey = "Api:Version";
        private const string ApiDescriptionKey = "Api:Description";

        /// <summary>
        /// Initialises a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The application configuration.</param>
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        /// <summary>
        /// Gets the application configuration.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Configures and adds services to the application container.
        /// </summary>
        /// <param name="services">The application services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
                options.ForwardLimit = 2;

                (string ipAddress, int mask) rootIp = this.GetIpAddressComponents(this.Configuration[KubernetesRootIpKey]);
                (string ipAddress, int mask) clusterIp = this.GetIpAddressComponents(this.Configuration[KubernetesClusterIpKey]);

                options.KnownNetworks.Add(new IPNetwork(IPAddress.Parse(rootIp.ipAddress), rootIp.mask));
                options.KnownNetworks.Add(new IPNetwork(IPAddress.Parse(clusterIp.ipAddress), clusterIp.mask));
            });

            services.AddApplicationInsightsTelemetry();
            services.AddControllersWithViews();
            services.AddSwaggerGen(config =>
            {
                config.IncludeXmlComments(this.GetXmlDocumentationFilePath());
                config.SwaggerDoc(
                    "api",
                    new OpenApiInfo()
                    {
                        Title = this.Configuration[ApiTitleKey],
                        Version = this.Configuration[ApiVersionKey],
                        Description = this.Configuration[ApiDescriptionKey]
                    });
            });
        }

        /// <summary>
        /// Configures the HTTP request pipeline.
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <param name="env">The environment.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
                app.UseHttpsRedirection();
            }

            app.UseStaticFiles();
            app.UseSwagger();
            app.UseSwaggerUI(config =>
            {
                config.SwaggerEndpoint(
                    "/swagger/api/swagger.json",
                    $"{this.Configuration[ApiTitleKey]} {this.Configuration[ApiVersionKey]}");
            });

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                );

                endpoints.MapControllers();
            });
        }

        /// <summary>
        /// Gets the path to the XML documentation file.
        /// </summary>
        /// <returns>The XML documentation file path.</returns>
        private string GetXmlDocumentationFilePath()
        {
            string xmlDocumentationFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            string xmlDocumentationFilePath = Path.Combine(AppContext.BaseDirectory, xmlDocumentationFile);
            return xmlDocumentationFilePath;
        }

        /// <summary>
        /// Extract an IP address and mask bits.
        /// </summary>
        /// <param name="cidrIpAddress">The IP address in CIDR notation.</param>
        /// <returns>An IP address and mask bits.</returns>
        private (string, int) GetIpAddressComponents(string cidrIpAddress)
        {
            string[] ipAddressComponents = cidrIpAddress.Split('/');
            return (ipAddressComponents[0], int.Parse(ipAddressComponents[1]));
        }
    }
}
