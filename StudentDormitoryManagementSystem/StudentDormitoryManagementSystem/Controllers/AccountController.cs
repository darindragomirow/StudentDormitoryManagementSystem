using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using StudentDormitoryManagementSystem.Contracts.Identity;
using StudentDormitoryManagementSystem.Data.Model.Models;
using StudentDormitoryManagementSystem.Models;
using StudentDormitoryManagementSystem.Services.Contracts;

namespace StudentDormitoryManagementSystem.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IApplicationUserManager userManager;
        private readonly IApplicationSignInManager signInManager;
        private readonly IAuthenticationManager authenticationManager;
        private readonly IInventoriesService inventoriesService;
        private readonly IRoomsService roomsService;
        private readonly IStudentsService studentsService;

        public AccountController()
        {
        }

        public AccountController(
            IApplicationUserManager userManager,
            IApplicationSignInManager signInManager,
            IAuthenticationManager authenticationManager,
            IInventoriesService inventoriesService,
            IRoomsService roomsService,
            IStudentsService studentsService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.authenticationManager = authenticationManager;
            this.inventoriesService = inventoriesService;
            this.roomsService = roomsService;
            this.studentsService = studentsService;
        }


        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await this.signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User { UserName = model.Email, Email = model.Email };

                if (model.IsStudentRegistration)
                {
                    var room = this.roomsService.GetAll()
                        .FirstOrDefault(r => r.Number == model.StudentInfo.Room.Number);

                    var inventory = new Inventory
                    {
                        Room = room,
                        Type = InventoryType.Personal,
                        LastDateModified = DateTime.Now,
                        RoomId = room?.Id
                    };

                    this.inventoriesService.Add(inventory);

                    user.StudentInfo = new StudentInfo
                    {
                        Address = model.StudentInfo.Address,
                        BirthPlace = model.StudentInfo.BirthPlace,
                        Course = model.StudentInfo.Course,
                        CreatedOn = DateTime.Now,
                        EGN = model.StudentInfo.EGN,
                        FirstName = model.StudentInfo.FirstName,
                        SecondName = model.StudentInfo.SecondName,
                        LastName = model.StudentInfo.LastName,
                        University = model.StudentInfo.University,
                        Years = model.StudentInfo.Years,
                        Nationality = model.StudentInfo.Nationality,
                        Speciality = model.StudentInfo.Speciality,
                        Phone = model.StudentInfo.Phone,
                        PersonalCardNumber = model.StudentInfo.PersonalCardNumber,
                        Inventory = inventory,
                        Room = room
                    };

                    studentsService.Add(user.StudentInfo);
                }

                var result = await this.userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await this.signInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Index", "Home");
                }

                AddErrors(result);

            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }


        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            this.authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }


        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}