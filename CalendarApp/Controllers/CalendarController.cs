using CalendarApp.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CalendarApp.Controllers
{
    [RoutePrefix("")]
    public class CalendarController : Controller
    {
        //[Route]
        //public ActionResult Index()
        //{
        //    return View();
        //}

        [Route]
        [Route("Add")]
        [Route("{id:int}")]
        public ActionResult List(int id = 0)
        {
            ItemViewModel<int> model = new ItemViewModel<int>();
            model.Item = id;
            return View(model);
        }
    }
}