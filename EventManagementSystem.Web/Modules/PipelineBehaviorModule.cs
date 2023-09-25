using Autofac;
using ConductorSharp.Engine.Extensions;
using EventManagementSystem.Commons.Behavior;
using EventManagementSystem.Web.Handler;
using MediatR;

namespace EventManagementSystem.Web.Modules
{
    public class PipelineBehaviorModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(LoggingPipelineBehavior<,>)).As(typeof(IPipelineBehavior<,>));
        }
    }
}
