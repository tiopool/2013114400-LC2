using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using _2013114400_ENT.Entities;
using _2013114400_PER;
using _2013114400_ENT.IRepositories;

namespace _2013114400_WebApi.Controllers
{
    public class ParabrisasController : ApiController
    {
        //  private EnsambladoraDbContext db = new EnsambladoraDbContext();
        private readonly IUnityOfWork _UnityOfWork;

        public ParabrisasController(IUnityOfWork unityOfWork)
        {
            _UnityOfWork = unityOfWork;

        }
        /*
                // GET: api/Parabrisas
                public IQueryable<Parabrisas> GetParabrisas()
                {
                    return db.Parabrisas;
                }
        */

        [HttpGet]
        public IHttpActionResult Get()
        {
            //La capa de persistencia no debe ser modificada, porque es única para todo canal de atencion de la aplicacion
            //por lo tanto, a nivel de controlador se debe de hacer las modificaciones.
            var parabrisas = _UnityOfWork.Parabrisas.GetAll();

            if (parabrisas == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return Ok(parabrisas);
        }

        // GET: api/Parabrisas/5
        [ResponseType(typeof(Parabrisas))]
        public IHttpActionResult GetParabrisas(int id)
        {
            Parabrisas parabrisas = _UnityOfWork.Parabrisas.Get(id);
            if (parabrisas == null)
            {
                return NotFound();
            }

            return Ok(parabrisas);
        }

        // PUT: api/Parabrisas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutParabrisas(int id, Parabrisas parabrisas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != parabrisas.ParabrisasId)
            {
                return BadRequest();
            }

            _UnityOfWork.StateModified(parabrisas);

            try
            {
                _UnityOfWork.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParabrisasExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        /*     // POST: api/Parabrisas
             [ResponseType(typeof(Parabrisas))]
             public IHttpActionResult PostParabrisas(Parabrisas parabrisas)
             {
                 if (!ModelState.IsValid)
                 {
                     return BadRequest(ModelState);
                 }

                 _UnityOfWork.Parabrisas.Add(parabrisas);
                 _UnityOfWork.SaveChanges();

                 return CreatedAtRoute("DefaultApi", new { id = parabrisas.ParabrisasId }, parabrisas);
             }*/

        [HttpPost]
        public IHttpActionResult Create(Parabrisas parabrisas)
        {
            if (!ModelState.IsValid)
                return BadRequest();



            _UnityOfWork.Parabrisas.Add(parabrisas);

            try
            {
                _UnityOfWork.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ParabrisasExists(parabrisas.ParabrisasId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = parabrisas.ParabrisasId }, parabrisas);
        }


        // DELETE: api/Parabrisas/5
        [ResponseType(typeof(Parabrisas))]
        public IHttpActionResult DeleteParabrisas(int id)
        {
            Parabrisas parabrisas = _UnityOfWork.Parabrisas.Get(id);
            if (parabrisas == null)
            {
                return NotFound();
            }

            _UnityOfWork.Parabrisas.Remove(parabrisas);
            _UnityOfWork.SaveChanges();

            return Ok(parabrisas);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _UnityOfWork.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ParabrisasExists(int id)
        {
            return _UnityOfWork.Parabrisas.GetEntity().Count(e => e.ParabrisasId == id) > 0;
        }
    }
}