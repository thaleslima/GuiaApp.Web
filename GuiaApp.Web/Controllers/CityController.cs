using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GuiaApp.Model;

namespace GuiaApp.Web.Controllers
{
    public class CityController : Controller
    {
        private BDContext db = new BDContext();

        // GET: /City/
        public ActionResult Index()
        {
            return View(db.City.ToList());
        }

        [HttpPost]
        public ActionResult Index(string Descricao, string Status)
        {
            return View(db.User.ToList());
        }

        // GET: /City/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            City city = db.City.Find(id);
            if (city == null)
            {
                return HttpNotFound();
            }
            return View(city);
        }

        // GET: /City/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /City/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Description,Uf")] City city)
        {
            if (ModelState.IsValid)
            {
                db.City.Add(city);
                db.SaveChanges();
                TempData["messageSuccess"] = "Record inserted successfully";
                return RedirectToAction("Index");
            }

            return View(city);
        }

        // GET: /City/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            City city = db.City.Find(id);
            if (city == null)
            {
                return HttpNotFound();
            }
            return View(city);
        }

        // POST: /City/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Description,Uf")] City city)
        {
            if (ModelState.IsValid)
            {
                db.Entry(city).State = EntityState.Modified;
                db.SaveChanges();

                TempData["messageSuccess"] = "Record edited successfully";
                return RedirectToAction("Index");
            }
            return View(city);
        }

        // GET: /City/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            City city = db.City.Find(id);
            if (city == null)
            {
                return HttpNotFound();
            }
            return View(city);
        }

        // POST: /City/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            City city = db.City.Find(id);
            db.City.Remove(city);
            db.SaveChanges();

            TempData["messageSuccess"] = "Record deleted successfully";
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
