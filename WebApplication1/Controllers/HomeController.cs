using System;
using System.Collections.Generic;
using System.Linq;
using PagedList;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        CoffeeDataDataContext data = new CoffeeDataDataContext();

        public ActionResult Index(int ? page)
        {
            if (page == null) page = 1;
            var all_cafe =(from cf in data.cafes select cf).OrderBy(m=>m.macafe);
            int pagedSize = 6;
            int pageNum = page ?? 1;

            return View(all_cafe.ToPagedList(pageNum,pagedSize));
        }
    }
}