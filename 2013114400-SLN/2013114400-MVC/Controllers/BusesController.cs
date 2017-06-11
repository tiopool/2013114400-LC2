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
    public class BusesController : Controller
    {
        //private EnsambladoraDbContext db = new EnsambladoraDbContext();
        private readonly IUnityOfWork _UnityOfWork;

        public BusesController()
        {

        }
        public BusesController(IUnityOfWork unityOfWork)
        {
            _UnityOfWork = unityOfWork;
        }
            

        // GET: Buses
        public ActionResult Index()
        {
          // var carros = _UnityOfWork.Carros.GetEntity().Include(b => b.Ensambladora).Include(b => b.Propietario);

            return View(_UnityOfWork.Buses.GetAll());
        }

        // GET: Buses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bus bus = _UnityOfWork.Buses.Get(id);
            if (bus == null)
            {

                return HttpNotFound();
            }
            return View(bus);
        }

        // GET: Buses/Create
        public ActionResult Create()
        {
            ViewBag.EnsambladoraId = new SelectList(_UnityOfWork.Ensambladoras.GetEntity(), "EnsambladoraId", "EnsambladoraId");
            ViewBag.CarroId = new SelectList(_UnityOfWork.Propietarios.GetEntity(), "PropietarioId", "DNI");
            return View();
        }

        // POST: Buses/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CarroId,EnsambladoraId,TipoCarro,NumSerieChasis,NumSerieMotor,BusId,TipoBus")] Bus bus)
        {
            if (ModelState.IsValid)
            {
                _UnityOfWork.Carros.Add(bus);
                _UnityOfWork.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EnsambladoraId = new SelectList(_UnityOfWork.Ensambladoras.GetEntity(), "EnsambladoraId", "EnsambladoraId", bus.EnsambladoraId);
            ViewBag.CarroId = new SelectList(_UnityOfWork.Propietarios.GetEntity(), "PropietarioId", "DNI", bus.CarroId);
            return View(bus);
        }

        // GET: Buses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bus bus = _UnityOfWork.Buses.Get(id);
            if (bus == null)
            {
                return HttpNotFound();
            }
            ViewBag.EnsambladoraId = new SelectList(_UnityOfWork.Ensambladoras.GetEntity(), "EnsambladoraId", "EnsambladoraId", bus.EnsambladoraId);
            ViewBag.CarroId = new SelectList(_UnityOfWork.Propietarios.GetEntity(), "PropietarioId", "DNI", bus.CarroId);
            return View(bus);
        }

        // POST: Buses/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CarroId,EnsambladoraId,TipoCarro,NumSerieChasis,NumSerieMotor,BusId,TipoBus")] Bus bus)
        {
            if (ModelState.IsValid)
            {
                _UnityOfWork.StateModified(bus);
                _UnityOfWork.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EnsambladoraId = new SelectList(_UnityOfWork.Ensambladoras.GetEntity(), "EnsambladoraId", "EnsambladoraId", bus.EnsambladoraId);
            ViewBag.CarroId = new SelectList(_UnityOfWork.Propietarios.GetEntity(), "PropietarioId", "DNI", bus.CarroId);
            return View(bus);
        }

        // GET: Buses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bus bus = _UnityOfWork.Buses.Get(id);
            if (bus == null)
            {
                return HttpNotFound();
            }
            return View(bus);
        }

        // POST: Buses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bus bus = _UnityOfWork.Buses.Get(id);
            _UnityOfWork.Carros.Remove(bus);
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
