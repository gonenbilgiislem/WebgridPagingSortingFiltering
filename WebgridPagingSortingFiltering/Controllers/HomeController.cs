using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic;
using WebgridPagingSortingFiltering.Models;

namespace WebgridPagingSortingFiltering.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index(int page =1, string sort = "Adi", string sortdir="asc", string search="")
        {
            int pageSize = 10;
            if (page < 1)
            {
                page = 1;
            }

            int skip = (page * pageSize) - pageSize;
            var data = List(search, sort, sortdir, skip, pageSize, out int totalRecord);
            ViewBag.TotalRows = totalRecord;
            ViewBag.search = search;
            return View(data);
        }

        public List<Kisiler> List(string search, string sort, string sortdir, int skip, int pageSize, out int totalRecord)
            
        {
            using ( EtiketAppEntities dc = new EtiketAppEntities())
            {
                var v = (from a in dc.Kisiler
                         where
                                 a.Adi.Contains(search) ||
                                 a.FirmaUnvani.Contains(search)||
                                 a.Unvani.Contains(search)||
                                 a.Adres.Contains(search) 
                                 select a
                             );
                totalRecord = v.Count();
                v = v.OrderBy(sort + " " + sortdir);
                if (pageSize > 0)
                {
                    v = v.Skip(skip).Take(pageSize);
                }
                return v.ToList();
            }
        }
	}
}