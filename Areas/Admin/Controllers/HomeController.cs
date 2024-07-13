﻿using BanGiay.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanGiay.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        CNPMEntities1 database = new CNPMEntities1();
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View(database.SanPhams.ToList());
        }




        //************************************************TẠO SẢN PHẨM***********************************************// 
        [HttpGet]
        public ActionResult Create()
        {
            var categories = database.Categories.ToList();
            ViewBag.CategoryList = new SelectList(categories, "CatID", "NameCate");

            var sizes = database.KichCoes.ToList();
            ViewBag.SizeList = new SelectList(sizes, "maSize", "size");

            var brand = database.ThuongHieuSPs.ToList();
            ViewBag.BrandList = new SelectList(brand, "MaTH", "TenTH");

            return View();
        }
        [HttpPost]
        public ActionResult Create(SanPham sanpham)
        {
            try
            {
                if (sanpham.UpLoadHinhAnh1 != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(sanpham.UpLoadHinhAnh1.FileName);
                    string extension = Path.GetExtension(sanpham.UpLoadHinhAnh1.FileName);
                    fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension;
                    sanpham.HinhAnh1 = fileName;
                    sanpham.UpLoadHinhAnh1.SaveAs(Path.Combine(Server.MapPath("~/Content/Image/SanPham/"), fileName));
                }
                database.SanPhams.Add(sanpham);
                if (sanpham.UpLoadHinhAnh2 != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(sanpham.UpLoadHinhAnh2.FileName);
                    string extension = Path.GetExtension(sanpham.UpLoadHinhAnh2.FileName);
                    fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension;
                    sanpham.HinhAnh2 = fileName;
                    sanpham.UpLoadHinhAnh2.SaveAs(Path.Combine(Server.MapPath("~/Content/Image/SanPham/"), fileName));
                }
                database.SanPhams.Add(sanpham);
                if (sanpham.UpLoadHinhAnh3 != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(sanpham.UpLoadHinhAnh3.FileName);
                    string extension = Path.GetExtension(sanpham.UpLoadHinhAnh3.FileName);
                    fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension;
                    sanpham.HinhAnh3 = fileName;
                    sanpham.UpLoadHinhAnh3.SaveAs(Path.Combine(Server.MapPath("~/Content/Image/SanPham/"), fileName));
                }
                database.SanPhams.Add(sanpham);
                if (sanpham.UpLoadHinhAnh4 != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(sanpham.UpLoadHinhAnh4.FileName);
                    string extension = Path.GetExtension(sanpham.UpLoadHinhAnh4.FileName);
                    fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension;
                    sanpham.HinhAnh4 = fileName;
                    sanpham.UpLoadHinhAnh4.SaveAs(Path.Combine(Server.MapPath("~/Content/Image/SanPham/"), fileName));
                }
                database.SanPhams.Add(sanpham);


                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }




        //************************************************XEM CHI TIẾT SẢN PHẨM***********************************************// 
        public ActionResult Details(int id)
        {
            return View(database.SanPhams.Where(n => n.maSP == id).FirstOrDefault());
        }






        //************************************************SỬA SẢN PHẨM***********************************************//
        [HttpGet]
        public ActionResult Edit(int id){
            var sanpham = database.SanPhams.Where(n => n.maSP == id).FirstOrDefault();

            var categories = database.Categories.ToList();
            ViewBag.CategoryList = new SelectList(categories, "CatID", "NameCate", sanpham.CatID);

            var sizes = database.KichCoes.ToList();
            ViewBag.SizeList = new SelectList(sizes, "maSize", "size", sanpham.maSize);

            var brand = database.ThuongHieuSPs.ToList();
            ViewBag.BrandList = new SelectList(brand, "MaTH", "TenTH", sanpham.MaTH);

            return View(sanpham);
        }

        [HttpPost]
        public ActionResult Edit(int id, SanPham sanpham)
        {
            using (var transaction = database.Database.BeginTransaction())
            {
                try
                {
                    var existingSanPham = database.SanPhams.Find(id);
                    if (existingSanPham == null)
                    {
                        return HttpNotFound();
                    }

                    if (sanpham.UpLoadHinhAnh1 != null)
                    {
                        SaveImage(sanpham.UpLoadHinhAnh1, out string fileName);
                        existingSanPham.HinhAnh1 = fileName;
                    }
                    if (sanpham.UpLoadHinhAnh2 != null)
                    {
                        SaveImage(sanpham.UpLoadHinhAnh2, out string fileName);
                        existingSanPham.HinhAnh2 = fileName;
                    }
                    if (sanpham.UpLoadHinhAnh3 != null)
                    {
                        SaveImage(sanpham.UpLoadHinhAnh3, out string fileName);
                        existingSanPham.HinhAnh3 = fileName;
                    }
                    if (sanpham.UpLoadHinhAnh4 != null)
                    {
                        SaveImage(sanpham.UpLoadHinhAnh4, out string fileName);
                        existingSanPham.HinhAnh4 = fileName;
                    }

                    existingSanPham.tenSP = sanpham.tenSP;
                    existingSanPham.TinhTrangSP = sanpham.TinhTrangSP;
                    existingSanPham.MauSac = sanpham.MauSac;
                    existingSanPham.MoTaSP = sanpham.MoTaSP;
                    existingSanPham.GiaGoc = sanpham.GiaGoc;
                    existingSanPham.GiaGiam = sanpham.GiaGiam;
                    existingSanPham.SLBan = sanpham.SLBan;
                    existingSanPham.MaTH = sanpham.MaTH;
                    existingSanPham.CatID = sanpham.CatID;
                    existingSanPham.maDanhgia = sanpham.maDanhgia;
                    existingSanPham.maSize = sanpham.maSize;
                    existingSanPham.giamGia = sanpham.giamGia;

                    database.Entry(existingSanPham).State = EntityState.Modified;
                    database.SaveChanges();

                    transaction.Commit();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return View(sanpham); 
                }
            }
        }




        //************************************************XÓA SẢN PHẨM***********************************************//
        public ActionResult Delete(int id) {
            return View(database.SanPhams.Where(n => n.maSP == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Delete(int id, SanPham sanpham)
        {
            sanpham = database.SanPhams.Where(n => n.maSP == id).FirstOrDefault();
            database.SanPhams.Remove(sanpham);
            database.SaveChanges();
            return RedirectToAction("Index");
        }










        //************************************************HÀM LƯU HÌNH ẢNH***********************************************//
        private void SaveImage(HttpPostedFileBase file, out string fileName)
        {
            fileName = Path.GetFileNameWithoutExtension(file.FileName);
            string extension = Path.GetExtension(file.FileName);
            fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension;
            string path = Path.Combine(Server.MapPath("~/Content/Image/SanPham/"), fileName);
            file.SaveAs(path);
        }
    }
}


