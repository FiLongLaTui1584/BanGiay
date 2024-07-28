using BanGiay.Context;
using BanGiay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace BanGiay.Controllers
{
    public class AccountController : Controller
    {
        CNPMEntities5 objCNPMEntities = new CNPMEntities5();
        // GET: Account
        //public ActionResult Index()
        //{
        //    return View();
        //}




        //************************************************ĐĂNG KÝ***********************************************// 
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra xem SDT và Email đã tồn tại trong database chưa
                bool isExistSDT = objCNPMEntities.Users.Any(u => u.SDT == model.SDT);
                bool isExistEmail = objCNPMEntities.Users.Any(u => u.Email == model.Email);

                if (isExistSDT)
                {
                    return Json(new { success = false, message = "Số điện thoại đã tồn tại trong hệ thống." });
                }

                if (isExistEmail)
                {
                    return Json(new { success = false, message = "Email đã tồn tại trong hệ thống." });
                }

                var user = new User
                {
                    TenTK = model.TenTK,
                    SDT = model.SDT,
                    diaChi = model.diaChi,
                    ngaySinh = model.ngaySinh,
                    Email = model.Email,
                    Pass = model.Pass,
                    isAdmin = false
                };

                objCNPMEntities.Users.Add(user);
                objCNPMEntities.SaveChanges();

                return Json(new { success = true, message = "Đăng ký thành công. Chuyển đến trang đăng nhập." });
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
            return Json(new { success = false, message = string.Join("  -  ", errors) });
        }








        //************************************************EDIT THÔNG TIN***********************************************// 
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var user = objCNPMEntities.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            var model = new EditUserViewModel
            {
                maUser = user.maUser,
                TenTK = user.TenTK,
                SDT = user.SDT,
                diaChi = user.diaChi,
                ngaySinh = user.ngaySinh,
                Email = user.Email,
                Pass =user.Pass
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = objCNPMEntities.Users.Find(model.maUser);
                if (user == null)
                {
                    return Json(new { success = false, message = "Không tìm thấy người dùng." });
                }

                user.TenTK = model.TenTK;
                user.SDT = model.SDT;
                user.diaChi = model.diaChi;
                user.ngaySinh = model.ngaySinh;
                user.Email = model.Email;
                user.Pass = model.Pass;

                objCNPMEntities.SaveChanges();

                return Json(new { success = true, message = "Cập nhật thông tin thành công! Chuyển về trang chủ ..." });
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
            return Json(new { success = false, message = string.Join("  -  ", errors) });
        }











        //************************************************ĐĂNG NHẬP***********************************************// 
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = objCNPMEntities.Users.FirstOrDefault(u => u.Email == model.Email);

                if (user == null)
                {
                    // Thông báo lỗi nếu tài khoản không tồn tại
                    ModelState.AddModelError("", "Tài khoản không tồn tại");
                }
                else if (user.Pass != model.Pass)
                {
                    // Thông báo lỗi nếu mật khẩu sai
                    ModelState.AddModelError("", "Email hoặc password nhập sai");
                }
                else
                {
                    // Đăng nhập thành công
                    Session["UserName"] = user.TenTK;
                    Session["IsAdmin"] = user.isAdmin;
                    Session["UserID"] = user.maUser;

                    return Json(new { success = true, message = "Đăng nhập thành công, Chuyển đến trang chủ", redirectUrl = Url.Action("Index", "Home") });
                }
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
            return Json(new { success = false, message = string.Join("  -  ", errors) });
        }







        //************************************************ĐĂNG XUẤT***********************************************// 
        [HttpGet]
        public ActionResult Logout()
        {
            TempData["ConfirmMessage"] = "Bạn muốn đăng xuất?";
            Session["UserName"] = null;
            return RedirectToAction("Index","Home");
        }








        //************************************************RESET PASS***********************************************// 
        public ActionResult ForgotPassword()
        {
            return View();
        }

        // Xử lý việc gửi mã OTP
        [HttpPost]
        public ActionResult ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool isEmail = new EmailAddressAttribute().IsValid(model.ContactInfo);
                bool isPhoneNumber = Regex.IsMatch(model.ContactInfo, @"^[0-9]{10}$");

                if (!isEmail && !isPhoneNumber)
                {
                    return Json(new { success = false, message = "Định dạng email hoặc số điện thoại không hợp lệ." });
                }
                else
                {
                    var user = objCNPMEntities.Users.FirstOrDefault(u => u.Email == model.ContactInfo || u.SDT == model.ContactInfo);
                    if (user != null)
                    {
                        // Gửi mã OTP qua email hoặc SMS
                        string otp = GenerateOtp();
                        Session["Otp"] = otp;
                        Session["ContactInfo"] = model.ContactInfo; 

                        return Json(new { success = true, message = "OTP đã được gửi. Chuyển đến trang xác nhận OTP." });
                    }
                    else
                    {
                        return Json(new { success = false, message = "Số điện thoại hoặc email không tồn tại." });
                    }
                }
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
            return Json(new { success = false, message = string.Join(" - ", errors) });
        }





        public ActionResult VerifyOtp()
        {
            return View();
        }

        // Xử lý việc xác nhận mã OTP
        [HttpPost]
        public ActionResult VerifyOtp(VerifyOtpViewModel model)
        {
            string otp = Session["Otp"] as string;

            if (model.Otp == otp)
            {
                return RedirectToAction("ResetPassword");
            }
            else
            {
                ModelState.AddModelError("", "Mã OTP không đúng.");
                return View();
            }
        }

        public ActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.NewPassword == model.ConfirmPassword)
                {
                    string contactInfo = Session["ContactInfo"] as string;
                    var user = objCNPMEntities.Users.FirstOrDefault(u => u.Email == contactInfo || u.SDT == contactInfo);
                    if (user != null)
                    {
                        user.Pass = model.NewPassword; 
                        objCNPMEntities.SaveChanges(); 


                        Session.Remove("Otp");
                        Session.Remove("ContactInfo");

                        return RedirectToAction("Login");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Người dùng không tồn tại.");
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Mật khẩu và xác nhận mật khẩu không khớp.");
                    return View();
                }
            }

            return View(model);
        }


        private string GenerateOtp()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString();
        }
    }
}