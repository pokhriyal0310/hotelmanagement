using DAL.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HotelManagementSystem.Controllers
{
        public class SecurityController : Controller
        {
            public IActionResult Login(string ReturnUrl)
            {
                ViewBag.ur = ReturnUrl;
                return View();
            }
            [HttpPost]
            public IActionResult Login(UserModel usr, string ReturnUrl)
            {
                ViewBag.ur = ReturnUrl;
                if (usr.UserName == "hotel" && usr.Password == "123456")
                {
                    // to store user information(Claims)
                    List<Claim> uinfo = new List<Claim>();
                    uinfo.Add(new Claim(ClaimTypes.NameIdentifier, usr.UserName));
                uinfo.Add(new Claim(ClaimTypes.Name, usr.UserName));
                uinfo.Add(new Claim(ClaimTypes.Role, usr.Role));
                    // to add identity of user          
                ClaimsIdentity iden = new ClaimsIdentity(uinfo, CookieAuthenticationDefaults.AuthenticationScheme);                 ClaimsPrincipal userDetail = new ClaimsPrincipal(iden);
                    HttpContext.SignInAsync(userDetail);
                    if (ReturnUrl != null)
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                return View();
            }
            public IActionResult LogOut(string ReturnUrl)
            {
                ViewBag.ur = ReturnUrl;
                HttpContext.SignOutAsync();
                return RedirectToAction("Login", "Security");
            }
            public IActionResult AccessDenied(string ReturnUrl)
            {
                ViewBag.ur = ReturnUrl;
                return View();
            }
        }


    }

