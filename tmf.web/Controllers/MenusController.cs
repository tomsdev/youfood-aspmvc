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
    public class MenusController : Controller
    {
		private readonly IProductTypeRepository producttypeRepository;
		private readonly IMenuRepository menuRepository;
        private readonly IOrderRepository orderRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public MenusController() : this(new ProductTypeRepository(), new MenuRepository(), new OrderRepository())
        {
        }

        public MenusController(IProductTypeRepository producttypeRepository, IMenuRepository menuRepository, IOrderRepository orderRepository)
        {
			this.producttypeRepository = producttypeRepository;
			this.menuRepository = menuRepository;
            this.orderRepository = orderRepository;
        }

        //
        // GET: /Menus/

        public ViewResult Index()
        {
            return View(menuRepository.AllIncluding(menu => menu.ProductType));
        }

        public ViewResult OrderOneMenu(System.Guid idOrder)
        {
            var vm = new OrderOneMenuViewModel();
            vm.Menus = menuRepository.AllIncluding(menu => menu.ProductType);
            vm.IdOrder = idOrder;
            return View(vm);
        }

        [HttpPost]
        public ActionResult OrderOneMenu(OrderOneMenuViewModel orderOneMenuViewModel)
        {
            if (ModelState.IsValid)
            {
                var menu = menuRepository.Find(orderOneMenuViewModel.IdMenuSelected);
                var order = orderRepository.Find(orderOneMenuViewModel.IdOrder);
                //many to many pose probleme
                //a voir pourquoi il recrée un menu au lieu de faire une foreign key; il s'agit d'une collection de menu le probleme viens peut etre de la

                //menuRepository.AddMenuToOrder(order, menu);
                order.Menus.Add(menu);
                
                orderRepository.Save();

                if (orderOneMenuViewModel.IsOrderTerminated)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("OrderOneMenu", new { orderOneMenuViewModel.IdOrder });
                }
            }
            else
            {
                ViewBag.PossibleProductTypes = producttypeRepository.All;
                return View();
            }
        }

        //
        // GET: /Menus/Details/5

        public ViewResult Details(System.Guid id)
        {
            return View(menuRepository.Find(id));
        }

        //
        // GET: /Menus/Create

        public ActionResult Create()
        {
			ViewBag.PossibleProductTypes = producttypeRepository.All;
            return View();
        } 

        //
        // POST: /Menus/Create

        [HttpPost]
        public ActionResult Create(Menu menu)
        {
            if (ModelState.IsValid) {
                menuRepository.InsertOrUpdate(menu);
                menuRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleProductTypes = producttypeRepository.All;
				return View();
			}
        }
        
        //
        // GET: /Menus/Edit/5
 
        public ActionResult Edit(System.Guid id)
        {
			ViewBag.PossibleProductTypes = producttypeRepository.All;
             return View(menuRepository.Find(id));
        }

        //
        // POST: /Menus/Edit/5

        [HttpPost]
        public ActionResult Edit(Menu menu)
        {
            if (ModelState.IsValid) {
                menuRepository.InsertOrUpdate(menu);
                menuRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleProductTypes = producttypeRepository.All;
				return View();
			}
        }

        //
        // GET: /Menus/Delete/5
 
        public ActionResult Delete(System.Guid id)
        {
            return View(menuRepository.Find(id));
        }

        //
        // POST: /Menus/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(System.Guid id)
        {
            menuRepository.Delete(id);
            menuRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                producttypeRepository.Dispose();
                menuRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

