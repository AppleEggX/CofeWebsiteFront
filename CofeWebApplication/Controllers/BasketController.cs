using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CofeWebApplication.Models;
using CofeWebApplication.BasketServiceReference;

namespace CofeWebApplication.Controllers
{
    public class BasketController : Controller
    {
        private newcofeshopEntities db = new newcofeshopEntities();
        Service1Client basketSrv = new Service1Client();

        // GET: Basket
        public ActionResult Index(string id)
        {
            var baskets = from b in db.Baskets
                          select b;
            if (!String.IsNullOrEmpty(id))
            {
                int basketIdInInt = Int32.Parse(id);
                baskets = baskets.Where(s => s.Id == basketIdInInt);
                var inBasketItems = basketSrv.GetTheBasketItems(basketIdInInt);
                ViewBag.items = inBasketItems;
                return View(baskets.ToList());
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        // GET: Basket/Details/5
        public ActionResult Details(int id)
        {
            //if (id)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            BasketItem basketItem = db.BasketItems.Where(m => m.Id == id).FirstOrDefault();
            //Basket basket = db.Baskets.Find(id);
            if (basketItem == null)
            {
                return HttpNotFound();
            }
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
        public ActionResult Edit(int id)
        {
            if (ModelState.IsValid)
            {
                int amount_to_change = Int32.Parse(Request.Form["Amount"]);
                var item = db.BasketItems.Find((int)id);
                basketSrv.EditBasketItem(id, amount_to_change);
                return RedirectToAction("Index",new { id = item.Basket_Id });
            }
            BasketItem basketItem = db.BasketItems.Find(id);
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
            int basket_id = (int)basketItem.Basket_Id;
            basketSrv.DeleteFromBasket(basket_id, (int)basketItem.Coffee_Id);
            //db.BasketItems.Remove(basketItem);
            //db.SaveChanges();
            return RedirectToAction("Index", new { id = basket_id });
        }

        // GET: BasketItems/Delete/5
        public ActionResult Checkout(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Basket basket = db.Baskets.Find(id);
            if (basket == null)
            {
                return HttpNotFound();
            }
            return View(basket);
        }

        // POST: BasketItems/Delete/5
        [HttpPost, ActionName("Checkout")]
        [ValidateAntiForgeryToken]
        public ActionResult CheckoutConfirmed(int id)
        {
            var basket = basketSrv.CheckOutBasket(id);
            int new_basket_id = (int)basket.Id;
            Response.Cookies["Basket_id"].Value = new_basket_id.ToString();
            return RedirectToAction("Index", new { id = new_basket_id });
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
