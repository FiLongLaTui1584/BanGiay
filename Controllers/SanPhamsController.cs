using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BanGiay.Models;

namespace BanGiay.Controllers
{
    public class SanPhamsController : Controller
    {
        private CNPM_LTEntities1 db = new CNPM_LTEntities1();

        public ActionResult IndexPV() { return View(); }
        // GET: SanPhams
        public ActionResult Index()
        {
            var sanPhams = db.SanPhams.Include(s => s.Category).Include(s => s.DanhGia).Include(s => s.KichCo).Include(s => s.ThuongHieuSP);
            return View(sanPhams.ToList());
        }

        // GET: SanPhams/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // GET: SanPhams/Create
        public ActionResult Create()
        {
            ViewBag.CatID = new SelectList(db.Categories, "CatID", "NameCate");
            ViewBag.maDanhgia = new SelectList(db.DanhGias, "maDanhgia", "comment");
            ViewBag.maSize = new SelectList(db.KichCoes, "maSize", "maSize");
            ViewBag.MaTH = new SelectList(db.ThuongHieuSPs, "MaTH", "TenTH");
            return View();
        }

        // POST: SanPhams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "maSP,TinhTrangSP,MauSac,HinhAnh1,MoTaSP,GiaGoc,SLBan,HinhAnh2,HinhAnh3,HinhAnh4,GiaGiam,MaTH,CatID,maDanhgia,maSize")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                db.SanPhams.Add(sanPham);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CatID = new SelectList(db.Categories, "CatID", "NameCate", sanPham.CatID);
            ViewBag.maDanhgia = new SelectList(db.DanhGias, "maDanhgia", "comment", sanPham.maDanhgia);
            ViewBag.maSize = new SelectList(db.KichCoes, "maSize", "maSize", sanPham.maSize);
            ViewBag.MaTH = new SelectList(db.ThuongHieuSPs, "MaTH", "TenTH", sanPham.MaTH);
            return View(sanPham);
        }

        // GET: SanPhams/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            ViewBag.CatID = new SelectList(db.Categories, "CatID", "NameCate", sanPham.CatID);
            ViewBag.maDanhgia = new SelectList(db.DanhGias, "maDanhgia", "comment", sanPham.maDanhgia);
            ViewBag.maSize = new SelectList(db.KichCoes, "maSize", "maSize", sanPham.maSize);
            ViewBag.MaTH = new SelectList(db.ThuongHieuSPs, "MaTH", "TenTH", sanPham.MaTH);
            return View(sanPham);
        }

        // POST: SanPhams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "maSP,TinhTrangSP,MauSac,HinhAnh1,MoTaSP,GiaGoc,SLBan,HinhAnh2,HinhAnh3,HinhAnh4,GiaGiam,MaTH,CatID,maDanhgia,maSize")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CatID = new SelectList(db.Categories, "CatID", "NameCate", sanPham.CatID);
            ViewBag.maDanhgia = new SelectList(db.DanhGias, "maDanhgia", "comment", sanPham.maDanhgia);
            ViewBag.maSize = new SelectList(db.KichCoes, "maSize", "maSize", sanPham.maSize);
            ViewBag.MaTH = new SelectList(db.ThuongHieuSPs, "MaTH", "TenTH", sanPham.MaTH);
            return View(sanPham);
        }

        // GET: SanPhams/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // POST: SanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            SanPham sanPham = db.SanPhams.Find(id);
            db.SanPhams.Remove(sanPham);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
