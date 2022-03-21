using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class CafeController : Controller
    {
        CoffeeDataDataContext data = new CoffeeDataDataContext();

        // GET: Cafe

        public ActionResult ListCafe()
        {
            var all_cafe = from cf in data.cafes select cf;
            return View(all_cafe);
        }
   

        public ActionResult DetailCafe(int id)
        {
            var D_cafe = data.cafes.Where(m => m.macafe == id).First();
            return View(D_cafe);
        }

        public ActionResult CreateCafe()
        {

            return View();
        }

        [HttpPost]
        public ActionResult CreateCafe(FormCollection collection, cafe cf)
        {
            var ten = collection["tencafe"];
            var hinh = collection["hinh"];
            var giaban = Convert.ToDecimal(collection["giaban"]);
            var ngaycapnhat = Convert.ToDateTime(collection["ngaycapnhat"]);
            var soluongton = Convert.ToInt32(collection["soluongton"]);
            if (string.IsNullOrEmpty(ten))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                cf.tencafe = ten.ToString();
                cf.hinh = hinh.ToString();
                cf.giaban = giaban;
                cf.soluongton = soluongton;
                data.cafes.InsertOnSubmit(cf);
                data.SubmitChanges();
                return RedirectToAction("ListCafe");
            }
            return this.CreateCafe();
        }

        public ActionResult EditCafe(int id)
        {
            var E_cafe = data.cafes.First(m => m.macafe == id);
            return View(E_cafe);
        }

        [HttpPost]
        public ActionResult EditCafe(int id, FormCollection collection)
        {
            var cafe = data.cafes.First(m => m.macafe == id);
            var E_cafe = collection["tencafe"];
            var E_image = collection["hinh"];
            var E_price = Convert.ToDecimal(collection["giaban"]);
            var E_update = Convert.ToDateTime(collection["ngaycapnhat"]);
            var E_stock = Convert.ToInt32(collection["soluongton"]);
           
            cafe.macafe = id;
            if (string.IsNullOrEmpty(E_cafe))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                cafe.tencafe = E_cafe;
                cafe.hinh = E_image;
                cafe.giaban = E_price;
                cafe.ngaycapnhat = E_update;
                cafe.soluongton = E_stock;
                UpdateModel(cafe);
                data.SubmitChanges();
                return RedirectToAction("ListCafe");
            }
            return this.EditCafe(id);
        }
        public ActionResult DeleteCafe(int id)
        {
            var D_cafe = data.cafes.First(m => m.macafe == id);
            return View(D_cafe);
        }

        [HttpPost]
        public ActionResult DeleteCafe(int id, FormCollection collection)
        {
            var D_cafe = data.cafes.Where(m => m.macafe == id).First();
            data.cafes.DeleteOnSubmit(D_cafe);
            data.SubmitChanges();
            return RedirectToAction("ListCafe");
        }

        public string ProcessUpload(HttpPostedFileBase file)
        {
            if (file == null)
            {
                return "";
            }
            file.SaveAs(Server.MapPath("~/Content/images/" + file.FileName));
            return "/Content/images/" + file.FileName;
        }

        }
}