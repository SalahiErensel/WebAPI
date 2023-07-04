using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Database;
using WebAPI.Database.Models;

namespace WebAPI.Controllers
{
    public class UserController : ApiController
    {
        DatabaseContext db = new DatabaseContext();

        //Route == /api/user  to get all users using GET request
        public IEnumerable<User> getAllUsers()
        {
            return db.users.ToList();
        }

        //Route == /api/user/{id}  to get user by user id using GET request
        public User getUser(int id)
        {
            return db.users.Find(id);
        }

        //Route == /api/user  to add user using POST request
        [HttpPost]
        public HttpResponseMessage addUser(User user)
        {
            try
            {
                db.users.Add(user);
                db.SaveChanges();
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.Created);
                return response;
            }

            catch(Exception ex) 
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                return response;
            }
        }

        //Route == /api/user/{id}  to update user using PUT request
        [HttpPut]
        public HttpResponseMessage updateUser(int id, User user)
        {
            try
            {
                if(id == user.Id)
                {
                    db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                    return response;
                }
                else
                {
                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NotModified);
                    return response;
                }
            }

            catch (Exception ex)
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                return response;
            }
        }

        //Route == /api/user/{id}  to delete user using DELETE request
        public HttpResponseMessage deleteUser(int id)
        {
            User user = db.users.Find(id);

            if(user != null)
            {
                db.users.Remove(user);
                db.SaveChanges();
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                return response;
            }
            else
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NotFound);
                return response;
            }
        }
    }
}
