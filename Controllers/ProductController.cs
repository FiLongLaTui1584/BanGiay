using BanGiay.Context;
using BanGiay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanGiay.Controllers
{
    public class ProductController : Controller
    {
        CNPMEntities objCNPMEntities = new CNPMEntities();
        // GET: Product
        
        public ActionResult Detail(int maSP)
        {
            var objSP = objCNPMEntities.SanPhams.Where(n => n.maSP == maSP).FirstOrDefault();
            var objThuongHieu = objCNPMEntities.ThuongHieuSPs.Find(objSP.MaTH);

            var viewModel = new ViewModel
            {
                SanPham1 = objSP,
                TenThuongHieu = objThuongHieu != null ? objThuongHieu.TenTH : string.Empty
            };

            return View(viewModel);
        }
    }
}