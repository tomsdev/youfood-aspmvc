using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tmf.entities;
using tmf.web.Models;
using tmf.web.ViewModels;

namespace tmf.web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWaiterRepository waiterRepository;
		private readonly IRestaurantRepository restaurantRepository;
		private readonly IOrderRepository orderRepository;
        private readonly IOrderCreatingRepository orderCreatingRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public HomeController()
            : this(new WaiterRepository(), new RestaurantRepository(), new OrderRepository(), new OrderCreatingRepository())
        {
        }

        public HomeController(IWaiterRepository waiterRepository, IRestaurantRepository restaurantRepository, IOrderRepository orderRepository, IOrderCreatingRepository orderCreatingRepository)
        {
			this.waiterRepository = waiterRepository;
			this.restaurantRepository = restaurantRepository;
			this.orderRepository = orderRepository;
            this.orderCreatingRepository = orderCreatingRepository;
        }

        public ActionResult Admin()
        {
            return View();
        }

        public ActionResult Index()
        {
            ViewBag.PossibleRestaurants = restaurantRepository.All;

            return View();
        }

        [HttpPost]
        public ActionResult Index(IndexViewModel indexViewModel)
        {
            var restaurant = restaurantRepository.Find(indexViewModel.RestaurantId);
            var table = indexViewModel.Table;

            Session["restaurant"] = restaurant;
            Session["table"] = table;

            //exemple pour recup le resto:
            //var monResto = Session["restaurant"] as Restaurant;

            return RedirectToAction("Roles");
        }

        public ActionResult Roles()
        {
            return View();
        }

        /// <summary>
        /// Stock choosen role in Session (as a string)
        /// </summary>
        /// <param name="category">client, waiter, cooker, admin</param>
        /// <returns></returns>
        public ActionResult ChooseRole(string role)
        {
            Session["role"] = role;

            if (role == "client")
            {
                return RedirectToAction("Create", "OrderCreatings");
            }
            else if (role == "waiter")
            {
                return RedirectToAction("Index", "OrderCreateds");
            }
            else if (role == "cooker")
            {
                return RedirectToAction("Index", "OrderPlaceds");
            }
            else if (role == "admin")
            {
                return RedirectToAction("Admin");
            }

            return RedirectToAction("Roles");
        }

        public PartialViewResult Result()
        {
            return PartialView();
        }


        public ActionResult About()
        {
            orderCreatingRepository.PurgeDatabase();
            ViewBag.Message = "Your quintessential app description page.";
            return View();
        }

        public ActionResult Contact()
        {
            orderCreatingRepository.PurgeDatabase();
            ViewBag.Message = "Your quintessential contact page.";
            return View();
        }
    }
}
