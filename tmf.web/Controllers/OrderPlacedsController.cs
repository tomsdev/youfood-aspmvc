using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tmf.entities;
using tmf.web.Models;

namespace tmf.web.Controllers
{   
    public class OrderPlacedsController : Controller
    {
		private readonly IWaiterRepository waiterRepository;
		private readonly IRestaurantRepository restaurantRepository;
		private readonly IOrderPlacedRepository orderplacedRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public OrderPlacedsController() : this(new WaiterRepository(), new RestaurantRepository(), new OrderPlacedRepository())
        {
        }

        public OrderPlacedsController(IWaiterRepository waiterRepository, IRestaurantRepository restaurantRepository, IOrderPlacedRepository orderplacedRepository)
        {
			this.waiterRepository = waiterRepository;
			this.restaurantRepository = restaurantRepository;
			this.orderplacedRepository = orderplacedRepository;
        }

        //
        // GET: /OrderPlaceds/

        public ViewResult Index()
        {
            return View(orderplacedRepository.AllIncluding(orderplaced => orderplaced.Waiter, orderplaced => orderplaced.Restaurant, orderplaced => orderplaced.Menus));
        }

        //
        // GET: /OrderPlaceds/Details/5

        public ViewResult Details(System.Guid id)
        {
            return View(orderplacedRepository.Find(id));
        }

        //
        // GET: /OrderPlaceds/Create

        public ActionResult Create()
        {
			ViewBag.PossibleWaiters = waiterRepository.All;
			ViewBag.PossibleRestaurants = restaurantRepository.All;
            return View();
        } 

        //
        // POST: /OrderPlaceds/Create

        [HttpPost]
        public ActionResult Create(OrderPlaced orderplaced)
        {
            if (ModelState.IsValid) {
                orderplacedRepository.InsertOrUpdate(orderplaced);
                orderplacedRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleWaiters = waiterRepository.All;
				ViewBag.PossibleRestaurants = restaurantRepository.All;
				return View();
			}
        }
        
        //
        // GET: /OrderPlaceds/Edit/5
 
        public ActionResult Edit(System.Guid id)
        {
			ViewBag.PossibleWaiters = waiterRepository.All;
			ViewBag.PossibleRestaurants = restaurantRepository.All;
             return View(orderplacedRepository.Find(id));
        }

        //
        // POST: /OrderPlaceds/Edit/5

        [HttpPost]
        public ActionResult Edit(OrderPlaced orderplaced)
        {
            if (ModelState.IsValid) {
                orderplacedRepository.InsertOrUpdate(orderplaced);
                orderplacedRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleWaiters = waiterRepository.All;
				ViewBag.PossibleRestaurants = restaurantRepository.All;
				return View();
			}
        }

        //
        // GET: /OrderPlaceds/Delete/5
 
        public ActionResult Delete(System.Guid id)
        {
            return View(orderplacedRepository.Find(id));
        }

        //
        // POST: /OrderPlaceds/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(System.Guid id)
        {
            orderplacedRepository.Delete(id);
            orderplacedRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                waiterRepository.Dispose();
                restaurantRepository.Dispose();
                orderplacedRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

