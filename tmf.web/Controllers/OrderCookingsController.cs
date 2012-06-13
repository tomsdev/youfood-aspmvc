using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tmf.entities;
using tmf.web.Models;

namespace tmf.web.Controllers
{   
    public class OrderCookingsController : Controller
    {
		private readonly IWaiterRepository waiterRepository;
		private readonly IRestaurantRepository restaurantRepository;
		private readonly IOrderCookingRepository ordercookingRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public OrderCookingsController() : this(new WaiterRepository(), new RestaurantRepository(), new OrderCookingRepository())
        {
        }

        public OrderCookingsController(IWaiterRepository waiterRepository, IRestaurantRepository restaurantRepository, IOrderCookingRepository ordercookingRepository)
        {
			this.waiterRepository = waiterRepository;
			this.restaurantRepository = restaurantRepository;
			this.ordercookingRepository = ordercookingRepository;
        }

        //
        // GET: /OrderCookings/

        public ViewResult Index()
        {
            return View(ordercookingRepository.AllIncluding(ordercooking => ordercooking.Waiter, ordercooking => ordercooking.Restaurant, ordercooking => ordercooking.Menus));
        }

        //
        // GET: /OrderCookings/Details/5

        public ViewResult Details(System.Guid id)
        {
            return View(ordercookingRepository.Find(id));
        }

        //
        // GET: /OrderCookings/Create

        public ActionResult Create()
        {
			ViewBag.PossibleWaiters = waiterRepository.All;
			ViewBag.PossibleRestaurants = restaurantRepository.All;
            return View();
        } 

        //
        // POST: /OrderCookings/Create

        [HttpPost]
        public ActionResult Create(OrderCooking ordercooking)
        {
            if (ModelState.IsValid) {
                ordercookingRepository.InsertOrUpdate(ordercooking);
                ordercookingRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleWaiters = waiterRepository.All;
				ViewBag.PossibleRestaurants = restaurantRepository.All;
				return View();
			}
        }
        
        //
        // GET: /OrderCookings/Edit/5
 
        public ActionResult Edit(System.Guid id)
        {
			ViewBag.PossibleWaiters = waiterRepository.All;
			ViewBag.PossibleRestaurants = restaurantRepository.All;
             return View(ordercookingRepository.Find(id));
        }

        //
        // POST: /OrderCookings/Edit/5

        [HttpPost]
        public ActionResult Edit(OrderCooking ordercooking)
        {
            if (ModelState.IsValid) {
                ordercookingRepository.InsertOrUpdate(ordercooking);
                ordercookingRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleWaiters = waiterRepository.All;
				ViewBag.PossibleRestaurants = restaurantRepository.All;
				return View();
			}
        }

        //
        // GET: /OrderCookings/Delete/5
 
        public ActionResult Delete(System.Guid id)
        {
            return View(ordercookingRepository.Find(id));
        }

        //
        // POST: /OrderCookings/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(System.Guid id)
        {
            ordercookingRepository.Delete(id);
            ordercookingRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                waiterRepository.Dispose();
                restaurantRepository.Dispose();
                ordercookingRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

