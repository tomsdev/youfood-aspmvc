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
    public class ZonesController : Controller
    {
		private readonly IWaiterRepository waiterRepository;
		private readonly IZoneRepository zoneRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public ZonesController() : this(new WaiterRepository(), new ZoneRepository())
        {
        }

        public ZonesController(IWaiterRepository waiterRepository, IZoneRepository zoneRepository)
        {
			this.waiterRepository = waiterRepository;
			this.zoneRepository = zoneRepository;
        }

        //
        // GET: /Zones/

        public ViewResult Index()
        {
            return View(zoneRepository.AllIncluding(zone => zone.Waiter, zone => zone.Orders));
        }

        //
        // GET: /Zones/Details/5

        public ViewResult Details(System.Guid id)
        {
            return View(zoneRepository.Find(id));
        }

        //
        // GET: /Zones/Create

        public ActionResult Create()
        {
			ViewBag.PossibleWaiters = waiterRepository.All;
            return View();
        } 

        //
        // POST: /Zones/Create

        [HttpPost]
        public ActionResult Create(Zone zone)
        {
            if (ModelState.IsValid) {
                zoneRepository.InsertOrUpdate(zone);
                zoneRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleWaiters = waiterRepository.All;
				return View();
			}
        }
        
        //
        // GET: /Zones/Edit/5
 
        public ActionResult Edit(System.Guid id)
        {
			ViewBag.PossibleWaiters = waiterRepository.All;
             return View(zoneRepository.Find(id));
        }

        //
        // POST: /Zones/Edit/5

        [HttpPost]
        public ActionResult Edit(Zone zone)
        {
            if (ModelState.IsValid) {
                zoneRepository.InsertOrUpdate(zone);
                zoneRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleWaiters = waiterRepository.All;
				return View();
			}
        }

        //
        // GET: /Zones/Delete/5
 
        public ActionResult Delete(System.Guid id)
        {
            return View(zoneRepository.Find(id));
        }

        //
        // POST: /Zones/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(System.Guid id)
        {
            zoneRepository.Delete(id);
            zoneRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                waiterRepository.Dispose();
                zoneRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

