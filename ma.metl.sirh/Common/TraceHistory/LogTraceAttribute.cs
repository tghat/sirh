using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ma.metl.sirh.Common.TraceHistory
{
    [AttributeUsage(AttributeTargets.Method)]
    public class LogTraceAttribute : System.Attribute
    {
        string action;

        public string Action
        {
            get { return action; }
            set { action = value; }
        }
    }
}