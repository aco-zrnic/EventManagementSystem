using ConductorSharp.Engine.Builders;
using ConductorSharp.Engine.Util;
using EventManagementSystem.Conductor.Definitions.Generated.Tasks;
using System;
using System.ComponentModel.DataAnnotations;

namespace EventManagementSystem.Conductor.Definitions
{
    public class RegisterEventInput : WorkflowInput<RegisterEventOutput>
    {
        /// <summary>
        /// Name of the event
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// Location of event
        /// </summary>
        [Required]
        public string Venue { get; set; }
        /// <summary>
        /// Date of event
        /// </summary>
        [Required]
        public DateTimeOffset Date { get; set; }
    }
    public class RegisterEventOutput : WorkflowOutput
    {
    }
    [OriginalName("Register_event")]
    public class RegisterEvent : Workflow<RegisterEvent, RegisterEventInput, RegisterEventOutput>
    {
        public RegisterEvent(
            WorkflowDefinitionBuilder<
                RegisterEvent,
                RegisterEventInput,
                RegisterEventOutput
            > builder
        ) : base(builder) { }

        public DbGetEvent CheckIfEventRegistred { get; set; }

        public override void BuildDefinition()
        {
            _builder.AddTask(
                task => task.CheckIfEventRegistred,
                wf =>
                    new()
                    {
                        Date = wf.WorkflowInput.Date,
                        Name = wf.WorkflowInput.Name,
                        Venue = wf.WorkflowInput.Venue
                    }
            );
            _builder.SetOptions(
                options =>
                {
                    options.OwnerApp = "EventManagementSystem";
                    options.OwnerEmail = "test@test.com";
                    options.Version = 1;
                    options.Labels = new[] { "Register", "Event" };
                }
            );
        }
    }
}
