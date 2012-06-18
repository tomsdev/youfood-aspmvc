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

		// If you are using Dependency Injection, you can delete the following constructor
        public HomeController()
            : this(new WaiterRepository(), new RestaurantRepository(), new OrderRepository())
        {
        }

        public HomeController(IWaiterRepository waiterRepository, IRestaurantRepository restaurantRepository, IOrderRepository orderRepository)
        {
			this.waiterRepository = waiterRepository;
			this.restaurantRepository = restaurantRepository;
			this.orderRepository = orderRepository;
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

            Session["restaurant"] = restaurant;

            //exemple pour recup le resto:
            //var monResto = Session["restaurant"] as Restaurant;

            return RedirectToAction("About");
        }

        public PartialViewResult Result()
        {
            return PartialView();
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your quintessential app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your quintessential contact page.";

            return View();
        }
    }
}
