using AutoMapper;
using StudentDormitoryManagementSystem.Areas.Profile.Models;
using StudentDormitoryManagementSystem.Contracts.Identity;
using System.Linq;
using System.Web.Mvc;

namespace StudentDormitoryManagementSystem.Areas.Admin.Controllers
{
    public class UsersController : Controller
    {
        private readonly IApplicationUserManager _userManager;
        private readonly IMapper _mapper;

        public UsersController(IApplicationUserManager userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        // GET: Admin/ListUsers
        [HttpGet]
        public ActionResult ListUsers()
        {
            var listOfUsers = _userManager.Users.ToList().Select(x => this._mapper.Map<UserViewModel>(x)).ToList();

            var model = new UsersViewModel();

            if(listOfUsers != null)
            {
                model.Users = listOfUsers;
                model.TotalCount = listOfUsers.Count;
            }

            return View(model);
        }
    }
}