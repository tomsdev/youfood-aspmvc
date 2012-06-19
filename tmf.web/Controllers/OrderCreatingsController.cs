using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tmf.entities;
using tmf.web.Models;

namespace tmf.web.Controllers
{   
    public class OrderCreatingsController : Controller
    {
		private readonly IWaiterRepository waiterRepository;
		private readonly IRestaurantRepository restaurantRepository;
		private readonly IOrderCreatingRepository ordercreatingRepository;
        private readonly IZoneRepository zoneRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public OrderCreatingsController() : this(new WaiterRepository(), new RestaurantRepository(), new OrderCreatingRepository(), new ZoneRepository())
        {
        }

        public OrderCreatingsController(IWaiterRepository waiterRepository, IRestaurantRepository restaurantRepository, IOrderCreatingRepository ordercreatingRepository, IZoneRepository zoneRepository)
        {
			this.waiterRepository = waiterRepository;
			this.restaurantRepository = restaurantRepository;
			this.ordercreatingRepository = ordercreatingRepository;
            this.zoneRepository = zoneRepository;
        }

        //
        // GET: /OrderCreatings/

        public ViewResult Index()
        {
            return View(ordercreatingRepository.AllIncluding(ordercreating => ordercreating.Waiter, ordercreating => ordercreating.Restaurant, ordercreating => ordercreating.Menus));
        }

        //
        // GET: /OrderCreatings/Details/5

        public ViewResult Details(System.Guid id)
        {
            return View(ordercreatingRepository.Find(id));
        }

        //
        // GET: /OrderCreatings/Create

        public ActionResult Create()
        {
            OrderCreating ordercreating = new OrderCreating();

            var monResto = Session["restaurant"] as Restaurant;
            ordercreating.RestaurantId = monResto.Id;

            var maTable = int.Parse(Session["table"].ToString());
            ordercreating.Table = maTable;

            var zone = zoneRepository.GetZoneByTable(maTable);
            ordercreating.WaiterId = zone.WaiterId;


            ordercreatingRepository.InsertOrUpdate(ordercreating);
            ordercreatingRepository.Save();

            // redirige sur l'ajout de menu pour un order:
            return RedirectToAction("OrderOneMenu", "Menus", new { idOrder = ordercreating.Id });

        } 
        
        //
        // GET: /OrderCreatings/Edit/5
 
        public ActionResult Edit(System.Guid id)
        {
			ViewBag.PossibleWaiters = waiterRepository.All;
			ViewBag.PossibleRestaurants = restaurantRepository.All;
             return View(ordercreatingRepository.Find(id));
        }

        //
        // POST: /OrderCreatings/Edit/5

        [HttpPost]
        public ActionResult Edit(OrderCreating ordercreating)
        {
            if (ModelState.IsValid) {
                ordercreatingRepository.InsertOrUpdate(ordercreating);
                ordercreatingRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleWaiters = waiterRepository.All;
				ViewBag.PossibleRestaurants = restaurantRepository.All;
				return View();
			}
        }

        //
        // GET: /OrderCreatings/Delete/5
 
        public ActionResult Delete(System.Guid id)
        {
            return View(ordercreatingRepository.Find(id));
        }

        //
        // POST: /OrderCreatings/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(System.Guid id)
        {
            ordercreatingRepository.Delete(id);
            ordercreatingRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                waiterRepository.Dispose();
                restaurantRepository.Dispose();
                ordercreatingRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

