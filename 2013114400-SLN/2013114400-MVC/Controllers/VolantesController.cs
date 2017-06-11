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
    public class VolantesController : Controller
    {
        //    private EnsambladoraDbContext db = new EnsambladoraDbContext();

        private readonly IUnityOfWork _UnityOfWork;

        public VolantesController()
        {

        }

        public VolantesController(IUnityOfWork unityOfWork)
        {
            _UnityOfWork = unityOfWork;
        }


        // GET: Volantes
        public ActionResult Index()
        {
            var volantes = _UnityOfWork.Volantes.GetEntity().Include(v => v.Carro);
            return View(volantes.ToList());
        }

        // GET: Volantes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Volante volante = _UnityOfWork.Volantes.Get(id);
            if (volante == null)
            {
                return HttpNotFound();
            }
            return View(volante);
        }

        // GET: Volantes/Create
        public ActionResult Create()
        {
            ViewBag.VolanteId = new SelectList(_UnityOfWork.Carros.GetEntity(), "CarroId", "NumSerieChasis");
            return View();
        }

        // POST: Volantes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VolanteId,NumSerie")] Volante volante)
        {
            if (ModelState.IsValid)
            {
                _UnityOfWork.Volantes.Add(volante);
                _UnityOfWork.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.VolanteId = new SelectList(_UnityOfWork.Carros.GetEntity(), "CarroId", "NumSerieChasis", volante.VolanteId);
            return View(volante);
        }

        // GET: Volantes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Volante volante = _UnityOfWork.Volantes.Get(id);
            if (volante == null)
            {
                return HttpNotFound();
            }
            ViewBag.VolanteId = new SelectList(_UnityOfWork.Carros.GetEntity(), "CarroId", "NumSerieChasis", volante.VolanteId);
            return View(volante);
        }

        // POST: Volantes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VolanteId,NumSerie")] Volante volante)
        {
            if (ModelState.IsValid)
            {
                _UnityOfWork.StateModified(volante);
                _UnityOfWork.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.VolanteId = new SelectList(_UnityOfWork.Carros.GetEntity(), "CarroId", "NumSerieChasis", volante.VolanteId);
            return View(volante);
        }

        // GET: Volantes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Volante volante = _UnityOfWork.Volantes.Get(id);
            if (volante == null)
            {
                return HttpNotFound();
            }
            return View(volante);
        }

        // POST: Volantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Volante volante = _UnityOfWork.Volantes.Get(id);
            _UnityOfWork.Volantes.Remove(volante);
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
