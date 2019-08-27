﻿using System;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using StudentDormitoryManagementSystem.Areas.Admin.Models;
using StudentDormitoryManagementSystem.Contracts.Identity;
using StudentDormitoryManagementSystem.Data.Model.Models;
using StudentDormitoryManagementSystem.Services.Contracts;
using StudentDormitoryManagementSystem.Common;

namespace StudentDormitoryManagementSystem.Areas.Admin
{
    [Authorize]
    public class AdminItemsController : Controller
    {
        private readonly IItemsService itemsService;
        private readonly IRoomsService roomService;
        private readonly IItemCategoriesService itemCategoriesService;
        private readonly IApplicationUserManager userManager;
        private readonly IInventoriesService inventoriesService;
        private readonly IMapper mapper;

        public AdminItemsController() { }

        public AdminItemsController(IItemsService itemsService, IRoomsService roomService, IItemCategoriesService itemCategoriesService, IApplicationUserManager userManager,IInventoriesService inventoriesService, IMapper mapper)
        {
            this.itemsService = itemsService;
            this.roomService = roomService;
            this.itemCategoriesService = itemCategoriesService;
            this.userManager = userManager;
            this.inventoriesService = inventoriesService;
            this.mapper = mapper;
        }

        // GET: Admin/AdminItems
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }


        // GET: Admin/AdminItems/AddNewItem
        [HttpGet]
        public ActionResult AddNewItem()
        {
            return View(new AddItemViewModel());
        }

        // POST: Admin/AdminItems/AddNewItem
        [HttpPost]
        public ActionResult AddNewItem(AddItemViewModel m)
        {
            if (!this.ModelState.IsValid) return View();

            //Getting model properties
            m.Id = Guid.NewGuid();
            m.DateRegistered = DateTime.Now;

            var room = roomService.GetAll().FirstOrDefault(r => r.Number == m.Room.Number);
            if (room != null)
                m.Room = room;
            else
                throw new Exception("The room does not exist!");

            var category = itemCategoriesService.GetAll().FirstOrDefault(c => c.CategoryName == m.ItemCategory.CategoryName);
            if (category != null)
                m.ItemCategory = category;
            else
            {
                var newCategory = new ItemCategory
                {
                    CategoryName = m.ItemCategory.CategoryName,
                    Id = Guid.NewGuid(),
                };

                itemCategoriesService.Add(newCategory);
                m.ItemCategory = newCategory;
            }

            if (m.ExpirationDate == null || m.ExpirationDate == DateTime.MinValue)
                m.ExpirationDate = DateTime.MaxValue;


            var item = this.mapper.Map<AddItemViewModel, Item>(m);


            if(m.Type == "Personal")
            {
                //Adding the item to the user inventory
                var owner = this.userManager.Users.FirstOrDefault(u => u.UserName == m.Owner);
                if (owner.UserName == Constants.AdminEmail)

                    owner?.StudentInfo.Inventory.Items.Add(item);

                inventoriesService.Update(owner?.StudentInfo.Inventory);
            }
            else if(m.Type == "Shared")
            {
                var roomInventory = inventoriesService.GetAll().SingleOrDefault(i => i.Room.Number == m.Room.Number);
                roomInventory.Items.Add(item);
                inventoriesService.Update(roomInventory);
            }

            ////Adding the mapped object to the repository
            //this.itemsService.Add(item);

            this.ModelState.Clear();

            return View();
        }
    }
}