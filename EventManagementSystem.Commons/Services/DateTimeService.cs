using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagementSystem.Commons.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTimeOffset UtcTime
        {
            get => DateTimeOffset.UtcNow;
        }
    }
}
