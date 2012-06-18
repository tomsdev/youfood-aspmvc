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
    public class OrderPaidsController : Controller
    {
		private readonly IWaiterRepository waiterRepository;
		private readonly IRestaurantRepository restaurantRepository;
        private readonly IOrderPaidRepository orderpaidRepository;
        private readonly IProductTypeRepository producttypeRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public OrderPaidsController()
            : this(new WaiterRepository(), new RestaurantRepository(), new OrderPaidRepository(), new ProductTypeRepository())
        {
        }

        public OrderPaidsController(IWaiterRepository waiterRepository, IRestaurantRepository restaurantRepository, IOrderPaidRepository orderpaidRepository, IProductTypeRepository producttypeRepository)
        {
			this.waiterRepository = waiterRepository;
			this.restaurantRepository = restaurantRepository;
            this.orderpaidRepository = orderpaidRepository;
            this.producttypeRepository = producttypeRepository;
        }

        //
        // GET: /OrderPaids/

        public ViewResult Filter()
        {
            Guid? productTypeId = null;
            Guid? restaurantId = null;

            if (TempData["productTypeId"] != null)
            {
                productTypeId = TempData["productTypeId"] as Guid?;
            }

            if (TempData["restaurantId"] != null)
            {
                restaurantId = TempData["restaurantId"] as Guid?;
            }

            var query = orderpaidRepository.AllIncluding(orderpaid => orderpaid.Waiter, orderpaid => orderpaid.Restaurant, orderpaid => orderpaid.Menus);
            
            //if(productType != null && restaurant != null)
            //{
                
            //}

            var vm = new OrderFilterViewModel();

            if (restaurantId.HasValue)
            {
                vm.RestaurantId = restaurantId.Value;
                query = query.Where(p => p.RestaurantId == restaurantId.Value);
            }

            if (productTypeId.HasValue)
            {
                vm.ProductTypeId = productTypeId.Value;
                query = query.Where(p => p.Menus.Count(m => m.ProductTypeId == productTypeId.Value) > 0);
            }

            vm.Orders = query;

            ViewBag.PossibleRestaurants = restaurantRepository.All;
            ViewBag.PossibleProductTypes = producttypeRepository.All;

            return View(vm);
        }

        //
        // GET: /OrderPaids/
        [HttpPost]
        public ActionResult Filter(OrderFilterViewModel orderFilterViewModel)
        {
            TempData["productTypeId"] = orderFilterViewModel.ProductTypeId;
            TempData["restaurantId"] = orderFilterViewModel.RestaurantId;

            return RedirectToAction("Filter");
        }

        //
        // GET: /OrderPaids/

        public ViewResult Index()
        {
            return View(orderpaidRepository.AllIncluding(orderpaid => orderpaid.Waiter, orderpaid => orderpaid.Restaurant, orderpaid => orderpaid.Menus));
        }

        //
        // GET: /OrderPaids/Details/5

        public ViewResult Details(System.Guid id)
        {
            return View(orderpaidRepository.Find(id));
        }

        //
        // GET: /OrderPaids/Create

        public ActionResult Create()
        {
			ViewBag.PossibleWaiters = waiterRepository.All;
			ViewBag.PossibleRestaurants = restaurantRepository.All;
            return View();
        } 

        //
        // POST: /OrderPaids/Create

        [HttpPost]
        public ActionResult Create(OrderPaid orderpaid)
        {
            if (ModelState.IsValid) {
                orderpaidRepository.InsertOrUpdate(orderpaid);
                orderpaidRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleWaiters = waiterRepository.All;
				ViewBag.PossibleRestaurants = restaurantRepository.All;
				return View();
			}
        }
        
        //
        // GET: /OrderPaids/Edit/5
 
        public ActionResult Edit(System.Guid id)
        {
			ViewBag.PossibleWaiters = waiterRepository.All;
			ViewBag.PossibleRestaurants = restaurantRepository.All;
             return View(orderpaidRepository.Find(id));
        }

        //
        // POST: /OrderPaids/Edit/5

        [HttpPost]
        public ActionResult Edit(OrderPaid orderpaid)
        {
            if (ModelState.IsValid) {
                orderpaidRepository.InsertOrUpdate(orderpaid);
                orderpaidRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleWaiters = waiterRepository.All;
				ViewBag.PossibleRestaurants = restaurantRepository.All;
				return View();
			}
        }

        //
        // GET: /OrderPaids/Delete/5
 
        public ActionResult Delete(System.Guid id)
        {
            return View(orderpaidRepository.Find(id));
        }

        //
        // POST: /OrderPaids/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(System.Guid id)
        {
            orderpaidRepository.Delete(id);
            orderpaidRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                waiterRepository.Dispose();
                restaurantRepository.Dispose();
                orderpaidRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

