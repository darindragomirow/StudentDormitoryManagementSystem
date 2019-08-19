using StudentDormitoryManagementSystem.Models;
using StudentDormitoryManagementSystem.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using StudentDormitoryManagementSystem.Common;
using StudentDormitoryManagementSystem.Contracts.Identity;
using StudentDormitoryManagementSystem.Data.Model.Models;
using StudentDormitoryManagementSystem.Services;

namespace StudentDormitoryManagementSystem.Controllers
{
    public class MyInventoryController : Controller
    {
        private readonly IItemsService _itemsService;
        private readonly IMapper _mapper;
        private readonly IApplicationUserManager _userManager;

        public MyInventoryController(IItemsService itemsService, IApplicationUserManager userManager, IMapper mapper)
        {
            this._itemsService = itemsService;
            this._userManager = userManager;
            this._mapper = mapper;
        }

        // GET: MyInventory/AvailableInventory
        public ActionResult AvailableInventory()
        {
            var userId = User.Identity.GetUserId();
            var user = this._userManager.Users.FirstOrDefault(u => u.Id == userId);

            if(user == null)
                return HttpNotFound($"The user with id: {userId} could not be found!");

            ItemsViewModel model = new ItemsViewModel();

            if (HasUserItems(user))
            {
                var userItems = user?.StudentInfo.Inventory.Items.ToList();
                var userItemsModels = userItems.Select(x => this._mapper.Map<ItemViewModel>(x)).ToList();

                model.RoomNumber = user.StudentInfo.Room.Number;
                model.AvailableItems = userItemsModels;
                model.TotalCount = model.AvailableItems.Count;
            }

            //var allItems = this._itemsService
            //    .GetAll()
            //    .OrderByDescending(x => x.CreatedOn.Value)
            //    .ToList()
            //    .Select(x => this._mapper.Map<ItemViewModel>(x));

            //var model = new ItemsViewModel
            //{
            //    AvailableItems = allItems.ToList()
            //};

            //if (model.AvailableItems == null)
            //    return View(model);

            //model.TotalCount = model.AvailableItems.Count;

            return View(model);
        }

        private bool HasUserItems(User user)
        {
            return user?.StudentInfo != null
                   && user?.StudentInfo.Inventory != null
                   && user?.StudentInfo.Inventory.Items.Count > 0;
        }

        // GET: MyInventory/GetProductById?=id
        [HttpGet]
        [Authorize]
        public ActionResult GetProductById(Guid? id)
        {

            var allItems = this._itemsService.GetAll().ToList();

            var model = this._itemsService.GetAll().Where(x => x.Id == id)
                .ToList()
                .Select(x => this._mapper.Map<ItemViewModel>(x))
                .FirstOrDefault();

            if (model != null)
            {
                return PartialView("_GridEditPartial", model);
            }
            else
            {
                return HttpNotFound($"The item with id: {id} could not be found!");
            }
        }

        // POST: MyInventory/UpdateProduct
        [HttpPost]
        [Authorize]
        public ActionResult UpdateProduct(ItemViewModel modified)
        {
            if (modified != null)
            {

                var itemForUpdate = _itemsService.GetAll().Where(x => x.Id == modified.Id)
                .ToList()
                .FirstOrDefault();

                UpdateItemProperties(modified, ref itemForUpdate);

                try
                {
                    _itemsService.Update(itemForUpdate);
                }
                catch (Exception ex)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);
                }

                return Redirect("myinventory/availableinventory");
            }
            else
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "The modified item is not valid and cannot be updated!");
        }

        private void UpdateItemProperties(ItemViewModel modified, ref Item itemForUpdate)
        {
            PropertyCopier<ItemViewModel, Item>.Copy(modified, itemForUpdate);
        }
    }
}