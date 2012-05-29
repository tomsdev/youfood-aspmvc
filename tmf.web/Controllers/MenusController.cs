using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tmf.business.repositories;
using tmf.entities;
using tmf.web.Models;

namespace tmf.web.Controllers
{   
    public class MenusController : Controller
    {
		private readonly IProductTypeRepository producttypeRepository;
		private readonly IMenuRepository menuRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public MenusController() : this(new ProductTypeRepository(), new MenuRepository())
        {
        }

        public MenusController(IProductTypeRepository producttypeRepository, IMenuRepository menuRepository)
        {
			this.producttypeRepository = producttypeRepository;
			this.menuRepository = menuRepository;
        }

        //
        // GET: /Menus/

        public ViewResult Index()
        {
            return View(menuRepository.AllIncluding(menu => menu.ProductType));
        }

        //
        // GET: /Menus/Details/5

        public ViewResult Details(System.Guid id)
        {
            return View(menuRepository.Find(id));
        }

        //
        // GET: /Menus/Create

        public ActionResult Create()
        {
			ViewBag.PossibleProductTypes = producttypeRepository.All;
            return View();
        } 

        //
        // POST: /Menus/Create

        [HttpPost]
        public ActionResult Create(Menu menu)
        {
            if (ModelState.IsValid) {
                menuRepository.InsertOrUpdate(menu);
                menuRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleProductTypes = producttypeRepository.All;
				return View();
			}
        }
        
        //
        // GET: /Menus/Edit/5
 
        public ActionResult Edit(System.Guid id)
        {
			ViewBag.PossibleProductTypes = producttypeRepository.All;
             return View(menuRepository.Find(id));
        }

        //
        // POST: /Menus/Edit/5

        [HttpPost]
        public ActionResult Edit(Menu menu)
        {
            if (ModelState.IsValid) {
                menuRepository.InsertOrUpdate(menu);
                menuRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleProductTypes = producttypeRepository.All;
				return View();
			}
        }

        //
        // GET: /Menus/Delete/5
 
        public ActionResult Delete(System.Guid id)
        {
            return View(menuRepository.Find(id));
        }

        //
        // POST: /Menus/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(System.Guid id)
        {
            menuRepository.Delete(id);
            menuRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                producttypeRepository.Dispose();
                menuRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

