﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ZoneRadar.Models.ViewModels;
using ZoneRadar.Services;

namespace ZoneRadar.Controllers
{
    public class MemberCenterController : Controller
    {
        private readonly MemberService _service;
        public MemberCenterController()
        {
            _service = new MemberService();
        }
        // GET: MemberCenter
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ForgetPassword()
        {
            return View();
        }
        public ActionResult EmailCheck()
        {
            return View();
        }
        public ActionResult NewPassword()
        {
            return View();
        }
        public ActionResult EditProfile()
        {
            return View();
        }
        public ActionResult MyCollection()
        {
            return View();
        }
        public ActionResult UserInfo()
        {
            return View();
        }
        public ActionResult HostInfo()
        {
            return View();
        }


        [HttpGet]
        public ActionResult Register()
        {
            return RedirectToAction("index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "Name, Email, Password, ConfirmPassword")] RegisterZONERadarViewModel registerVM)
        {
            if (!ModelState.IsValid || registerVM.Password != registerVM.ConfirmPassword)
            {
                return View("Register");
            }
            else
            {
                //先將註冊資訊存進資料庫中
                var registerResult = _service.RegisterMember(registerVM);
                //如果註冊資訊成功儲存
                if (registerResult.IsSuccessful)
                {
                    //接著寄送驗證信
                    _service.SentEmail(Request, Url, registerResult.User.Email);
                    return View("HadSentEmail");
                }
                else
                {
                    //註冊失敗，請重新註冊
                    return View("HadSentEmail");
                }              
            }
        }

        public ActionResult ConfirmEmail(string email)
        {
            //確認是否有此註冊資訊
            var registerResult = _service.ConfirmRegister(email);

            //註冊成功
            if (registerResult.IsSuccessful)
            {
                //讓使用者登入，呈現登入後的畫面
                var encryptedTicket = _service.CreateEncryptedTicket(registerResult.User);
                _service.CreateCookie(encryptedTicket, Response);
                //導回原本的畫面
                return Redirect(_service.GetOriginalUrl(registerResult.User.MemberID.ToString()));
            }
            else
            {
                //註冊失敗，回去註冊畫面，跳出錯誤訊息(還沒寫)
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public ActionResult Login()
        {
            //(每個非授權畫面都要加這行，例：預約頁面)
            TempData["LoginModalPopup"] = true;
            //如果想進入未授權畫面
            if (Request.QueryString["ReturnUrl"] != null)
            {
                //導回原本所在的畫面上，攜帶ReturnUrl參數，並跳出登入Modal
                return Redirect($"{Request.UrlReferrer.AbsolutePath}?ReturnUrl={Request.QueryString["ReturnUrl"]}");
            }
            //(目前專案狀況都不會執行到這一行)
            return Redirect(Request.UrlReferrer.AbsolutePath);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "Email, Password")] LoginZONERadarViewModel loginVM)
        {
            //若未通過Model驗證(前端已先驗證過)
            if (!ModelState.IsValid)
            {
                //回到原本頁面並跳出登入Modal，跳出錯誤訊息(還沒做)
                TempData["LoginModalPopup"] = true;
                Response.Redirect(Request.UrlReferrer.AbsolutePath);
                //return View();
            }

            var user = _service.UserLogin(loginVM);
                       
            //找不到則彈回Login頁
            if (user == null)
            {
                TempData["LoginModalPopup"] = true;
                TempData["Email"] = loginVM.Email;
                //TempData["Email"] = ModelState["LoginZONERadarVM.Email"];
                
                //回到原本頁面並跳出登入Modal
                return Redirect(Request.UrlReferrer.AbsolutePath);
            }

            //建造加密表單驗證票證
            var encryptedTicket = _service.CreateEncryptedTicket(user);

            //建造cookie
            _service.CreateCookie(encryptedTicket, Response);

            //導向使用者原先欲造訪的路由
            var originalUrl = _service.GetOriginalUrl(user.MemberID.ToString());

            return Redirect(originalUrl);
        }

        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}