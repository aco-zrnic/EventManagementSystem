using Autofac;
using Autofac.Extensions.DependencyInjection;
using ConductorSharp.Engine;
using ConductorSharp.Engine.Extensions;
using EventManagementSystem.Commons.Services;
using EventManagementSystem.Commons.Behavior;
using EventManagementSystem.Ticketmaster.Modules;
using EventManagementSystem.Ticketmaster.Options;
using MediatR;
using Microsoft.AspNetCore.Mvc.Filters;
using EventManagementSystem.Ticketmaster.Util;


var appBuilder = WebApplication.CreateBuilder(args);


// Add services to the container.
appBuilder.Host
    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(
        builder =>
        {
            //builder.RegisterModule<TaskDefinitionModules>();

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

            builder.RegisterType<DateTimeService>().As<IDateTimeService>();
            builder.RegisterType<LoggingActionFilter>();
            builder.RegisterType<TicketmasterClient>();
        }
    );


appBuilder.Services.AddAutoMapper(typeof(Program).Assembly);
appBuilder.Services
    .AddOptions<GatewayOptions>()
    .Bind(appBuilder.Configuration.GetSection("Gateway"))
    .ValidateDataAnnotations();
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
