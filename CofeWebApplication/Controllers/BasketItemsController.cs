using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CofeWebApplication.Models;

namespace CofeWebApplication.Controllers
{
    public class BasketItemsController : Controller
    {
        private newcofeshopEntities db = new newcofeshopEntities();

        // GET: BasketItems
        public ActionResult Index()
        {
            var basketItems = db.BasketItems.Include(b => b.Basket).Include(b => b.Coffee);
            //ViewBag.items = basketItems;
            return View(basketItems.ToList());
        }

        // GET: BasketItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BasketItem basketItem = db.BasketItems.Find(id);
            if (basketItem == null)
            {
                return HttpNotFound();
            }
            return View(basketItem);
        }

        // GET: BasketItems/Create
        public ActionResult Create()
        {
            ViewBag.Basket_Id = new SelectList(db.Baskets, "Id", "Id");
            ViewBag.Coffee_Id = new SelectList(db.Coffees, "Id", "Name");
            return View();
        }

        // POST: BasketItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Amount,TotalPrice,Basket_Id,Coffee_Id")] BasketItem basketItem)
        {
            if (ModelState.IsValid)
            {
                db.BasketItems.Add(basketItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Basket_Id = new SelectList(db.Baskets, "Id", "Id", basketItem.Basket_Id);
            ViewBag.Coffee_Id = new SelectList(db.Coffees, "Id", "Name", basketItem.Coffee_Id);
            return View(basketItem);
        }

        // GET: BasketItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BasketItem basketItem = db.BasketItems.Find(id);
            if (basketItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.Basket_Id = new SelectList(db.Baskets, "Id", "Id", basketItem.Basket_Id);
            ViewBag.Coffee_Id = new SelectList(db.Coffees, "Id", "Name", basketItem.Coffee_Id);
            return View(basketItem);
        }

        // POST: BasketItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Amount,TotalPrice,Basket_Id,Coffee_Id")] BasketItem basketItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(basketItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Basket_Id = new SelectList(db.Baskets, "Id", "Id", basketItem.Basket_Id);
            ViewBag.Coffee_Id = new SelectList(db.Coffees, "Id", "Name", basketItem.Coffee_Id);
            return View(basketItem);
        }

        // GET: BasketItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BasketItem basketItem = db.BasketItems.Find(id);
            if (basketItem == null)
            {
                return HttpNotFound();
            }
            return View(basketItem);
        }

        // POST: BasketItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BasketItem basketItem = db.BasketItems.Find(id);
            db.BasketItems.Remove(basketItem);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
