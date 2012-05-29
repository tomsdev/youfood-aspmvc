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
    public class WaitersController : Controller
    {
		private readonly IWaiterRepository waiterRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public WaitersController() : this(new WaiterRepository())
        {
        }

        public WaitersController(IWaiterRepository waiterRepository)
        {
			this.waiterRepository = waiterRepository;
        }

        //
        // GET: /Waiters/

        public ViewResult Index()
        {
            return View(waiterRepository.AllIncluding(waiter => waiter.Zones, waiter => waiter.Orders));
        }

        //
        // GET: /Waiters/Details/5

        public ViewResult Details(System.Guid id)
        {
            return View(waiterRepository.Find(id));
        }

        //
        // GET: /Waiters/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Waiters/Create

        [HttpPost]
        public ActionResult Create(Waiter waiter)
        {
            if (ModelState.IsValid) {
                waiterRepository.InsertOrUpdate(waiter);
                waiterRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }
        
        //
        // GET: /Waiters/Edit/5
 
        public ActionResult Edit(System.Guid id)
        {
             return View(waiterRepository.Find(id));
        }

        //
        // POST: /Waiters/Edit/5

        [HttpPost]
        public ActionResult Edit(Waiter waiter)
        {
            if (ModelState.IsValid) {
                waiterRepository.InsertOrUpdate(waiter);
                waiterRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }

        //
        // GET: /Waiters/Delete/5
 
        public ActionResult Delete(System.Guid id)
        {
            return View(waiterRepository.Find(id));
        }

        //
        // POST: /Waiters/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(System.Guid id)
        {
            waiterRepository.Delete(id);
            waiterRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                waiterRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

