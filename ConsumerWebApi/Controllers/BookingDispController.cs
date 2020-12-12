using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using ConsumerWebApi.Models;

namespace ConsumerWebApi.Controllers
{
    public class BookingDispController : Controller
    {
        public ActionResult Index()
        {
            IEnumerable<Booking> books = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:51485/api/");
                var responseTask = client.GetAsync("Book");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Booking>>();
                    readTask.Wait();
                    books = readTask.Result;
                }
                else
                {
                    books = Enumerable.Empty<Booking>();
                    ModelState.AddModelError(string.Empty, "Server error");
                }
            }
            return View(books);
        }
        public ActionResult ajaxGet()
        {
            return View();
        }
    }
}
