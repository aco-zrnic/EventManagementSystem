// <autogenerated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </autogenerated>
using ConductorSharp.Engine.Model;
using ConductorSharp.Engine.Util;
using MediatR;
using Newtonsoft.Json;
using ConductorSharp.Engine.Builders;

namespace EventManagementSystem.Conductor.Definitions.Generated.Tasks
{
    public partial class DbGetEventInput : IRequest<DbGetEventOutput>
    {
        /// <originalName>
        /// name
        /// </originalName>
        [JsonProperty("name")]
        public object Name { get; set; }

        /// <originalName>
        /// venue
        /// </originalName>
        [JsonProperty("venue")]
        public object Venue { get; set; }

        /// <originalName>
        /// date
        /// </originalName>
        [JsonProperty("date")]
        public object Date { get; set; }
    }

    public partial class DbGetEventOutput
    {
        /// <originalName>
        /// event
        /// </originalName>
        [JsonProperty("event")]
        public object Event { get; set; }
    }

    /// <originalName>
    /// DB_get_event
    /// </originalName>
    /// <ownerEmail>
    /// undefined@undefined.local
    /// </ownerEmail>
    /// <node>
    /// 
    /// </node>
    /// <summary>
    /// Missing description
    /// </summary>
    [OriginalName("DB_get_event")]
    public partial class DbGetEvent : SimpleTaskModel<DbGetEventInput, DbGetEventOutput>
    {
    }
}