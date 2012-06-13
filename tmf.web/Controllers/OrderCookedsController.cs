using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tmf.entities;
using tmf.web.Models;

namespace tmf.web.Controllers
{   
    public class OrderCookedsController : Controller
    {
		private readonly IWaiterRepository waiterRepository;
		private readonly IRestaurantRepository restaurantRepository;
		private readonly IOrderCookedRepository ordercookedRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public OrderCookedsController() : this(new WaiterRepository(), new RestaurantRepository(), new OrderCookedRepository())
        {
        }

        public OrderCookedsController(IWaiterRepository waiterRepository, IRestaurantRepository restaurantRepository, IOrderCookedRepository ordercookedRepository)
        {
			this.waiterRepository = waiterRepository;
			this.restaurantRepository = restaurantRepository;
			this.ordercookedRepository = ordercookedRepository;
        }

        //
        // GET: /OrderCookeds/

        public ViewResult Index()
        {
            return View(ordercookedRepository.AllIncluding(ordercooked => ordercooked.Waiter, ordercooked => ordercooked.Restaurant, ordercooked => ordercooked.Menus));
        }

        //
        // GET: /OrderCookeds/Details/5

        public ViewResult Details(System.Guid id)
        {
            return View(ordercookedRepository.Find(id));
        }

        //
        // GET: /OrderCookeds/Create

        public ActionResult Create()
        {
			ViewBag.PossibleWaiters = waiterRepository.All;
			ViewBag.PossibleRestaurants = restaurantRepository.All;
            return View();
        } 

        //
        // POST: /OrderCookeds/Create

        [HttpPost]
        public ActionResult Create(OrderCooked ordercooked)
        {
            if (ModelState.IsValid) {
                ordercookedRepository.InsertOrUpdate(ordercooked);
                ordercookedRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleWaiters = waiterRepository.All;
				ViewBag.PossibleRestaurants = restaurantRepository.All;
				return View();
			}
        }
        
        //
        // GET: /OrderCookeds/Edit/5
 
        public ActionResult Edit(System.Guid id)
        {
			ViewBag.PossibleWaiters = waiterRepository.All;
			ViewBag.PossibleRestaurants = restaurantRepository.All;
             return View(ordercookedRepository.Find(id));
        }

        //
        // POST: /OrderCookeds/Edit/5

        [HttpPost]
        public ActionResult Edit(OrderCooked ordercooked)
        {
            if (ModelState.IsValid) {
                ordercookedRepository.InsertOrUpdate(ordercooked);
                ordercookedRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleWaiters = waiterRepository.All;
				ViewBag.PossibleRestaurants = restaurantRepository.All;
				return View();
			}
        }

        //
        // GET: /OrderCookeds/Delete/5
 
        public ActionResult Delete(System.Guid id)
        {
            return View(ordercookedRepository.Find(id));
        }

        //
        // POST: /OrderCookeds/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(System.Guid id)
        {
            ordercookedRepository.Delete(id);
            ordercookedRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                waiterRepository.Dispose();
                restaurantRepository.Dispose();
                ordercookedRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

