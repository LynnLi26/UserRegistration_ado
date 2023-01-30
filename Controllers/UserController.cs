using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserRegistration_ado.DAL;
using UserRegistration_ado.Models;

namespace UserRegistration_ado.Controllers
{
    public class UserController : Controller
    {
        protected List<User> users = new List<User>();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(User user)
        {
            if(ModelState.IsValid)
            {
                var result = new DAL_Layer().AddUser(user);
                return RedirectToAction("GetInfo");
            }
            return View(user);
        }

        public ActionResult GetInfo()
        {
            DAL_Layer dal_layer = new DAL_Layer();
            DataSet ds = dal_layer.getUser();
            DataTable dt = ds.Tables[0];

            var query = from q in dt.AsEnumerable()
                        select new
                        {
                            ID = q.Field<int>("UserID"),
                            Name = q.Field<string>("Name"),
                            Email = q.Field<string>("Email"),
                            Password = q.Field<string>("Password")
                        };

            foreach (var item in query)
            {
                var emp = new User
                {
                    UserID = item.ID,
                    Name = item.Name,
                    Email = item.Email,
                    Password = item.Password,
                };
                users.Add(emp);
            }
            return View(users);
        }
    }
}