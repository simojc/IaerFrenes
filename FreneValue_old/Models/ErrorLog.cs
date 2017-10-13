using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreneValue.Models
{
    public class ErrorLog
    {
        public int id { get; set; }
        public string message { get; set; }
        public string controllername { get; set; }
        public string useragent { get; set; }
        public string stacktrace { get; set; }
        public string sessionud { get; set; } 
        public string targetresult { get; set; }        
        public DateTime datetime { get; set; }
    }
}