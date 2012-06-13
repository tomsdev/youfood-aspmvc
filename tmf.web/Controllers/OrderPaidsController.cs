using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tmf.entities;
using tmf.web.Models;

namespace tmf.web.Controllers
{   
    public class OrderPaidsController : Controller
    {
		private readonly IWaiterRepository waiterRepository;
		private readonly IRestaurantRepository restaurantRepository;
		private readonly IOrderPaidRepository orderpaidRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public OrderPaidsController() : this(new WaiterRepository(), new RestaurantRepository(), new OrderPaidRepository())
        {
        }

        public OrderPaidsController(IWaiterRepository waiterRepository, IRestaurantRepository restaurantRepository, IOrderPaidRepository orderpaidRepository)
        {
			this.waiterRepository = waiterRepository;
			this.restaurantRepository = restaurantRepository;
			this.orderpaidRepository = orderpaidRepository;
        }

        //
        // GET: /OrderPaids/

        public ViewResult Index()
        {
            return View(orderpaidRepository.AllIncluding(orderpaid => orderpaid.Waiter, orderpaid => orderpaid.Restaurant, orderpaid => orderpaid.Menus));
        }

        //
        // GET: /OrderPaids/Details/5

        public ViewResult Details(System.Guid id)
        {
            return View(orderpaidRepository.Find(id));
        }

        //
        // GET: /OrderPaids/Create

        public ActionResult Create()
        {
			ViewBag.PossibleWaiters = waiterRepository.All;
			ViewBag.PossibleRestaurants = restaurantRepository.All;
            return View();
        } 

        //
        // POST: /OrderPaids/Create

        [HttpPost]
        public ActionResult Create(OrderPaid orderpaid)
        {
            if (ModelState.IsValid) {
                orderpaidRepository.InsertOrUpdate(orderpaid);
                orderpaidRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleWaiters = waiterRepository.All;
				ViewBag.PossibleRestaurants = restaurantRepository.All;
				return View();
			}
        }
        
        //
        // GET: /OrderPaids/Edit/5
 
        public ActionResult Edit(System.Guid id)
        {
			ViewBag.PossibleWaiters = waiterRepository.All;
			ViewBag.PossibleRestaurants = restaurantRepository.All;
             return View(orderpaidRepository.Find(id));
        }

        //
        // POST: /OrderPaids/Edit/5

        [HttpPost]
        public ActionResult Edit(OrderPaid orderpaid)
        {
            if (ModelState.IsValid) {
                orderpaidRepository.InsertOrUpdate(orderpaid);
                orderpaidRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleWaiters = waiterRepository.All;
				ViewBag.PossibleRestaurants = restaurantRepository.All;
				return View();
			}
        }

        //
        // GET: /OrderPaids/Delete/5
 
        public ActionResult Delete(System.Guid id)
        {
            return View(orderpaidRepository.Find(id));
        }

        //
        // POST: /OrderPaids/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(System.Guid id)
        {
            orderpaidRepository.Delete(id);
            orderpaidRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                waiterRepository.Dispose();
                restaurantRepository.Dispose();
                orderpaidRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

