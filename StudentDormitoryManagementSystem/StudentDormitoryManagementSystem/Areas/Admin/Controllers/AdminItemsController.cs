using System;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using StudentDormitoryManagementSystem.Areas.Admin.Models;
using StudentDormitoryManagementSystem.Contracts.Identity;
using StudentDormitoryManagementSystem.Data.Model.Models;
using StudentDormitoryManagementSystem.Services.Contracts;
using System.Collections.Generic;
using System.Web;
using System.IO;
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
        private readonly IImagesService imagesService;
        private readonly IMapper mapper;

        public AdminItemsController() { }

        public AdminItemsController(IItemsService itemsService, IRoomsService roomService,
            IItemCategoriesService itemCategoriesService, IApplicationUserManager userManager,
            IInventoriesService inventoriesService, IImagesService imagesService, IMapper mapper)
        {
            this.itemsService = itemsService;
            this.roomService = roomService;
            this.itemCategoriesService = itemCategoriesService;
            this.userManager = userManager;
            this.inventoriesService = inventoriesService;
            this.imagesService = imagesService;
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
            var roomList = GetExistingRooms();
            ViewBag.RoomList = new SelectList(roomList);

            var categorylist = GetExistingCategories();
            ViewBag.CategoryList = new SelectList(categorylist);

            var userList = GetExistingUsers();
            ViewBag.UserList = new SelectList(userList);

            var itemTypeList = Enum.GetNames(typeof(ItemType)).ToList();
            ViewBag.ItemTypeList = new SelectList(itemTypeList);

            var sizeList = Enum.GetNames(typeof(Size)).ToList();
            ViewBag.SizeList = new SelectList(sizeList);

            var canReserveList = new List<string> { "No", "Yes" };
            ViewBag.CanReserveList = new SelectList(canReserveList);

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

            string roomSelected = Request.Form["RoomList"].ToString();

            var roomSelectedAsShort = short.Parse(roomSelected);
            var room = roomService.GetAll().FirstOrDefault(r => r.Number == roomSelectedAsShort);
            //var room = roomService.GetAll().FirstOrDefault(r => r.Number == m.Room.Number);
            if (room != null)
                m.Room = room;
            else
                throw new Exception("The room does not exist!");

            string categorySelected = Request.Form["CategoryList"].ToString();

            var category = itemCategoriesService.GetAll().FirstOrDefault(c => c.CategoryName == categorySelected);
            //var category = itemCategoriesService.GetAll().FirstOrDefault(c => c.CategoryName == m.ItemCategory.CategoryName);
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

            var typeSelected = Request.Form["ItemTypeList"].ToString();
            m.Type = typeSelected;

            var canReserveSelected = Request.Form["CanReserveList"].ToString();
            m.canReserve = canReserveSelected == "Yes" ? true : false;

            var sizeSelected = Request.Form["SizeList"].ToString();
            m.Size = sizeSelected;

            var item = this.mapper.Map<AddItemViewModel, Item>(m);


            if (typeSelected == "Personal")
            {
                //Adding the item to the user inventory
                var userSelected = Request.Form["UserList"].ToString();
                var owner = this.userManager.Users.FirstOrDefault(u => u.UserName == userSelected);
                if (owner.StudentInfo != null && owner.StudentInfo.Inventory != null)
                    owner?.StudentInfo.Inventory.Items.Add(item);

                inventoriesService.Update(owner?.StudentInfo.Inventory);
            }
            else if (typeSelected == "Shared")
            {
                var roomInventory = inventoriesService.GetAll().SingleOrDefault(i => i.Room.Number == m.Room.Number);
                roomInventory.Items.Add(item);
                inventoriesService.Update(roomInventory);
            }

            // Get selected image from model, save it to local directory and also its path to the database
            UploadImage(m.FileAttach, m.Id);


            this.ModelState.Clear();

            return Redirect(Url.Action("Index", "AdminItems", new { area = "Admin" }));
        }

        public List<string> GetExistingRooms()
        {
            return roomService.GetAll().Select(r => r.Number.ToString()).Distinct().ToList();
        }

        public List<string> GetExistingCategories()
        {
            return itemCategoriesService.GetAll().Select(c => c.CategoryName).Distinct().ToList();
        }

        public List<string> GetExistingUsers()
        {
            return userManager.Users.ToList().Select(u => u.UserName).ToList();
        }

        private void UploadImage(HttpPostedFileBase ImageFile, Guid? itemId)
        {
            string extension = Path.GetExtension(ImageFile.FileName);
            string fileName = Path.GetFileNameWithoutExtension(ImageFile.FileName) + "_" + GetCurrentDateAsString() + extension;

            var path = Constants.UploadedImagesPath + fileName;
            var fullPath = Path.Combine(Server.MapPath($"~{Constants.UploadedImagesPath}"), fileName);

            // Upload the image to local directory
            ImageFile.SaveAs(fullPath);

            // Save image path to database
            var image = new Image
            {
                Path = path,
                Name = fileName,
                ItemId = itemId
            };

            imagesService.Add(image);
        }

        private string GetCurrentDateAsString()
        {
            var now = DateTime.Now;
            return $"{now.Year}{now.Month}{now.Day}{now.Hour}{now.Minute}";
        }
    }
}