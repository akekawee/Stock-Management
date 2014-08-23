using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StockManagementSystem.Models;

namespace StockManagementSystem.Controllers
{
    public class StockController : Controller
    {
        private stockmanagementEntities db = new stockmanagementEntities();

        //
        // GET: /Stock/

        public ActionResult Index()
        {
            var stock = db.stock.Include(s => s.Product).Include(s => s.Warehouse);
            return View(stock.ToList());
        }

        //
        // GET: /Stock/Details/5

        public ActionResult Details(int id = 0)
        {
            stock stock = db.stock.Find(id);
            if (stock == null)
            {
                return HttpNotFound();
            }
            return View(stock);
        }

        //
        // GET: /Stock/Create

        public ActionResult Create()
        {
            ViewBag.product_id = new SelectList(db.product, "product_id", "product_name");
            ViewBag.warehouse_id = new SelectList(db.warehouse, "warehouse_id", "warehouse_name");
            return View();
        }

        //
        // POST: /Stock/Create
        public ActionResult Report()
        {
            return Redirect("../Reports/StockReport.aspx");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(stock stock)
        {
            if (ModelState.IsValid)
            {
                db.stock.Add(stock);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.product_id = new SelectList(db.product, "product_id", "product_name", stock.product_id);
            ViewBag.warehouse_id = new SelectList(db.warehouse, "warehouse_id", "warehouse_name", stock.warehouse_id);
            return View(stock);
        }

        //
        // GET: /Stock/Edit/5

        public ActionResult Edit(int id = 0)
        {
            stock stock = db.stock.Find(id);
            if (stock == null)
            {
                return HttpNotFound();
            }
            ViewBag.product_id = new SelectList(db.product, "product_id", "product_name", stock.product_id);
            ViewBag.warehouse_id = new SelectList(db.warehouse, "warehouse_id", "warehouse_name", stock.warehouse_id);
            return View(stock);
        }

        //
        // POST: /Stock/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(stock stock)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stock).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.product_id = new SelectList(db.product, "product_id", "product_name", stock.product_id);
            ViewBag.warehouse_id = new SelectList(db.warehouse, "warehouse_id", "warehouse_name", stock.warehouse_id);
            return View(stock);
        }

        //
        // GET: /Stock/Delete/5

        public ActionResult Delete(int id = 0)
        {
            stock stock = db.stock.Find(id);
            if (stock == null)
            {
                return HttpNotFound();
            }
            return View(stock);
        }

        //
        // POST: /Stock/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            stock stock = db.stock.Find(id);
            db.stock.Remove(stock);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}