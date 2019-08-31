using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentDormitoryManagementSystem.Controllers
{
    public class CodeController : Controller
    {
        // GET: Code
        public ActionResult Index()
        {
            return View();
        }

        // GET: Code/Qrcode
        [HttpGet]
        public ActionResult Qrcode()
        {
            List<ExpandOptionsqrcode> position = new List<ExpandOptionsqrcode>();
            position.Add(new ExpandOptionsqrcode() { text = "Low", value = "7" });
            position.Add(new ExpandOptionsqrcode() { text = "Medium", value = "15" });
            position.Add(new ExpandOptionsqrcode() { text = "Quartile", value = "25" });
            position.Add(new ExpandOptionsqrcode() { text = "High", value = "30" });

            ViewBag.position = position;




            return View();
        }
    }

    public class ExpandOptionsqrcode
    {
        public string text;
        public string value;
    }
}
