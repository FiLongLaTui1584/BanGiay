using BanGiay.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace BanGiay.Areas.Admin.Controllers
{
    public class OrderManagementController : Controller
    {
        private CNPMEntities2 database = new CNPMEntities2();

        public ActionResult Index()
        {
            var orders = database.HoaDons
                                 .Include(o => o.ChiTietHoaDons.Select(c => c.SanPham)) // Tải thông tin sản phẩm
                                 .Include(o => o.User) // Tải thông tin người dùng
                                 .ToList();
            return View(orders);
        }




        public ActionResult ConfirmOrder(int id)
        {
            var order = database.HoaDons.SingleOrDefault(o => o.maHD == id);
            if (order != null)
            {
                order.TrangThai = "Đã xác nhận";
                database.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult RejectOrder(int id)
        {
            var order = database.HoaDons.SingleOrDefault(o => o.maHD == id);
            if (order != null)
            {
                order.TrangThai = "Đã từ chối";
                database.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }

}