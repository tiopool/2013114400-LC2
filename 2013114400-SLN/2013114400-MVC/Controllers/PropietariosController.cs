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
    public class PropietariosController : Controller
    {
       //  private EnsambladoraDbContext db = new EnsambladoraDbContext();
          private readonly IUnityOfWork _UnityOfWork;

       public PropietariosController()
        {

        }

        public PropietariosController(IUnityOfWork unityOfWork)
        {
            _UnityOfWork = unityOfWork;
        }

    

        // GET: Propietarios
        public ActionResult Index()
        {
            //   return View(db.Propietarios.ToList());
              return View(_UnityOfWork.Propietarios.GetAll());
        }

        // GET: Propietarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //   Propietario propietario = db.Propietarios.Find(id);
             Propietario propietario = _UnityOfWork.Propietarios.Get(id);
            if (propietario == null)
            {
                return HttpNotFound();
            }
            return View(propietario);
        }

        // GET: Propietarios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Propietarios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PropietarioId,DNI,Nombres,Apellidos,LicenciaConducir")] Propietario propietario)
        {
            if (ModelState.IsValid)
            {
                // db.Propietarios.Add(propietario);
                 _UnityOfWork.Propietarios.Add(propietario);

                //  db.SaveChanges();
                  _UnityOfWork.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(propietario);
        }

        // GET: Propietarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //  Propietario propietario = db.Propietarios.Find(id);
            Propietario propietario = _UnityOfWork.Propietarios.Get(id);

            if (propietario == null)
            {
                return HttpNotFound();
            }
            return View(propietario);
        }

        // POST: Propietarios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PropietarioId,DNI,Nombres,Apellidos,LicenciaConducir")] Propietario propietario)
        {
            if (ModelState.IsValid)
            {
                //   db.Entry(propietario).State = EntityState.Modified;
                  _UnityOfWork.StateModified(propietario);

                //  db.SaveChanges();
                 _UnityOfWork.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(propietario);
        }

        // GET: Propietarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // Propietario propietario = db.Propietarios.Find(id);
            Propietario propietario = _UnityOfWork.Propietarios.Get(id);
            if (propietario == null)
            {
                return HttpNotFound();
            }
            return View(propietario);
        }

        // POST: Propietarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //   Propietario propietario = db.Propietarios.Find(id);
            Propietario propietario = _UnityOfWork.Propietarios.Get(id);
            //   db.Propietarios.Remove(propietario);
            _UnityOfWork.Propietarios.Remove(propietario);
            //   db.SaveChanges();
            _UnityOfWork.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //   db.Dispose();
                _UnityOfWork.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
