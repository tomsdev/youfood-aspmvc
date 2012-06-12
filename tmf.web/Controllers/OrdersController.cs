using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tmf.entities;
using tmf.web.Hubs;
using tmf.web.Models;

namespace tmf.web.Controllers
{   
    public class OrdersController : Controller
    {
		private readonly IWaiterRepository waiterRepository;
		private readonly IRestaurantRepository restaurantRepository;
		private readonly IOrderRepository orderRepository;
        private readonly IZoneRepository zoneRepository;
        private readonly IMenuRepository menuRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public OrdersController() : this(new WaiterRepository(), new RestaurantRepository(), new OrderRepository(), new ZoneRepository(), new MenuRepository())
        {
        }

        public OrdersController(IWaiterRepository waiterRepository, IRestaurantRepository restaurantRepository, IOrderRepository orderRepository, IZoneRepository zoneRepository, IMenuRepository menuRepository)
        {
			this.waiterRepository = waiterRepository;
			this.restaurantRepository = restaurantRepository;
			this.orderRepository = orderRepository;
            this.zoneRepository = zoneRepository;
            this.menuRepository = menuRepository;
        }

        //
        // GET: /Orders/

        public ViewResult Index()
        {
            return View(orderRepository.AllIncluding(order => order.Waiter, order => order.Restaurant, order => order.Menus));
        }

        //
        // GET: /Orders/Details/5

        public ViewResult Details(System.Guid id)
        {
            return View(orderRepository.Find(id));
        }

        //
        // GET: /Orders/Create

        public ActionResult Create()
        {
			ViewBag.PossibleWaiters = waiterRepository.All;
			ViewBag.PossibleRestaurants = restaurantRepository.All;
            return View();
        } 

        //
        // POST: /Orders/Create

        [HttpPost]
        public ActionResult Create(Order order, System.Guid idMenu)
        {
            if (ModelState.IsValid) {
                if (order.Table <= 10) // pour choisir une zone suivant le numero de table
                {
                    var zoneQuery = from zone in zoneRepository.All
                                    where zone.Name == "Zone1"
                                    select zone;

                    // c'est ici que ça chie je recup bien le waiterId mais le waiter il en a rien a foutre il reste a null quand meme
                    order.WaiterId = zoneQuery.FirstOrDefault().WaiterId;
                    order.Waiter = zoneQuery.FirstOrDefault().Waiter;
                    //c'est ici que le soft crash comme tout l'order est a null je peut rien y ajouter paradoxal je veux remplir bah il veut pas car null
                    order.Menus.Add(menuRepository.Find(idMenu));

                }
                orderRepository.InsertOrUpdate(order);
                orderRepository.Save();

                // SignalR
                // Push en temps réel du nouvel Order aux clients connecté
                TmfHubActions.CreateItem();

                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleWaiters = waiterRepository.All;
				ViewBag.PossibleRestaurants = restaurantRepository.All;
				return View();
			}
        }
        
        //
        // GET: /Orders/Edit/5
 
        public ActionResult Edit(System.Guid id)
        {
			ViewBag.PossibleWaiters = waiterRepository.All;
			ViewBag.PossibleRestaurants = restaurantRepository.All;
             return View(orderRepository.Find(id));
        }

        //
        // POST: /Orders/Edit/5

        [HttpPost]
        public ActionResult Edit(Order order)
        {
            if (ModelState.IsValid) {
                orderRepository.InsertOrUpdate(order);
                orderRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleWaiters = waiterRepository.All;
				ViewBag.PossibleRestaurants = restaurantRepository.All;
				return View();
			}
        }

        //
        // GET: /Orders/Delete/5
 
        public ActionResult Delete(System.Guid id)
        {
            return View(orderRepository.Find(id));
        }

        //
        // POST: /Orders/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(System.Guid id)
        {
            orderRepository.Delete(id);
            orderRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                waiterRepository.Dispose();
                restaurantRepository.Dispose();
                orderRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

