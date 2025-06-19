using BiblioPortCartier.Data;
using BiblioPortCartier.Helpers;
using BiblioPortCartier.Models;
using BiblioPortCartier.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.Collections;

namespace BiblioPortCartier.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly AppDbContext _db;
        private Object[] provinces = new Object[] {
                new{Id=0, Name="--Sélectionnez une province--"},
                new{Id=1, Name="AB"},
                new{Id=2, Name="BC"},
                new{Id=3, Name="MB"},
                new{Id=4, Name="NB"},
                new{Id=5, Name="NL"},
                new{Id=6, Name="NT"},
                new{Id=7, Name="NS"},
                new{Id=8, Name="NU"},
                new{Id=9, Name="ON"},
                new{Id=10, Name="PE"},
                new{Id=11, Name="QC"},
                new{Id=12, Name="SK"},
                new{Id=13, Name="YT"}
            };

        private Object[] sexes = new Object[] {
                new{Id=0, Name="--Sélectionnez un sexe--"},
                new{Id=1, Name="Homme"},
                new{Id=2, Name="Femme"}
            };

        public AccountController(SignInManager<User> signInManager, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, AppDbContext db)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.roleManager = roleManager;
            _db = db;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Login(LogInViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                
                if (await userManager.IsInRoleAsync(user, "Member"))
                {
                    var member = UsersHelper.Cast(user, typeof(Member));
                    string memberString = JsonConvert.SerializeObject(member);
                    HttpContext.Session.SetString("currentUser", memberString);
                }
                else
                {
                    var employee = UsersHelper.Cast(user, typeof(Employee));
                    string employeeString = JsonConvert.SerializeObject(employee);
                    HttpContext.Session.SetString("currentUser", employeeString);
                }

                return RedirectToAction("Home", "Library");
            }

            ModelState.AddModelError(string.Empty, "Invalid Login Attempt.");
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            CreateViewBags();

            ViewBag.UserCode = UsersHelper.GenerateUserCode();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Register(CreateMemberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                CreateViewBags();
                return View(model);
            }

            object objProvince = provinces.ElementAt(Int32.Parse(model.Province));
            object objSexe = sexes.ElementAt(Int32.Parse(model.Gender));

            var province = objProvince.GetType().GetProperty("Name").GetValue(objProvince).ToString();
            var sexe = objSexe.GetType().GetProperty("Name").GetValue(objSexe).ToString();

            var address = Address.CreateAddress(model, province);
            var userCode = UsersHelper.CreateUserCode(model.UserCode, "Member");

            var member = new Member
            {
                Id = userCode,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Gender = sexe,
                PhoneNumber = model.PhoneNumber,
                Address = address,
                BirthdayDate = model.BirthdayDate,
                RegisterDate = DateTime.Now,
                UserName = model.Email,
                NormalizedUserName = model.Email.ToUpper(),
                Email = model.Email,
                NormalizedEmail = model.Email.ToUpper()
            };

            var result = await userManager.CreateAsync(member, model.Password);

            if (result.Succeeded)
            {
                var roleExist = await roleManager.RoleExistsAsync("Member");

                if (!roleExist)
                {
                    var role = new IdentityRole("Member");
                    await roleManager.CreateAsync(role);
                }

                await userManager.AddToRoleAsync(member, "Member");

                await signInManager.SignInAsync(member, isPersistent: false);
                return RedirectToAction("Login", "Account");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            CreateViewBags();
            return View(model);
        }

        [HttpGet]
        public IActionResult VerifyEmail()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> VerifyEmail(VerifyEmailViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                ModelState.AddModelError("", "User not found!");
                return View(model);
            }
            else
            {
                return RedirectToAction("ChangePassword", "Account", new { email = user.Email });
            }
        }

        [HttpGet]
        public IActionResult ChangePassword(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("VerifyEmail", "Account");
            }

            return View(new ChangePasswordViewModel { Email = email });
        }

        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Something went wrong");
                return View(model);
            }

            var user = await userManager.FindByEmailAsync(model.Email);

            if(user == null)
            {
                ModelState.AddModelError("", "User not found!");
                return View(model);
            }

            var result = await userManager.RemovePasswordAsync(user);
            if (result.Succeeded)
            {
                result = await userManager.AddPasswordAsync(user,model.NewPassword);
                return RedirectToAction("Login", "Account");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        private void CreateViewBags()
        {
            ViewBag.Provinces = new SelectList(provinces, "Id", "Name");
            ViewBag.Sexes = new SelectList(sexes, "Id", "Name");
        }
    }
}
