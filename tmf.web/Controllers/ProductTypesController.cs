using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tmf.entities;
using tmf.web.Models;
using tmf.business.repositories;

namespace tmf.web.Controllers
{   
    public class ProductTypesController : Controller
    {
		private readonly IProductTypeRepository producttypeRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public ProductTypesController() : this(new ProductTypeRepository())
        {
        }

        public ProductTypesController(IProductTypeRepository producttypeRepository)
        {
			this.producttypeRepository = producttypeRepository;
        }

        //
        // GET: /ProductTypes/

        public ViewResult Index()
        {
            return View(producttypeRepository.AllIncluding(producttype => producttype.Menus));
        }

        //
        // GET: /ProductTypes/Details/5

        public ViewResult Details(System.Guid id)
        {
            return View(producttypeRepository.Find(id));
        }

        //
        // GET: /ProductTypes/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /ProductTypes/Create

        [HttpPost]
        public ActionResult Create(ProductType producttype)
        {
            if (ModelState.IsValid) {
                producttypeRepository.InsertOrUpdate(producttype);
                producttypeRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }
        
        //
        // GET: /ProductTypes/Edit/5
 
        public ActionResult Edit(System.Guid id)
        {
             return View(producttypeRepository.Find(id));
        }

        //
        // POST: /ProductTypes/Edit/5

        [HttpPost]
        public ActionResult Edit(ProductType producttype)
        {
            if (ModelState.IsValid) {
                producttypeRepository.InsertOrUpdate(producttype);
                producttypeRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }

        //
        // GET: /ProductTypes/Delete/5
 
        public ActionResult Delete(System.Guid id)
        {
            return View(producttypeRepository.Find(id));
        }

        //
        // POST: /ProductTypes/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(System.Guid id)
        {
            producttypeRepository.Delete(id);
            producttypeRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                producttypeRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

