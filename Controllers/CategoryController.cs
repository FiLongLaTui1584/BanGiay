using BanGiay.Context;
using BanGiay.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BanGiay.Controllers
{
    public class CategoryController : Controller
    {
        CNPM_Entities objCNPMEntities = new CNPM_Entities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductCategoryByBrand(int MaTH)
        {
            var listSP = objCNPMEntities.SanPhams.Where(n => n.MaTH == MaTH).ToList();
            var listTH = objCNPMEntities.ThuongHieuSPs.Where(n => n.MaTH == MaTH).ToList();

            var viewModel = new ViewModel
            {
                SanPham = listSP,
                ThuongHieuSP = listTH
            };

            return View(viewModel);
        }

        public ActionResult ProductCategoryByCate(int CatID)
        {
            var listSP = objCNPMEntities.SanPhams.Where(n => n.CatID == CatID).ToList();
            var listCat = objCNPMEntities.Categories.Where(n => n.CatID == CatID).ToList();

            var viewModel = new ViewModel
            {
                SanPham = listSP,
                Category = listCat
            };

            return View(viewModel);
        }

        public ActionResult ProductCategory(int CatID, int? MaTH)
        {
            var listSP = objCNPMEntities.SanPhams.Where(n => n.CatID == CatID);
            Category category = objCNPMEntities.Categories.FirstOrDefault(c => c.CatID == CatID);
            IEnumerable<ThuongHieuSP> thuongHieuSP = null;

            if (MaTH.HasValue)
            {
                listSP = listSP.Where(n => n.MaTH == MaTH.Value);
                thuongHieuSP = objCNPMEntities.ThuongHieuSPs.Where(th => th.MaTH == MaTH.Value).ToList();
            }
            else
            {
                thuongHieuSP = objCNPMEntities.ThuongHieuSPs.ToList();
            }

            var viewModel = new ViewModel
            {
                SanPham = listSP.ToList(),
                ThuongHieuSP = thuongHieuSP,
                Category = objCNPMEntities.Categories.ToList(),
                Category1 = category
            };

            return View(viewModel);
        }
    }
}
