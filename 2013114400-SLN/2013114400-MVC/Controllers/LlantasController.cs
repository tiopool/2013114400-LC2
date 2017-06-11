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
    public class LlantasController : Controller
    {
       // private EnsambladoraDbContext db = new EnsambladoraDbContext();

        private readonly IUnityOfWork _UnityOfWork;

        public LlantasController(IUnityOfWork unityOfWork)
        {
            _UnityOfWork = unityOfWork;

        }

        // GET: Llantas
        public ActionResult Index()
        {
            var llantas = _UnityOfWork.Llantas.GetEntity().Include(l => l.Carros);
            return View(llantas.ToList());
        }

        // GET: Llantas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Llanta llanta = _UnityOfWork.Llantas.Get(id);
            if (llanta == null)
            {
                return HttpNotFound();
            }
            return View(llanta);
        }

        // GET: Llantas/Create
        public ActionResult Create()
        {
            ViewBag.CarroId = new SelectList(_UnityOfWork.Carros.GetEntity(), "CarroId", "NumSerieChasis");
            return View();
        }

        // POST: Llantas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LlantaId,NumSerie,CarroId")] Llanta llanta)
        {
            if (ModelState.IsValid)
            {
                _UnityOfWork.Llantas.Add(llanta);
                _UnityOfWork.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CarroId = new SelectList(_UnityOfWork.Carros.GetEntity(), "CarroId", "NumSerieChasis", llanta.CarroId);
            return View(llanta);
        }

        // GET: Llantas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Llanta llanta = _UnityOfWork.Llantas.Get(id);
            if (llanta == null)
            {
                return HttpNotFound();
            }
            ViewBag.CarroId = new SelectList(_UnityOfWork.Carros.GetEntity(), "CarroId", "NumSerieChasis", llanta.CarroId);
            return View(llanta);
        }

        // POST: Llantas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LlantaId,NumSerie,CarroId")] Llanta llanta)
        {
            if (ModelState.IsValid)
            {
                _UnityOfWork.StateModified(llanta);
                _UnityOfWork.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CarroId = new SelectList(_UnityOfWork.Carros.GetEntity(), "CarroId", "NumSerieChasis", llanta.CarroId);
            return View(llanta);
        }

        // GET: Llantas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Llanta llanta = _UnityOfWork.Llantas.Get(id);
            if (llanta == null)
            {
                return HttpNotFound();
            }
            return View(llanta);
        }

        // POST: Llantas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Llanta llanta = _UnityOfWork.Llantas.Get(id);
            _UnityOfWork.Llantas.Remove(llanta);
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
