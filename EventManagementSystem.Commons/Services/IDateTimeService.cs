using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagementSystem.Commons.Services
{
    public interface IDateTimeService
    {
        DateTimeOffset UtcTime { get; }
    }
}
