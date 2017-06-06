using CalendarApp.Domain;
using CalendarApp.Models.AddRequest;
using CalendarApp.Models.GetRequest;
using CalendarApp.Models.Responses;
using CalendarApp.Models.UpdateRequest;
using CalendarApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace CalendarApp.Controllers.ApiControllers
{
    [AllowAnonymous]
    [RoutePrefix("api/Calendar")]
    public class CalendarApiController : ApiController
    {

        [Route, HttpPost]
        public HttpResponseMessage Add(CalendarAddRequest model)
        {
            string timenow = DateTime.Now.ToString();

            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
           
            try
            {
                ItemResponse<int> response = new ItemResponse<int>();
                response.Item = CalendarService.InsertCalendarItem(model);
                return Request.CreateResponse(response);
            }
            catch (Exception ex)
            {
                ErrorResponse er = new ErrorResponse(ex.Message);
                return Request.CreateResponse(HttpStatusCode.BadRequest, er);
            }
        }

        [Route("{id:int}"), HttpPut]
        public HttpResponseMessage Update(CalendarUpdateRequest model, int id)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            try
            {
                SuccessResponse response = new SuccessResponse();
                CalendarService.UpdateCalendarItem(model);
                return Request.CreateResponse(response);
            }
            catch (Exception ex)
            {
                ErrorResponse er = new ErrorResponse(ex.Message);
                return Request.CreateResponse(HttpStatusCode.BadRequest, er);
            }
        }

        [Route("{id:int}"), HttpGet]
        public HttpResponseMessage GetCalendarItem(int id)
        {
            try
            {
                ItemResponse<CalendarDomainItem> response = new ItemResponse<CalendarDomainItem>();
                response.Item = CalendarService.GetCalendarItem(id);
                return Request.CreateResponse(response);
            }
            catch (Exception ex)
            {
                ErrorResponse er = new ErrorResponse(ex.Message);
                return Request.CreateResponse(HttpStatusCode.BadRequest, er);
            }
        }

        [Route, HttpGet]
        public HttpResponseMessage GetCalendarList()
        {
            try
            {
                ItemsResponse<CalendarDomainItem> response = new ItemsResponse<CalendarDomainItem>();
                response.Items = CalendarService.GetCalendarItemList();
                return Request.CreateResponse(response);
            }
            catch (Exception ex)
            {
                ErrorResponse er = new ErrorResponse(ex.Message);
                return Request.CreateResponse(HttpStatusCode.BadRequest, er);
            }
        }

        [Route("{DateName}{Date:DateTime}"), HttpGet]
        public HttpResponseMessage GetCalendarListByDate(DateTime Date)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            try
            {
                ItemsResponse<CalendarDomainItem> response = new ItemsResponse<CalendarDomainItem>();
                response.Items = CalendarService.GetCalendarItemListByDate(Date);
                return Request.CreateResponse(response);
            }
            catch (Exception ex)
            {
                ErrorResponse er = new ErrorResponse(ex.Message);
                return Request.CreateResponse(HttpStatusCode.BadRequest, er);
            }
        }

        [Route("{id:int}"), HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                SuccessResponse response = new SuccessResponse();
                CalendarService.DeleteCalendarItem(id);
                return Request.CreateResponse(response);
            }
            catch (Exception ex)
            {
                ErrorResponse er = new ErrorResponse(ex.Message);
                return Request.CreateResponse(HttpStatusCode.BadRequest, er);
            }
        }

    }
}