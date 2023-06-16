using Autofac;
using ConductorSharp.Engine.Extensions;
using EventManagementSystem.Web.Handler;

namespace EventManagementSystem.Web.Modules
{
    public class TaskDefinitionModules : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterWorkerTask<DB_GetEvent>();
        }
    }
}
