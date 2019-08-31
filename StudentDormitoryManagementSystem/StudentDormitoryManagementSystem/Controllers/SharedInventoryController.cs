using AutoMapper;
using Microsoft.AspNet.Identity;
using StudentDormitoryManagementSystem.Contracts.Identity;
using StudentDormitoryManagementSystem.Data.Model.Models;
using StudentDormitoryManagementSystem.Models;
using StudentDormitoryManagementSystem.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace StudentDormitoryManagementSystem.Controllers
{
    [Authorize]
    public class SharedInventoryController : Controller
    {
        private readonly IItemsService _itemsService;
        private readonly IApplicationUserManager _userManager;
        private readonly IInventoriesService _inventoriesService;
        private readonly IReservationsService _reservationsService;
        private readonly IImagesService _imagesService;
        private readonly IMapper _mapper;

        public SharedInventoryController(IItemsService itemsService, IApplicationUserManager userManager, IInventoriesService inventoriesService, IReservationsService reservationsService, IImagesService imagesService, IMapper mapper)
        {
            _itemsService = itemsService;
            _userManager = userManager;
            _inventoriesService = inventoriesService;
            _reservationsService = reservationsService;
            _imagesService = imagesService;
            _mapper = mapper;
        }

        // GET: SharedInventory
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        // GET: GetSharedItems?roomNumber={number}
        [HttpGet]
        public ActionResult GetSharedItems(short roomNumber)
        {
            var inventory = _inventoriesService.GetAll().SingleOrDefault(i => i.Room.Number == roomNumber);
            var inventoryItems = inventory?.Items.ToList().Select(x => _mapper.Map<ItemViewModel>(x)).ToList();

            ItemsViewModel model = new ItemsViewModel();

            if (inventoryItems != null && inventoryItems?.Count > 0)
            {
                SetItemsCurrentStatus(inventoryItems);
                SetItemsImages(inventoryItems);
                model.RoomNumber = inventory.Room.Number;
                model.AvailableItems = inventoryItems;
                model.TotalCount = inventoryItems.Count;
            }

            return View(model);
        }

        

        private void SetItemsCurrentStatus(List<ItemViewModel> inventoryItems)
        {
            foreach (var item in inventoryItems)
            {
                var currentItemReservations = _reservationsService.GetAll().Where(r => r.ItemId == item.Id && r.EndTime > DateTime.Now).ToList();
                bool isAvailable = true;
                var availableAt = DateTime.MaxValue;
                foreach (var res in currentItemReservations)
                {
                    if (res.StartTime <= DateTime.Now && res.EndTime > DateTime.Now)
                    {
                        isAvailable = false;
                        availableAt = res.EndTime;
                    }
                    else
                    {
                        if (res.StartTime == availableAt)
                            availableAt = res.EndTime;
                    }
                }

                if (isAvailable)
                    item.CurrentStatus = "Available";
                else
                    item.CurrentStatus = "Reserved, available after " + Convert.ToInt32((availableAt - DateTime.Now).TotalMinutes) + " mins";
            }
        }

        private void SetItemsImages(List<ItemViewModel> inventoryItems)
        {
            foreach (var item in inventoryItems)
            {
                var imagePath = _imagesService.GetAll().FirstOrDefault(img => img.ItemId == item.Id)?.Path;
                item.ImagePath = imagePath != null ? Common.Constants.LocalServerPath + imagePath : Common.Constants.NotFoundImagePath;
            }
        }

        //GET: GetItemReservations?id={id}
        [HttpGet]
        public ActionResult GetItemReservations(Guid? id)
        {
            var model = this._itemsService.GetAll().Where(x => x.Id == id)
                .ToList()
                .Select(x => this._mapper.Map<ItemViewModel>(x))
                .FirstOrDefault();

            return View(model);
        }


        public JsonResult GetData(Guid? id)
        {
            var data = _reservationsService.GetAll().Where(r => r.ItemId == id).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateData(EditParams param, Guid? id)
        {
            var userId = User.Identity.GetUserId();
            var user = this._userManager.Users.FirstOrDefault(u => u.Id == userId);

            if (param.action == "insert" || (param.action == "batch" && param.added != null)) // this block of code will execute while inserting the appointments
            {
                var value = (param.action == "insert") ? param.value : param.added[0];
                DateTime startTime = Convert.ToDateTime(value.StartTime);
                DateTime endTime = Convert.ToDateTime(value.EndTime);
                ItemReservation appointment = new ItemReservation()
                {
                    Id = value.Id,
                    StartTime = startTime,
                    EndTime = endTime,
                    Subject = value.Subject,
                    IsAllDay = value.IsAllDay,
                    StartTimezone = value.StartTimezone,
                    EndTimezone = value.EndTimezone,
                    RecurrenceRule = value.RecurrenceRule,
                    RecurrenceID = value.RecurrenceID,
                    RecurrenceException = value.RecurrenceException,
                    ItemId = (Guid)id
                };

                //Automatically commit changes after adding the new appointment
                _reservationsService.Add(appointment);
            }
            if (param.action == "update" || (param.action == "batch" && param.changed != null)) // this block of code will execute while updating the appointment
            {
                var value = (param.action == "update") ? param.value : param.changed[0];
                var appointment = _reservationsService.GetAll().Single(a => a.Id == value.Id);
                if (appointment != null)
                {
                    DateTime startTime = Convert.ToDateTime(value.StartTime);
                    DateTime endTime = Convert.ToDateTime(value.EndTime);
                    appointment.StartTime = startTime;
                    appointment.EndTime = endTime;
                    appointment.StartTimezone = value.StartTimezone;
                    appointment.EndTimezone = value.EndTimezone;
                    appointment.Subject = value.Subject;
                    appointment.IsAllDay = value.IsAllDay;
                    appointment.RecurrenceRule = value.RecurrenceRule;
                    appointment.RecurrenceID = value.RecurrenceID;
                    appointment.RecurrenceException = value.RecurrenceException;
                }
                // Update the appointment and automatically commit changes
                _reservationsService.Update(appointment);
            }
            if (param.action == "remove" || (param.action == "batch" && param.deleted != null)) // this block of code will execute while removing the appointment
            {
                if (param.action == "remove")
                {
                    var key = Guid.Parse(param.key);
                    var appointment = _reservationsService.GetAll().Single(a => a.Id == key);

                    if (appointment != null)
                        _reservationsService.Delete(appointment);
                }
                else
                {
                    foreach (var apps in param.deleted)
                    {
                        var appointment = _reservationsService.GetAll().Single(a => a.Id == apps.Id);
                        if (apps != null)
                            _reservationsService.Delete(appointment);
                    }
                }
            }

            var data = _reservationsService.GetAll().ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public class EditParams
        {
            public string key { get; set; }
            public string action { get; set; }
            public List<ItemReservation> added { get; set; }
            public List<ItemReservation> changed { get; set; }
            public List<ItemReservation> deleted { get; set; }
            public ItemReservation value { get; set; }
        }
    }
}