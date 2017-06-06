using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CalendarApp.Models.AddRequest
{
    public class CalendarAddRequest
    {
        public string Title { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime Date { get; set; }
    }
}