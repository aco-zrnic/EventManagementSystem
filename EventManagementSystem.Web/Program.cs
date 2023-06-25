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
appBuilder.Services.AddSwaggerGen();


var app = appBuilder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseAuthorization();


app.MapControllers();


app.Run();
