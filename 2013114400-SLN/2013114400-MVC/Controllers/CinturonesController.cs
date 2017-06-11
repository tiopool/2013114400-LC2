using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using _2013114400_ENT.Entities;
using _2013114400_PER;
using _2013114400_ENT.IRepositories;

namespace _2013114400_MVC.Controllers
{
    public class CinturonesController : Controller
    {
        // private EnsambladoraDbContext db = new EnsambladoraDbContext();
        private readonly IUnityOfWork _UnityOfWork;

        public CinturonesController(IUnityOfWork unityOfWork)
        {
            _UnityOfWork = unityOfWork;

        }

        // GET: Cinturones
        public ActionResult Index()
        {
            var cinturones = _UnityOfWork.Cinturones.GetEntity().Include(c => c.Asiento);
            return View(cinturones.ToList());
        }

        // GET: Cinturones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cinturon cinturon = _UnityOfWork.Cinturones.Get(id);
            if (cinturon == null)
            {
                return HttpNotFound();
            }
            return View(cinturon);
        }

        // GET: Cinturones/Create
        public ActionResult Create()
        {
            ViewBag.CinturonId = new SelectList(_UnityOfWork.Asientos.GetEntity(), "AsientoId", "NumSerie");
            return View();
        }

        // POST: Cinturones/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CinturonId,NumSerie,Metraje")] Cinturon cinturon)
        {
            if (ModelState.IsValid)
            {
                _UnityOfWork.Cinturones.Add(cinturon);
                _UnityOfWork.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CinturonId = new SelectList(_UnityOfWork.Asientos.GetEntity(), "AsientoId", "NumSerie", cinturon.CinturonId);
            return View(cinturon);
        }

        // GET: Cinturones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cinturon cinturon = _UnityOfWork.Cinturones.Get(id);
            if (cinturon == null)
            {
                return HttpNotFound();
            }
            ViewBag.CinturonId = new SelectList(_UnityOfWork.Asientos.GetEntity(), "AsientoId", "NumSerie", cinturon.CinturonId);
            return View(cinturon);
        }

        // POST: Cinturones/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CinturonId,NumSerie,Metraje")] Cinturon cinturon)
        {
            if (ModelState.IsValid)
            {
                _UnityOfWork.StateModified(cinturon);
                _UnityOfWork.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CinturonId = new SelectList(_UnityOfWork.Asientos.GetEntity(), "AsientoId", "NumSerie", cinturon.CinturonId);
            return View(cinturon);
        }

        // GET: Cinturones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cinturon cinturon = _UnityOfWork.Cinturones.Get(id);
            if (cinturon == null)
            {
                return HttpNotFound();
            }
            return View(cinturon);
        }

        // POST: Cinturones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cinturon cinturon = _UnityOfWork.Cinturones.Get(id);
            _UnityOfWork.Cinturones.Remove(cinturon);
            _UnityOfWork.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _UnityOfWork.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
