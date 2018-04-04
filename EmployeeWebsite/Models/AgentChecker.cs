using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeWebsite
{
    public class AgentChecker
    {

        public bool IsIE { get; set; }

        // makes sure check is done only when object is created
        public AgentChecker(IHttpContextAccessor accessor)
        {
            string UA = accessor.HttpContext.Request.Headers["User-Agent"].ToString();
            if (UA.Contains("Trident") || UA.Contains("MSIE"))
            {
                IsIE = true;
            }
            else
            {
                IsIE = false; 
            }
        }

        // optional to simplify usage further. 
        public static implicit operator bool(AgentChecker checker) => checker.IsIE;

    }
}
