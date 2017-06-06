using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CalendarApp.Models.Responses
{
    public class SuccessResponse : BaseResponse
    {
        public SuccessResponse()
        {
            this.IsSuccessfull = true;
        }
    }
}