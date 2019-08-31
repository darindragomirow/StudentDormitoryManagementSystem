using AutoMapper;
using StudentDormitoryManagementSystem.Models;
using StudentDormitoryManagementSystem.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentDormitoryManagementSystem.Areas.Admin.Controllers
{
    public class BreakdownsController : Controller
    {
        private readonly IBreakdownsService _breakdownsService;
        private readonly IMapper _mapper;

        public BreakdownsController(IBreakdownsService breakdownsService, IMapper mapper)
        {
            _breakdownsService = breakdownsService;
            _mapper = mapper;
        }

        // GET: Admin/Breakdowns
        public ActionResult Index()
        {
            return View();
        }

        // GET: Report/GetAllBreakdowns
        [HttpGet]
        public ActionResult GetAllBreakdowns()
        {
            var allReportedBreakdowns = _breakdownsService.GetAll().Where(b => b.Acknowledged).ToList().Select(x => _mapper.Map<BreakdownViewModel>(x)).ToList();

            return View(allReportedBreakdowns);
        }

        // GET: Breakdowns/GetCurrentBreakdowns
        [HttpGet]
        public ActionResult GetCurrentBreakdowns()
        {
            var currentBreakdowns = _breakdownsService.GetAll().Where(b => !b.Acknowledged).ToList().Select(x => _mapper.Map<BreakdownViewModel>(x)).ToList();

            return View(currentBreakdowns);
        }

        // POST: Breakdowns/Acknowledge
        [HttpPost]
        public ActionResult Acknowledge(Guid id)
        {
            var breakdown = _breakdownsService.GetAll().FirstOrDefault(b => b.Id == id);
            breakdown.Acknowledged = true;

            _breakdownsService.Update(breakdown);

            return RedirectToAction("GetCurrentBreakdowns", "Breakdowns");
        }
    }
}