using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BanGiay.Context;
using BanGiay.Models;


namespace BanGiay.Controllers
{
    public class HomeController : Controller
    {
        CNPMEntities1 objCNPMEntities = new CNPMEntities1();  
        public ActionResult Index()
        {
            var listSP = objCNPMEntities.SanPhams.ToList();
            var listCat = objCNPMEntities.Categories.ToList();
            var listTH  = objCNPMEntities.ThuongHieuSPs.ToList();
            

            var viewModel = new ViewModel
            {
                SanPham = listSP,
                Category = listCat,
                ThuongHieuSP = listTH
            };


            return View(viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }








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
                var user = objCNPMEntities.Users.FirstOrDefault(u => u.Email == model.Email && u.Pass == model.Pass);

                if (user != null)
                {
                    Session["UserName"] = user.TenTK;
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Thông tin đăng nhập không hợp lệ");
                }
            }

            return View(model);
        }





        //************************************************ĐĂNG XUẤT***********************************************// 
        [HttpGet]
        public ActionResult Logout()
        {
            // Hiển thị alert khi đăng xuất
            TempData["ConfirmMessage"] = "Bạn muốn đăng xuất?";
            Session["UserName"] = null;
            return RedirectToAction("Index");
        }
    }
}