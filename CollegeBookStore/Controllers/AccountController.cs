//https://www.youtube.com/watch?v=TfarnVqnhX0

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using CollegeBookStore.Models;
using Microsoft.AspNetCore.Authorization;

namespace CollegeBookStore.Controllers
{
    public class AccountController : Controller
    {
        public UserManager<IdentityUser>    userManager   { get; }
        public SignInManager<IdentityUser>  signInManager { get; }


        public AccountController(UserManager<IdentityUser> UserManager, SignInManager<IdentityUser> SignInManager)
        {
            userManager = UserManager;
            signInManager = SignInManager;
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {

            //Once the user POSTS their details from the registration page, grab the values from the 
            //  RegisterViewModel, and check if they're valid
            if (ModelState.IsValid)
            {
                //Create the new user, using their provided username, email, and password
                var user = new IdentityUser { UserName = model.Email, Email = model.Email };

                //Register the user in the DB
                var result =  await userManager.CreateAsync(user, model.Password);

                //If the DB registration worked, sign them in, and redirect to their home page
                if(result.Succeeded)
                {
                    if (signInManager.IsSignedIn(User) && User.IsInRole("Admin"))
                    {
                        return RedirectToAction("ListUsers", "Administration");
                    }

                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("index", "home");
                }
                else
                {

                    //Loop through the errors correction, and return the casue to the RegisterViewModel
                    //  The web page will display the error in the <div asp-validation-summary helper /> line of the view
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(String.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }


    }//class
}//namespace