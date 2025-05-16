//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using Malyshev_Project;
//using Malyshev_Project.Models;

//namespace Malyshev_Project.Controllers
//{
//    public class DiscountController : Controller
//    {
//        private readonly ILogger<DiscountController> _logger;
//        private readonly PostgresContext _db;

//        public DiscountController(ILogger<DiscountController> logger, PostgresContext db)
//        {
//            _logger = logger;
//            _db = db;
//        }

//        public IActionResult List()
//        {
//            List<Discount> discounts = _db.Discounts
//                .Include(d => d.Product)
//                .ToList();
//            return View(discounts);
//        }

//        public IActionResult Create()
//        {
//            var model = new CreateDiscountModel
//            {
//                Products = _db.Products.ToList()
//            };

//            return View(model);
//        }

//        [HttpPost]
//        public IActionResult Create(CreateDiscountModel model)
//        {
//            var discount = (Discount)model;

//            _db.Discounts.Add(discount);
//            _db.SaveChanges();

//            int createdDiscountId = _db.Discounts.Last().IdDiscount;

//            return RedirectToAction("Details", "Discount", new { id = createdDiscountId });
//        }

//		public IActionResult Edit(int id)
//		{
//			var discount = _db.Discounts
//				.Include(d => d.Product)
//				.FirstOrDefault(d => d.IdDiscount == id);
//			if (discount == null) return NotFound();

//			var model = (EditDiscountModel)discount;
//            model.Products = _db.Products.ToList();

//			return View(model);
//		}

//		[HttpPost]
//		public IActionResult Edit(EditDiscountModel model)
//		{
//			var oldDiscount = _db.Discounts.FirstOrDefault(d => d.IdDiscount == model.IdDiscount);
//			if (oldDiscount == null) return NotFound();

//			oldDiscount.ProductId = model.ProductId;
//			oldDiscount.Amount = model.Amount;
//			oldDiscount.DateOfStart = model.DateOfStart;
//			oldDiscount.DateOfEnd = model.DateOfEnd;

//			_db.SaveChanges();
//			return RedirectToAction("Edit", "Discount", new { id = model.IdDiscount });
//		}

//		public IActionResult Details(int id)
//		{
//			var discount = _db.Discounts
//				.Include(d => d.Product)
//				.FirstOrDefault(d => d.IdDiscount == id);
//			if (discount == null) return NotFound();

//			return View(discount);
//		}

//		public IActionResult Detele(int id)
//		{
//			var discount = _db.Discounts.FirstOrDefault(d => d.IdDiscount == id);
//			if (discount == null) return NotFound();
//			return View(discount);
//		}

//		[HttpPost]
//		public IActionResult Delete(int id)
//		{
//			var discount = _db.Discounts.FirstOrDefault(d => d.IdDiscount == id);
//			if (discount == null) return NotFound();

//			_db.Discounts.Remove(discount);
//			_db.SaveChanges();
//			return RedirectToAction("List", "Discount");
//		}
//	}
//}
