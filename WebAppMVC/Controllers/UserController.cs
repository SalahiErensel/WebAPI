using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Web.Mvc;
using WebAppMVC.Models;

namespace WebAppMVC.Controllers
{
    public class UserController : Controller
    {
        //Consuming Web API
        Uri baseAddress = new Uri("https://localhost:44354/api");
        HttpClient client;

        //Constructor
        public UserController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }

        public ActionResult Index()
        {
            List<UserViewModel> modelList = new List<UserViewModel>();
            HttpResponseMessage responseMessage = client.GetAsync(client.BaseAddress + "/user").Result;

            if(responseMessage.IsSuccessStatusCode)
            {
                string data = responseMessage.Content.ReadAsStringAsync().Result;
                modelList = JsonConvert.DeserializeObject<List<UserViewModel>>(data);

            }

            return View(modelList);
        }

        public ActionResult createUser() 
        {
            return View();
        }

        [HttpPost]
        public ActionResult createUser(UserViewModel model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage responseMessage = client.PostAsync(client.BaseAddress + "/user", content).Result;
            if(responseMessage.IsSuccessStatusCode )
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        public ActionResult editUser(int id)
        {
            UserViewModel model= new UserViewModel();
            HttpResponseMessage responseMessage = client.GetAsync(client.BaseAddress + "/user/"+id).Result;

            if (responseMessage.IsSuccessStatusCode)
            {
                string data = responseMessage.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<UserViewModel>(data);
            }

            return View("createUser" , model);
        }

        [HttpPost]
        public ActionResult editUser(UserViewModel model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage responseMessage = client.PutAsync(client.BaseAddress + "/user" + model.Id, content).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View("createUser", model);
        }
    }

}