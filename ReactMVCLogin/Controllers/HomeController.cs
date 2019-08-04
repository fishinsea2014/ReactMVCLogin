using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;



namespace ReactMVCLogin.Controllers
{
    public class HomeController : Controller
    {
        private Models.Login db = new Models.Login();
        //private Models.User _user = new Models.User()
        //{
        //    UserName = "Jason",
        //    Password = "123456"
        //};

        public HomeController()
        {
            //for (int i = 1; i < 6; i++)
            //{
            //    Models.Team team = new Models.Team()
            //    {
            //        Id = i,
            //        TeamName = "Team" + i.ToString()
            //    };
            //    _teams.Add(team);
            //    db.Set<Models.Team>().Add(team);
            //}

            //Models.User user1 = new Models.User()
            //{
            //    UserName = "Jason",
            //    Password = "123456"
            //};

            //db.UserInfo.Add(user1);

            //db.SaveChanges();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password,string team, bool rememberMe)
        {
            Models.User curUser = new Models.User()
                {
                    UserName = username,
                    Password = password,
                    Team = team
                };
            try
            {
                
                ValidateModel(curUser); // If not validate, throw invalidoperationexception
                if (db.Set<Models.User>().Where(u => u.UserName==username && u.Password == password && u.Team == team) != null)
                {
                    Session["LoginUser"] = curUser;
                    return Json(new {result = "ok" });
                }

                return Json(new { result = "failed", errorMessage = "Incorrect username or password or team" });
               
                
            }
            catch (DbEntityValidationException vex)
            {
                return Json(new { result = "failed" ,errorMessage = vex.Message });
            }
            catch (InvalidOperationException iex)
            {
                //Check ModelState's error dictionary, and extract them into a object. The covert to a Json and return.
                List<Models.ErrorMessage> errorList = new List<Models.ErrorMessage>(); 
                foreach (var item in ModelState)
                {
                    Models.ErrorMessage eItem = new Models.ErrorMessage();
                    eItem.Field = item.Key;
                    foreach (var error in item.Value.Errors)
                    {
                        if (error.ErrorMessage != null)
                        {
                            eItem.EMessage += error.ErrorMessage + ";";
                        }                        
                    }
                    errorList.Add(eItem);
                }

                return Json(new { result = "failed", errorMessage = errorList });
            }
            catch (Exception ex)
            {

                return Json(new { result = "failed", errorMessage = ex.Message });
            }
            
        }

        [HttpGet]
        public ActionResult Teams()
        {           

            var teams = db.Set<Models.Team>().Where<Models.Team>( x => true).ToList();

            return Json(teams, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Main()
        {
            Models.User user = Session["LoginUser"] as Models.User;
            ViewBag.Message = "Log in succeed. Wellcome..."+ user.UserName;
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