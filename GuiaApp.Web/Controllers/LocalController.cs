using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GuiaApp.Model;
using System.IO;

namespace GuiaApp.Web.Controllers
{
    public class LocalController : Controller
    {
        private BDContext db = new BDContext();

        // GET: /Local/
        public ActionResult Index()
        {
            var local = db.Local.Include(l => l.City).Include(l => l.Menu);
            return View(local.ToList());
        }

        [HttpPost]
        public ActionResult Index(string Descricao, string Status)
        {
            return View(db.User.ToList());
        }


        // GET: /Local/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Local local = db.Local.Find(id);
            if (local == null)
            {
                return HttpNotFound();
            }
            return View(local);
        }

        // GET: /Local/Create
        public ActionResult Create()
        {
            ViewBag.IdCity = new SelectList(db.City, "Id", "Description");
            ViewBag.IdMenu = new SelectList(db.Menu, "Id", "Description");
            return View();
        }

        // POST: /Local/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Name,Telephone,Site,Email,Description,Address,Latitude,Longitude,Active,PathImage,IdMenu,IdCity")] Local local)
        {
            if (ModelState.IsValid)
            {
                local.Date = DateTime.Now;

                if (!InsereImage(local))
                {
                    return View(local);
                }

                db.Local.Add(local);
                db.SaveChanges();
                TempData["messageSuccess"] = "Record inserted successfully";
                return RedirectToAction("Index");
            }

            ViewBag.IdCity = new SelectList(db.City, "Id", "Description", local.IdCity);
            ViewBag.IdMenu = new SelectList(db.Menu, "Id", "Description", local.IdMenu);
            return View(local);
        }


        public bool InsereImage(Local local)
        {
            if (!string.IsNullOrEmpty(local.PathImage))
            {
                try
                {
                    string sourceFile = Path.Combine(Server.MapPath("~/ImagensAux"), local.PathImage);
                    if(!System.IO.File.Exists(sourceFile))
                    {
                        return true;
                    }

                    Local localAux = db.Local.Where(p => p.Id == local.Id).FirstOrDefault();
                    if (localAux != null && !string.IsNullOrEmpty(localAux.PathImage))
                    {
                        string source = Path.Combine(Server.MapPath("~/Imagens"), localAux.PathImage);
                        System.IO.File.Delete(source);
                    }

                    Image image = db.Image.Where(q => q.Name == local.PathImage).FirstOrDefault();

                    if (image == null)
                    {
                        ModelState.AddModelError(string.Empty, "Imagem não encontrada!");
                        return false;
                    }

                    db.Image.Remove(image);
                    string destFile = Path.Combine(Server.MapPath("~/Imagens"), local.PathImage);
                    System.IO.File.Move(sourceFile, destFile);
                    db.SaveChanges();

                }
                catch (Exception e)
                {
                    ModelState.AddModelError(string.Empty, "Imagem não encontrada! " + e.Message);
                    return false;
                }
            }

            return true;
        }


        // GET: /Local/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Local local = db.Local.Find(id);
            if (local == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdCity = new SelectList(db.City, "Id", "Description", local.IdCity);
            ViewBag.IdMenu = new SelectList(db.Menu, "Id", "Description", local.IdMenu);
            return View(local);
        }

        // POST: /Local/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Name,Telephone,Site,Email,Description,Latitude,Longitude,Address,Active,PathImage,IdMenu,IdCity")] Local local)
        {
            if (ModelState.IsValid)
            {
                local.Date = DateTime.Now;
                db.Entry(local).State = EntityState.Modified;

                if (!InsereImage(local))
                {
                    return View(local);
                }

                db.SaveChanges();
                TempData["messageSuccess"] = "Record edited successfully";
                return RedirectToAction("Index");
            }
            ViewBag.IdCity = new SelectList(db.City, "Id", "Description", local.IdCity);
            ViewBag.IdMenu = new SelectList(db.Menu, "Id", "Description", local.IdMenu);
            return View(local);
        }

        // GET: /Local/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Local local = db.Local.Find(id);
            if (local == null)
            {
                return HttpNotFound();
            }
            return View(local);
        }

        // POST: /Local/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Local local = db.Local.Find(id);
            db.Local.Remove(local);
            db.SaveChanges();

            try
            {
                if(!string.IsNullOrEmpty(local.PathImage))
                {
                    string source = Path.Combine(Server.MapPath("~/Imagens"), local.PathImage);
                    System.IO.File.Delete(source);
                }
            }
            catch (Exception)
            {
                
            }
 
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

        [HttpPost]
        public ActionResult InsertImage()
        {
            try
            {
                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase file = Request.Files[0];
                    string extension = Path.GetExtension(file.FileName);

                    Image image = new Image() { Name = "" };
                    db.Image.Add(image);

                    db.SaveChanges();

                    string fileName = image.Id + extension;
                    string uploadPath = Server.MapPath("~/ImagensAux");
                    file.SaveAs(Path.Combine(uploadPath, fileName));

                    image.Name = fileName;
                    db.Entry(image).State = EntityState.Modified;
                    db.SaveChanges();

                    return Json(new
                   {
                       success = true,
                       file = fileName,
                       messagem = ""
                   });
                }
            }
            catch (Exception e)
            {
                return Json(new
                       {
                           success = false,
                           file = "",
                           messagem = e.Message
                       });

            }

            return Json(new
            {
                success = false,
                messagem = "Arquivo não enviado."
            });
        }
    }
}
