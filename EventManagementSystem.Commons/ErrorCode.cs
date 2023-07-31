using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagementSystem.Commons
{
    public enum ErrorCode
    {
        GENERAL = 1000,
        ITEM_NOT_FOUND = 2000,
        BAD_REQUEST = 3000,
        GATEWAY_ERROR = 8000
    }
}
