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
    public class LlantasController : ApiController
    {
        //private EnsambladoraDbContext db = new EnsambladoraDbContext();
        private readonly IUnityOfWork _UnityOfWork;

        public LlantasController(IUnityOfWork unityOfWork)
        {
            _UnityOfWork = unityOfWork;

        }

        /*
                // GET: api/Llantas
                public IQueryable<Llanta> GetLlantas()
                {
                    return db.Llantas;
                }
                */
        [HttpGet]
        public IHttpActionResult Get()
        {
            //La capa de persistencia no debe ser modificada, porque es única para todo canal de atencion de la aplicacion
            //por lo tanto, a nivel de controlador se debe de hacer las modificaciones.
            var llantas = _UnityOfWork.Llantas.GetAll();

            if (llantas == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return Ok(llantas);
        }

        // GET: api/Llantas/5
        [ResponseType(typeof(Llanta))]
        public IHttpActionResult GetLlanta(int id)
        {
            Llanta llanta = _UnityOfWork.Llantas.Get(id);
            if (llanta == null)
            {
                return NotFound();
            }

            return Ok(llanta);
        }

        // PUT: api/Llantas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLlanta(int id, Llanta llanta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != llanta.LlantaId)
            {
                return BadRequest();
            }

            _UnityOfWork.StateModified(llanta);

            try
            {
                _UnityOfWork.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LlantaExists(id))
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

        /* // POST: api/Llantas
         [ResponseType(typeof(Llanta))]
         public IHttpActionResult PostLlanta(Llanta llanta)
         {
             if (!ModelState.IsValid)
             {
                 return BadRequest(ModelState);
             }

             _UnityOfWork.Llantas.Add(llanta);
             _UnityOfWork.SaveChanges();

             return CreatedAtRoute("DefaultApi", new { id = llanta.LlantaId }, llanta);
         }

     */
        [HttpPost]
        public IHttpActionResult Create(Llanta llantas)
        {
            if (!ModelState.IsValid)
                return BadRequest();

           

            _UnityOfWork.Llantas.Add(llantas);

            try
            {
                _UnityOfWork.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (LlantaExists(llantas.LlantaId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = llantas.LlantaId }, llantas);
        }


        // DELETE: api/Llantas/5
        [ResponseType(typeof(Llanta))]
        public IHttpActionResult DeleteLlanta(int id)
        {
            Llanta llanta = _UnityOfWork.Llantas.Get(id);
            if (llanta == null)
            {
                return NotFound();
            }

            _UnityOfWork.Llantas.Remove(llanta);
            _UnityOfWork.SaveChanges();

            return Ok(llanta);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _UnityOfWork.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LlantaExists(int id)
        {
            return _UnityOfWork.Llantas.GetEntity().Count(e => e.LlantaId == id) > 0;
        }
    }
}