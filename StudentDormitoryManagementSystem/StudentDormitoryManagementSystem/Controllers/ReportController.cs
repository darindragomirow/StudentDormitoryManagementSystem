using AutoMapper;
using StudentDormitoryManagementSystem.Data.Model.Models;
using StudentDormitoryManagementSystem.Models;
using StudentDormitoryManagementSystem.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentDormitoryManagementSystem.Controllers
{
    [Authorize]
    public class ReportController : Controller
    {
        private readonly IBreakdownsService _breakdownsService;
        private readonly IMapper _mapper;

        public ReportController(IBreakdownsService breakdownsService, IMapper mapper)
        {
            _breakdownsService = breakdownsService;
            _mapper = mapper;
        }

        // GET: Report
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        // GET: Report/CreateReport
        [HttpGet]
        public ActionResult ReportBreakdown()
        {
            var severityList = new List<string> { "Low", "Medium", "High" };
            ViewBag.SeverityList = new SelectList(severityList);

            return View();
        }

        // POST: Report/CreateReport
        [HttpPost]
        public ActionResult ReportBreakdown(BreakdownViewModel model)
        {
            model.Id = Guid.NewGuid();
            var currentUser = User.Identity.Name;
            model.Sender = currentUser;

            string severitySelected = Request.Form["SeverityList"].ToString();
            model.Severity = severitySelected;

            var breakdown = _mapper.Map<Breakdown>(model);

            if (breakdown != null)
                _breakdownsService.Add(breakdown);

            return RedirectToAction("Index", "Home");
        }
    }
}