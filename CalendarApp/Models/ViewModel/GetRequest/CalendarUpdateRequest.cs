using CalendarApp.Models.AddRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CalendarApp.Models.UpdateRequest
{
    public class CalendarUpdateRequest : CalendarAddRequest
    {
        public int Id { get; set; }
    }
}