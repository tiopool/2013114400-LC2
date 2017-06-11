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
    public class AutomovilesController : Controller
    {
        //private EnsambladoraDbContext db = new EnsambladoraDbContext();
        private readonly IUnityOfWork _UnityOfWork;

        public AutomovilesController()
        {

        }
        public AutomovilesController(IUnityOfWork unityOfWork)
        {
            _UnityOfWork = unityOfWork;
        }

        // GET: Automoviles
        public ActionResult Index()
        {
          // var carros = _UnityOfWork.Carros.GetEntity().Include(a => a.Ensambladora).Include(a => a.Propietario);
            return View(_UnityOfWork.Automoviles.GetAll());
        }

        // GET: Automoviles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Automovil automovil = _UnityOfWork.Automoviles.Get(id);
            if (automovil == null)
            {
                return HttpNotFound();
            }
            return View(automovil);
        }

        // GET: Automoviles/Create
        public ActionResult Create()
        {
            ViewBag.EnsambladoraId = new SelectList(_UnityOfWork.Ensambladoras.GetEntity(), "EnsambladoraId", "EnsambladoraId");
            ViewBag.CarroId = new SelectList(_UnityOfWork.Propietarios.GetEntity(), "PropietarioId", "DNI");
            return View();
        }

        // POST: Automoviles/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CarroId,EnsambladoraId,TipoCarro,NumSerieChasis,NumSerieMotor,AutomovilId,TipoAuto")] Automovil automovil)
        {
            if (ModelState.IsValid)
            {
                _UnityOfWork.Carros.Add(automovil);
                _UnityOfWork.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EnsambladoraId = new SelectList(_UnityOfWork.Ensambladoras.GetEntity(), "EnsambladoraId", "EnsambladoraId", automovil.EnsambladoraId);
            ViewBag.CarroId = new SelectList(_UnityOfWork.Propietarios.GetEntity(), "PropietarioId", "DNI", automovil.CarroId);
            return View(automovil);
        }

        // GET: Automoviles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Automovil automovil = _UnityOfWork.Automoviles.Get(id);
            if (automovil == null)
            {
                return HttpNotFound();
            }
            ViewBag.EnsambladoraId = new SelectList(_UnityOfWork.Ensambladoras.GetEntity(), "EnsambladoraId", "EnsambladoraId", automovil.EnsambladoraId);
            ViewBag.CarroId = new SelectList(_UnityOfWork.Propietarios.GetEntity(), "PropietarioId", "DNI", automovil.CarroId);
            return View(automovil);
        }

        // POST: Automoviles/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CarroId,EnsambladoraId,TipoCarro,NumSerieChasis,NumSerieMotor,AutomovilId,TipoAuto")] Automovil automovil)
        {
            if (ModelState.IsValid)
            {
                _UnityOfWork.StateModified(automovil);
                _UnityOfWork.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EnsambladoraId = new SelectList(_UnityOfWork.Ensambladoras.GetEntity(), "EnsambladoraId", "EnsambladoraId", automovil.EnsambladoraId);
            ViewBag.CarroId = new SelectList(_UnityOfWork.Propietarios.GetEntity(), "PropietarioId", "DNI", automovil.CarroId);
            return View(automovil);
        }

        // GET: Automoviles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Automovil automovil = _UnityOfWork.Automoviles.Get(id);
            if (automovil == null)
            {
                return HttpNotFound();
            }
            return View(automovil);
        }

        // POST: Automoviles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Automovil automovil = _UnityOfWork.Automoviles.Get(id);
            _UnityOfWork.Carros.Remove(automovil);
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
