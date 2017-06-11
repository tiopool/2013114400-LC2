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
    public class ParabrisasController : Controller
    {
        // private EnsambladoraDbContext db = new EnsambladoraDbContext();

        private readonly IUnityOfWork _UnityOfWork;

        public ParabrisasController(IUnityOfWork unityOfWork)
        {
            _UnityOfWork = unityOfWork;

        }

        // GET: Parabrisas
        public ActionResult Index()
        {
            return View(_UnityOfWork.Parabrisas.GetAll());
        }

        // GET: Parabrisas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Parabrisas parabrisas = _UnityOfWork.Parabrisas.Get(id);
            if (parabrisas == null)
            {
                return HttpNotFound();
            }
            return View(parabrisas);
        }

        // GET: Parabrisas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Parabrisas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ParabrisasId,NumSerie")] Parabrisas parabrisas)
        {
            if (ModelState.IsValid)
            {
                _UnityOfWork.Parabrisas.Add(parabrisas);
                _UnityOfWork.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(parabrisas);
        }

        // GET: Parabrisas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Parabrisas parabrisas = _UnityOfWork.Parabrisas.Get(id);
            if (parabrisas == null)
            {
                return HttpNotFound();
            }
            return View(parabrisas);
        }

        // POST: Parabrisas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ParabrisasId,NumSerie")] Parabrisas parabrisas)
        {
            if (ModelState.IsValid)
            {
                _UnityOfWork.StateModified(parabrisas);
                _UnityOfWork.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(parabrisas);
        }

        // GET: Parabrisas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Parabrisas parabrisas = _UnityOfWork.Parabrisas.Get(id);
            if (parabrisas == null)
            {
                return HttpNotFound();
            }
            return View(parabrisas);
        }

        // POST: Parabrisas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Parabrisas parabrisas = _UnityOfWork.Parabrisas.Get(id);
            _UnityOfWork.Parabrisas.Remove(parabrisas);
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
