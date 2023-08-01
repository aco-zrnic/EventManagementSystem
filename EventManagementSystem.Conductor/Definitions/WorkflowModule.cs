using Autofac;
using ConductorSharp.Engine.Extensions;

namespace EventManagementSystem.Conductor.Definitions
{
    public class WorkflowModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterWorkflow<RegisterEvent>();
        }
    }
}
