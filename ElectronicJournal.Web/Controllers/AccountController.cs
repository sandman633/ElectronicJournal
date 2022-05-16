using ElectronicJournal.Web.Models;
using ElectronicJournal.Web.Repositories.Interfaces;
using ElectronicJournal.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ElectronicJournal.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUserGroupRepository _userGroupRepository;
        private readonly IGroupRepository _groupRepository;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager,IUserGroupRepository userGroupRepository,IGroupRepository groupRepository )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userGroupRepository = userGroupRepository;
            _groupRepository = groupRepository;
        }
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                var result =
                    await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
                if (result.Succeeded)
                {
                    
                    var claims = await GetUserClaimsAsync(user);
                    

                    await _signInManager.SignInWithClaimsAsync(user, model.RememberMe, claims);
                    // проверяем, принадлежит ли URL приложению
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }

                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }
            return View(model);
        }

        private async Task<List<Claim>> GetUserClaimsAsync(User user)
        {
            List<Claim> claims = new List<Claim>();

            var usergroups = await _userGroupRepository.GetByCriteriaAsync(e => e.UserId == user.Id);

            foreach (var usergroup in usergroups)
            {
                claims.Add(new Claim("Group", usergroup.Group.Name));
                claims.Add(new Claim("Group", usergroup.Group.Name + "1"));
            }
            claims.Add(new Claim("Id", user.Id.ToString()));
            return claims;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // удаляем аутентификационные куки
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
