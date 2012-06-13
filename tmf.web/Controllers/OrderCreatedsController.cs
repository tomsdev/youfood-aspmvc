using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tmf.entities;
using tmf.web.Models;

namespace tmf.web.Controllers
{   
    public class OrderCreatedsController : Controller
    {
		private readonly IWaiterRepository waiterRepository;
		private readonly IRestaurantRepository restaurantRepository;
		private readonly IOrderCreatedRepository ordercreatedRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public OrderCreatedsController() : this(new WaiterRepository(), new RestaurantRepository(), new OrderCreatedRepository())
        {
        }

        public OrderCreatedsController(IWaiterRepository waiterRepository, IRestaurantRepository restaurantRepository, IOrderCreatedRepository ordercreatedRepository)
        {
			this.waiterRepository = waiterRepository;
			this.restaurantRepository = restaurantRepository;
			this.ordercreatedRepository = ordercreatedRepository;
        }

        //
        // GET: /OrderCreateds/

        public ViewResult Index()
        {
            return View(ordercreatedRepository.AllIncluding(ordercreated => ordercreated.Waiter, ordercreated => ordercreated.Restaurant, ordercreated => ordercreated.Menus));
        }

        //
        // GET: /OrderCreateds/Details/5

        public ViewResult Details(System.Guid id)
        {
            return View(ordercreatedRepository.Find(id));
        }

        //
        // GET: /OrderCreateds/Create

        public ActionResult Create()
        {
			ViewBag.PossibleWaiters = waiterRepository.All;
			ViewBag.PossibleRestaurants = restaurantRepository.All;
            return View();
        } 

        //
        // POST: /OrderCreateds/Create

        [HttpPost]
        public ActionResult Create(OrderCreated ordercreated)
        {
            if (ModelState.IsValid) {
                ordercreatedRepository.InsertOrUpdate(ordercreated);
                ordercreatedRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleWaiters = waiterRepository.All;
				ViewBag.PossibleRestaurants = restaurantRepository.All;
				return View();
			}
        }
        
        //
        // GET: /OrderCreateds/Edit/5
 
        public ActionResult Edit(System.Guid id)
        {
			ViewBag.PossibleWaiters = waiterRepository.All;
			ViewBag.PossibleRestaurants = restaurantRepository.All;
             return View(ordercreatedRepository.Find(id));
        }

        //
        // POST: /OrderCreateds/Edit/5

        [HttpPost]
        public ActionResult Edit(OrderCreated ordercreated)
        {
            if (ModelState.IsValid) {
                ordercreatedRepository.InsertOrUpdate(ordercreated);
                ordercreatedRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleWaiters = waiterRepository.All;
				ViewBag.PossibleRestaurants = restaurantRepository.All;
				return View();
			}
        }

        //
        // GET: /OrderCreateds/Delete/5
 
        public ActionResult Delete(System.Guid id)
        {
            return View(ordercreatedRepository.Find(id));
        }

        //
        // POST: /OrderCreateds/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(System.Guid id)
        {
            ordercreatedRepository.Delete(id);
            ordercreatedRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                waiterRepository.Dispose();
                restaurantRepository.Dispose();
                ordercreatedRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

