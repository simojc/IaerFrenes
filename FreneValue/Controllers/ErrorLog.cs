using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcTestApp2017.Models
{
    public class ErrorLog
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string ControllerName { get; set; }
        public string UserAgent { get; set; }
        public string StackTrace { get; set; }
        public string SessionUd { get; set; } 
        public string TargetResult { get; set; }        
        public DateTime Timestamp { get; set; }
    }
}