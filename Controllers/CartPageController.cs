﻿using BanGiay.Context;
using BanGiay.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace BanGiay.Controllers
{
    public class CartPageController : Controller
    {
        private CNPM_Entities database = new CNPM_Entities();




        private Cart GetCart()
        {
            Cart cart = Session["Cart"] as Cart;
            if (cart == null || Session["Cart"] == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }

        private bool IsUserLoggedIn()
        {
            return Session["UserName"] != null;
        }

        private User GetLoggedInUser()
        {
            var username = Session["UserName"] as string;
            if (username != null)
            {
                return database.Users.FirstOrDefault(u => u.TenTK == username);
            }
            return null;
        }


        public ActionResult AddToCart(int id, int quantity)
        {
            var product = database.SanPhams.SingleOrDefault(s => s.maSP == id);
            if (product != null)
            {
                if (product.SLTon >= quantity)
                {
                    GetCart().AddProductToCart(product, quantity);
                }
                else
                {
                    TempData["Error"] = "Số lượng tồn kho không đủ.";
                }
            }
            return RedirectToAction("CartPage");
        }

        [HttpPost]
        public ActionResult UpdateCartQuantity(FormCollection form)
        {
            Cart cart = Session["Cart"] as Cart;
            int idPro = int.Parse(form["idPro"]);
            int quantity = int.Parse(form["carQuantity"]);
            cart.UpdateQuantity(idPro, quantity);
            return RedirectToAction("CartPage");
        }

        public ActionResult RemoveCart(int id)
        {
            Cart cart = Session["Cart"] as Cart;
            if (cart != null)
            {
                cart.RemoveCartItem(id);
            }
            return RedirectToAction("CartPage");
        }

        public ActionResult CartPage()
        {
            if (Session["Cart"] == null)
            {
                return View("EmptyCart");
            }

            if (!IsUserLoggedIn())
            {
                return RedirectToAction("Login", "Account");
            }

            var user = GetLoggedInUser();
            if (user != null)
            {
                ViewBag.UserName = user.TenTK;
                ViewBag.UserEmail = user.Email;
                ViewBag.UserAddress = user.diaChi;
                ViewBag.UserPhone = user.SDT; // Thay đổi tùy theo thuộc tính của user
            }

            Cart cart = Session["Cart"] as Cart;
            return View(cart);
        }
        public PartialViewResult BagCart()
        {
            decimal total_money_item = 0;
            Cart cart = Session["Cart"] as Cart;
            if (cart != null)
                total_money_item = cart.TotalMoney();
            ViewBag.TotalCart = total_money_item;
            return PartialView("BagCart");
        }
        public PartialViewResult CartQuan()
        {
            decimal total_cart_item = 0;
            Cart cart = Session["Cart"] as Cart;
            if (cart != null)
                total_cart_item = cart.TotalQuantity();
            ViewBag.TotalCartQuan = total_cart_item;
            return PartialView("CartQuan");
        }

        [HttpPost]
        public ActionResult ReceiveValueWithSession(string inputValue)
        {
            Session["InputValue"] = inputValue;
            return RedirectToAction("DisplayValueFromSession");
        }

        public ActionResult DisplayValueFromSession()
        {
            ViewBag.InputValue = Session["InputValue"];
            return View();
        }















        [HttpPost]
        public ActionResult ThanhToan(FormCollection form)
        {
            if (!IsUserLoggedIn())
            {
                return RedirectToAction("Login", "Account");
            }

            try
            {
                Cart cart = Session["Cart"] as Cart;
                var user = GetLoggedInUser();

                HoaDon _order = new HoaDon
                {
                    NgayDat = DateTime.Now,
                    diaChi = form["AddressDeliverry"],
                    maUser = user.maUser,
                    TrangThai = "Chờ xác nhận"
                };

                database.HoaDons.Add(_order);
                database.SaveChanges();

                foreach (var item in cart.Items)
                {
                    ChiTietHoaDon _order_detail = new ChiTietHoaDon
                    {
                        maHD = _order.maHD,
                        maSP = item.Product.maSP,
                        GiaTien = (double)item.Product.GiaGiam,
                        SoLuong = item.Quantity
                    };
                    database.ChiTietHoaDons.Add(_order_detail);

                    var product = database.SanPhams.SingleOrDefault(p => p.maSP == item.Product.maSP);
                    if (product != null)
                    {
                        product.SLBan += item.Quantity;
                        product.SLTon -= item.Quantity;

                        if (product.SLTon == 0)
                        {
                            product.TinhTrangSP = "Hết hàng";
                        }
                    }
                }

                database.SaveChanges();
                cart.ClearCart();

                return RedirectToAction("CheckOut_Success", "CartPage");
            }
            catch
            {
                return Content("Có sai sót! Xin kiểm tra lại thông tin");
            }
        }





        public ActionResult CheckOut_Success()
        {
            return View();
        }






        public ActionResult OrderStatus()
        {
            var user = GetLoggedInUser();
            var orders = database.HoaDons
                                 .Where(o => o.maUser == user.maUser)
                                 .Include(o => o.ChiTietHoaDons.Select(c => c.SanPham))
                                 .ToList();
            return View(orders);
        }


        public ActionResult ConfirmReceipt(int id)
        {
            var order = database.HoaDons.SingleOrDefault(o => o.maHD == id);
            if (order != null && order.TrangThai == "Đã xác nhận, đang giao đơn hàng")
            {
                order.TrangThai = "Đã giao";
                database.SaveChanges();
            }
            return RedirectToAction("OrderStatus");
        }

        public ActionResult CancelOrder(int id)
        {
            var order = database.HoaDons.SingleOrDefault(o => o.maHD == id);
            if (order != null && order.TrangThai == "Đã xác nhận, đang giao đơn hàng")
            {
                order.TrangThai = "Đã hủy đơn";
                database.SaveChanges();
            }
            return RedirectToAction("OrderStatus");
        }



    }
}
