using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SignalR;
using SignalR.Hubs;
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

		// If you are using Dependency Injection, you can delete the following constructor
        public OrdersController() : this(new WaiterRepository(), new RestaurantRepository(), new OrderRepository())
        {
        }

        public OrdersController(IWaiterRepository waiterRepository, IRestaurantRepository restaurantRepository, IOrderRepository orderRepository)
        {
			this.waiterRepository = waiterRepository;
			this.restaurantRepository = restaurantRepository;
			this.orderRepository = orderRepository;
        }

        public PartialViewResult GetOrder(Guid id)
        {
            var order = orderRepository.Find(id);
            //var order = orderRepository.All.First();

            return PartialView("Order", order);
        }

        //
        // GET: /OrderPlaceds/CreateFromOrder

        public ActionResult CreateFromOrder(Guid idOrder, string controllerName, string state)
        {
            Order order = null;
            if (state == "created")
            {
                order = orderRepository.TransformOrderTo<OrderCreated>(idOrder);
                return RedirectToAction("PayCommand", "OrderCreateds");
            }
            else if (state == "placed")
            {
                order = orderRepository.TransformOrderTo<OrderPlaced>(idOrder);
                return RedirectToAction("Index", "OrderCreateds");
            }
            else if (state == "cooking")
            {
                order = orderRepository.TransformOrderTo<OrderCooking>(idOrder);
                return RedirectToAction("Index", "OrderPlaceds");
            }
            else if (state == "cooked")
            {
                order = orderRepository.TransformOrderTo<OrderCooked>(idOrder);
                return RedirectToAction("Index", "OrderPlaceds");
            }
            else if (state == "served")
            {
                order = orderRepository.TransformOrderTo<OrderServed>(idOrder);
                return RedirectToAction("Index", "OrderCreateds");
            }
            else if (state == "paid")
            {
                order = orderRepository.TransformOrderTo<OrderPaid>(idOrder);
                return RedirectToAction("Index", "OrderServeds");
            }
            
            TmfHubActions.AddOrder(order.Id, state);

            return RedirectToAction("Index", "Home");
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
        public ActionResult Create(Order order)
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

