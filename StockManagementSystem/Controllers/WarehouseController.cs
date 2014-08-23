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
    public class WarehouseController : Controller
    {
        private stockmanagementEntities db = new stockmanagementEntities();

        //
        // GET: /Warehouse/

        public ActionResult Index()
        {
            var warehouse = db.warehouse.Include(w => w.Type);
            return View(warehouse.ToList());
        }

        //
        // GET: /Warehouse/Details/5

        public ActionResult Details(int id = 0)
        {
            warehouse warehouse = db.warehouse.Find(id);
            if (warehouse == null)
            {
                return HttpNotFound();
            }
            return View(warehouse);
        }

        //
        // GET: /Warehouse/Create

        public ActionResult Create()
        {
            ViewBag.warehouse_type = new SelectList(db.type, "type_id", "type_name");
            return View();
        }

        //
        // POST: /Warehouse/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(warehouse warehouse)
        {
            if (ModelState.IsValid)
            {
                db.warehouse.Add(warehouse);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.warehouse_type = new SelectList(db.type, "type_id", "type_name", warehouse.warehouse_type);
            return View(warehouse);
        }

        //
        // GET: /Warehouse/Edit/5

        public ActionResult Edit(int id = 0)
        {
            warehouse warehouse = db.warehouse.Find(id);
            if (warehouse == null)
            {
                return HttpNotFound();
            }
            ViewBag.warehouse_type = new SelectList(db.type, "type_id", "type_name", warehouse.warehouse_type);
            return View(warehouse);
        }

        //
        // POST: /Warehouse/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(warehouse warehouse)
        {
            if (ModelState.IsValid)
            {
                db.Entry(warehouse).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.warehouse_type = new SelectList(db.type, "type_id", "type_name", warehouse.warehouse_type);
            return View(warehouse);
        }

        //
        // GET: /Warehouse/Delete/5

        public ActionResult Delete(int id = 0)
        {
            warehouse warehouse = db.warehouse.Find(id);
            if (warehouse == null)
            {
                return HttpNotFound();
            }
            return View(warehouse);
        }

        //
        // POST: /Warehouse/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            warehouse warehouse = db.warehouse.Find(id);
            db.warehouse.Remove(warehouse);
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