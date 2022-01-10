using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Azure.Identity;
using BWHazel.Api.Web;

const string AzureAdTenantIdKey = "AzureAD:TenantId";
const string AzureAdClientIdKey = "AzureAD:ClientId";
const string AzureAdClientSecretKey = "AzureAD:ClientSecret";
const string SecretsKeyVaultKey = "Secrets:KeyVault";

Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((context, config) =>
    {
        IConfigurationRoot rootConfig = config
            .AddEnvironmentVariables()
            .Build();

        if (!context.HostingEnvironment.IsEnvironment("Test"))
        {
            config.AddAzureKeyVault(
            new Uri($"https://{rootConfig[SecretsKeyVaultKey]}.vault.azure.net"),
                new ClientSecretCredential(
                    rootConfig[AzureAdTenantIdKey],
                    rootConfig[AzureAdClientIdKey],
                    rootConfig[AzureAdClientSecretKey]
                )
            );
        }
    })
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.UseStartup<Startup>();
    })
    .Build()
    .Run();