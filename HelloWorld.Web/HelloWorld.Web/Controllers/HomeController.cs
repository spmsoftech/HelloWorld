using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace HelloWorld.Web.Controllers
{
    public class HomeController : Controller
    {
        private string URL = System.Configuration.ConfigurationManager.AppSettings["ApiURL"];
        public ActionResult Index()
        {

            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(URL);

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                HttpResponseMessage response = client.GetAsync("/api/values").Result;  // Blocking call!
                if (response.IsSuccessStatusCode)
                {
                    // Parse the response body. Blocking!
                    var dataObjects = response.Content.ReadAsStringAsync().Result;
                    List<string> cities = JsonConvert.DeserializeObject<List<String>>(dataObjects);
                    string cityNames = string.Empty;
                    foreach (var city in cities)
                    {
                        if (string.IsNullOrEmpty(cityNames))
                        {
                            cityNames = city;
                        }
                        else
                        {
                            cityNames = cityNames + ";" + city;
                        }
                    }
                    ViewBag.Message = cityNames;
                }
                else
                {
                    ViewBag.Message = response.ReasonPhrase;
                }
            }
            catch(Exception Ex)
            {
                ViewBag.Message = Ex.Message;
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}