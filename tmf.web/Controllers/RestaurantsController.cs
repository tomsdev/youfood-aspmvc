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
    public class RestaurantsController : Controller
    {
		private readonly IRestaurantRepository restaurantRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public RestaurantsController() : this(new RestaurantRepository())
        {
        }

        public RestaurantsController(IRestaurantRepository restaurantRepository)
        {
			this.restaurantRepository = restaurantRepository;
        }

        //
        // GET: /Restaurants/

        public ViewResult Index()
        {
            return View(restaurantRepository.AllIncluding(restaurant => restaurant.Orders));
        }

        //
        // GET: /Restaurants/Details/5

        public ViewResult Details(System.Guid id)
        {
            return View(restaurantRepository.Find(id));
        }

        //
        // GET: /Restaurants/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Restaurants/Create

        [HttpPost]
        public ActionResult Create(Restaurant restaurant)
        {
            if (ModelState.IsValid) {
                restaurantRepository.InsertOrUpdate(restaurant);
                restaurantRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }
        
        //
        // GET: /Restaurants/Edit/5
 
        public ActionResult Edit(System.Guid id)
        {
             return View(restaurantRepository.Find(id));
        }

        //
        // POST: /Restaurants/Edit/5

        [HttpPost]
        public ActionResult Edit(Restaurant restaurant)
        {
            if (ModelState.IsValid) {
                restaurantRepository.InsertOrUpdate(restaurant);
                restaurantRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }

        //
        // GET: /Restaurants/Delete/5
 
        public ActionResult Delete(System.Guid id)
        {
            return View(restaurantRepository.Find(id));
        }

        //
        // POST: /Restaurants/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(System.Guid id)
        {
            restaurantRepository.Delete(id);
            restaurantRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                restaurantRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

