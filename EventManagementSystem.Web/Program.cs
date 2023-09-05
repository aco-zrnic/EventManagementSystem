using Autofac;
using Autofac.Extensions.DependencyInjection;
using EventManagementSystem.Web;
using ConductorSharp.Engine;
using ConductorSharp.Engine.Extensions;
using EventManagementSystem.Web.Entities;
using Microsoft.EntityFrameworkCore;
using EventManagementSystem.Commons.Services;
using EventManagementSystem.Web.Modules;
using MediatR;
using Microsoft.AspNetCore.Mvc.Filters;
using EventManagementSystem.Commons.Behavior;
using EventManagementSystem.Web.Auth0;
using Microsoft.OpenApi.Models;
using EventManagementSystem.Web.Auth0.OpenApiSecurity;
using Swashbuckle.AspNetCore.SwaggerUI;
using Swashbuckle.AspNetCore.Filters;

var appBuilder = WebApplication.CreateBuilder(args);


// Add services to the container.
appBuilder.Host
    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(
        builder =>
        {
            builder.RegisterModule<TaskDefinitionModules>();

            builder
                .AddConductorSharp(
                    apiPath: appBuilder.Configuration.GetValue<string>("Conductor:ApiUrl"),
                    baseUrl: appBuilder.Configuration.GetValue<string>("Conductor:BaseUrl"),
                    preventErrorOnBadRequest: appBuilder.Configuration.GetValue<bool>(
                        "Conductor:PreventErrorOnBadRequest"
                    )
                )
                .AddExecutionManager(
                    maxConcurrentWorkers: appBuilder.Configuration.GetValue<int>(
                        "Conductor:MaxConcurrentWorkers"
                    ),
                    sleepInterval: appBuilder.Configuration.GetValue<int>(
                        "Conductor:SleepInterval"
                    ),
                    longPollInterval: appBuilder.Configuration.GetValue<int>(
                        "Conductor:LongPollInterval"
                    ),
                    domain: appBuilder.Configuration.GetValue<string>("Conductor:WorkerDomain"),
                    handlerAssemblies: typeof(Program).Assembly
                )
                .AddPipelines(
                    pipelines =>
                    {
                        pipelines.AddContextLogging();
                        pipelines.AddRequestResponseLogging();
                        pipelines.AddValidation();
                    }
                );
            builder.RegisterType<EmContext>().InstancePerLifetimeScope();
            builder.RegisterType<DateTimeService>().As<IDateTimeService>();
            builder.RegisterType<LoggingActionFilter>();
        }
    );
appBuilder.Services.AddDbContext<EmContext>(
    contextLifetime: ServiceLifetime.Transient,
    optionsAction: options =>
        options
            .UseNpgsql(appBuilder.Configuration.GetConnectionString("Db"))
            .UseSnakeCaseNamingConvention()
);


appBuilder.Services.AddAutoMapper(typeof(Program).Assembly);
appBuilder.Services.AddControllers();
appBuilder.Services.AddRouting(options => options.LowercaseUrls = true);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
appBuilder.Services.AddEndpointsApiExplorer();
appBuilder.Services.AddAuthServiceCollection(appBuilder.Configuration);
appBuilder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo() { Description = " Event Management Project" });
    string securityDefinitionName = appBuilder.Configuration.GetValue<string>("SwaggerUISecurityMode") ?? "Bearer";
    OpenApiSecurityScheme securityScheme = new OpenApiBearerSecurityScheme();
    OpenApiSecurityRequirement securityRequirement = new OpenApiBearerSecurityRequirement(securityScheme);

    if (securityDefinitionName.ToLower() == "oauth2")
    {
        securityScheme = new OpenApiOAuthSecurityScheme(appBuilder.Configuration.GetValue<string>("Auth0:Domain"), appBuilder.Configuration.GetValue<string>("Auth0:Audience"));
        securityRequirement = new OpenApiOAuthSecurityRequirement();
    }

    c.AddSecurityDefinition(securityDefinitionName, securityScheme);
    c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
    c.AddSecurityRequirement(securityRequirement);
});


var app = appBuilder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Event Management V1");
        c.DocExpansion(DocExpansion.None);

        if (appBuilder.Configuration["SwaggerUISecurityMode"]?.ToLower() == "oauth2")
        {
            c.OAuthClientId(appBuilder.Configuration["Auth0:ClientId"]);
            c.OAuthClientSecret(appBuilder.Configuration["Auth0:ClientSecret"]);
            c.OAuthAppName("Event Management");
            c.OAuthAdditionalQueryStringParams(new Dictionary<string, string> { { "audience", appBuilder.Configuration["Auth0:Audience"] } });
            c.OAuthUseBasicAuthenticationWithAccessCodeGrant();
        }
        else
        {
            //Configure Swagger to use JWT for authentication
            c.OAuthClientId(appBuilder.Configuration["Auth0:ClientId"]);
            c.OAuthAppName("Event Management");
        }
    });
}


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();
