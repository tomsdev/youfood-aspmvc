using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tmf.entities;
using tmf.web.Models;

namespace tmf.web.Controllers
{   
    public class OrderServedsController : Controller
    {
		private readonly IWaiterRepository waiterRepository;
		private readonly IRestaurantRepository restaurantRepository;
		private readonly IOrderServedRepository orderservedRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public OrderServedsController() : this(new WaiterRepository(), new RestaurantRepository(), new OrderServedRepository())
        {
        }

        public OrderServedsController(IWaiterRepository waiterRepository, IRestaurantRepository restaurantRepository, IOrderServedRepository orderservedRepository)
        {
			this.waiterRepository = waiterRepository;
			this.restaurantRepository = restaurantRepository;
			this.orderservedRepository = orderservedRepository;
        }

        //
        // GET: /OrderServeds/

        public ViewResult Index()
        {
            return View(orderservedRepository.AllIncluding(orderserved => orderserved.Waiter, orderserved => orderserved.Restaurant, orderserved => orderserved.Menus));
        }

        //
        // GET: /OrderServeds/Details/5

        public ViewResult Details(System.Guid id)
        {
            return View(orderservedRepository.Find(id));
        }

        //
        // GET: /OrderServeds/Create

        public ActionResult Create()
        {
			ViewBag.PossibleWaiters = waiterRepository.All;
			ViewBag.PossibleRestaurants = restaurantRepository.All;
            return View();
        } 

        //
        // POST: /OrderServeds/Create

        [HttpPost]
        public ActionResult Create(OrderServed orderserved)
        {
            if (ModelState.IsValid) {
                orderservedRepository.InsertOrUpdate(orderserved);
                orderservedRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleWaiters = waiterRepository.All;
				ViewBag.PossibleRestaurants = restaurantRepository.All;
				return View();
			}
        }
        
        //
        // GET: /OrderServeds/Edit/5
 
        public ActionResult Edit(System.Guid id)
        {
			ViewBag.PossibleWaiters = waiterRepository.All;
			ViewBag.PossibleRestaurants = restaurantRepository.All;
             return View(orderservedRepository.Find(id));
        }

        //
        // POST: /OrderServeds/Edit/5

        [HttpPost]
        public ActionResult Edit(OrderServed orderserved)
        {
            if (ModelState.IsValid) {
                orderservedRepository.InsertOrUpdate(orderserved);
                orderservedRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleWaiters = waiterRepository.All;
				ViewBag.PossibleRestaurants = restaurantRepository.All;
				return View();
			}
        }

        //
        // GET: /OrderServeds/Delete/5
 
        public ActionResult Delete(System.Guid id)
        {
            return View(orderservedRepository.Find(id));
        }

        //
        // POST: /OrderServeds/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(System.Guid id)
        {
            orderservedRepository.Delete(id);
            orderservedRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                waiterRepository.Dispose();
                restaurantRepository.Dispose();
                orderservedRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

