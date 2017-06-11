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
    public class AsientosController : Controller
    {
        //private EnsambladoraDbContext db = new EnsambladoraDbContext();
        private readonly IUnityOfWork _UnityOfWork;

        public AsientosController()
        {

        }
        public AsientosController(IUnityOfWork unityOfWork)
        {
            _UnityOfWork = unityOfWork;
        }




        // GET: Asientos
        public ActionResult Index()
        {
            var asientos = _UnityOfWork.Asientos.GetEntity().Include(a => a.Carro);
              return View(asientos.ToList());
           // return View(_UnityOfWork.Asientos.GetAll());
        }

        // GET: Asientos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asiento asiento = _UnityOfWork.Asientos.Get(id);
            if (asiento == null)
            {
                return HttpNotFound();
            }
            return View(asiento);
        }

        // GET: Asientos/Create
        public ActionResult Create()
        {
            ViewBag.CarroId = new SelectList(_UnityOfWork.Carros.GetEntity(), "CarroId", "NumSerieChasis");
            return View();
        }

        // POST: Asientos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AsientoId,NumSerie,CarroId")] Asiento asiento)
        {
            if (ModelState.IsValid)
            {
                _UnityOfWork.Asientos.Add(asiento);
                _UnityOfWork.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CarroId = new SelectList(_UnityOfWork.Carros.GetEntity(), "CarroId", "NumSerieChasis", asiento.CarroId);
            return View(asiento);
        }

        // GET: Asientos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asiento asiento = _UnityOfWork.Asientos.Get(id);
            if (asiento == null)
            {
                return HttpNotFound();
            }
            ViewBag.CarroId = new SelectList(_UnityOfWork.Carros.GetEntity(), "CarroId", "NumSerieChasis", asiento.CarroId);
            return View(asiento);
        }

        // POST: Asientos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AsientoId,NumSerie,CarroId")] Asiento asiento)
        {
            if (ModelState.IsValid)
            {
                _UnityOfWork.StateModified(asiento);
                _UnityOfWork.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CarroId = new SelectList(_UnityOfWork.Carros.GetEntity(), "CarroId", "NumSerieChasis", asiento.CarroId);
            return View(asiento);
        }

        // GET: Asientos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asiento asiento = _UnityOfWork.Asientos.Get(id);
            if (asiento == null)
            {
                return HttpNotFound();
            }
            return View(asiento);
        }

        // POST: Asientos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Asiento asiento = _UnityOfWork.Asientos.Get(id);
            _UnityOfWork.Asientos.Remove(asiento);
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
