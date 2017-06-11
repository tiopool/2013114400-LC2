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
    public class EnsambladorasController : Controller
    {
        //private EnsambladoraDbContext db = new EnsambladoraDbContext();

        private readonly IUnityOfWork _UnityOfWork;

        public EnsambladorasController(IUnityOfWork unityOfWork)
        {
            _UnityOfWork = unityOfWork;

        }

        // GET: Ensambladoras
        public ActionResult Index()
        {
            return View(_UnityOfWork.Ensambladoras.GetAll());
        }

        // GET: Ensambladoras/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ensambladora ensambladora = _UnityOfWork.Ensambladoras.Get(id);
            if (ensambladora == null)
            {
                return HttpNotFound();
            }
            return View(ensambladora);
        }

        // GET: Ensambladoras/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ensambladoras/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EnsambladoraId")] Ensambladora ensambladora)
        {
            if (ModelState.IsValid)
            {
                _UnityOfWork.Ensambladoras.Add(ensambladora);
                _UnityOfWork.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ensambladora);
        }

        // GET: Ensambladoras/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ensambladora ensambladora = _UnityOfWork.Ensambladoras.Get(id);
            if (ensambladora == null)
            {
                return HttpNotFound();
            }
            return View(ensambladora);
        }

        // POST: Ensambladoras/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EnsambladoraId")] Ensambladora ensambladora)
        {
            if (ModelState.IsValid)
            {
                _UnityOfWork.StateModified(ensambladora);
                _UnityOfWork.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ensambladora);
        }

        // GET: Ensambladoras/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ensambladora ensambladora = _UnityOfWork.Ensambladoras.Get(id);
            if (ensambladora == null)
            {
                return HttpNotFound();
            }
            return View(ensambladora);
        }

        // POST: Ensambladoras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ensambladora ensambladora = _UnityOfWork.Ensambladoras.Get(id);
            _UnityOfWork.Ensambladoras.Remove(ensambladora);
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
