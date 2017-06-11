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
    public class CinturonesController : ApiController
    {
        //  private EnsambladoraDbContext db = new EnsambladoraDbContext();
        private readonly IUnityOfWork _UnityOfWork;

        public CinturonesController(IUnityOfWork unityOfWork)
        {
            _UnityOfWork = unityOfWork;

        }


     /*   // GET: api/Cinturones
        public IQueryable<Cinturon> GetCinturones()
        {
            return _UnityOfWork.Cinturones;
        }
        */
            
        [HttpGet]
        public IHttpActionResult Get()
        {
            //La capa de persistencia no debe ser modificada, porque es única para todo canal de atencion de la aplicacion
            //por lo tanto, a nivel de controlador se debe de hacer las modificaciones.
            var asientos = _UnityOfWork.Asientos.GetAll();

            if (asientos == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);   

            return Ok(asientos);
        }


        // GET: api/Cinturones/5
        [ResponseType(typeof(Cinturon))]
        public IHttpActionResult GetCinturon(int id)
        {
            Cinturon cinturon = _UnityOfWork.Cinturones.Get(id);
            if (cinturon == null)
            {
                return NotFound();
            }

            return Ok(cinturon);
        }

        // PUT: api/Cinturones/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCinturon(int id, Cinturon cinturon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cinturon.CinturonId)
            {
                return BadRequest();
            }

            _UnityOfWork.StateModified(cinturon);

            try
            {
                _UnityOfWork.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CinturonExists(id))
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


        [HttpPost]
        public IHttpActionResult Create(Cinturon cinturones)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            _UnityOfWork.Cinturones.Add(cinturones);

            try
            {
                _UnityOfWork.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CinturonExists(cinturones.CinturonId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = cinturones.CinturonId }, cinturones);
        }


        // DELETE: api/Cinturones/5
        [ResponseType(typeof(Cinturon))]
        public IHttpActionResult DeleteCinturon(int id)
        {
            Cinturon cinturon = _UnityOfWork.Cinturones.Get(id);
            if (cinturon == null)
            {
                return NotFound();
            }

            _UnityOfWork.Cinturones.Remove(cinturon);
            _UnityOfWork.SaveChanges();

            return Ok(cinturon);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _UnityOfWork.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CinturonExists(int id)
        {
            return _UnityOfWork.Cinturones.GetEntity().Count(e => e.CinturonId == id) > 0;
        }
    }
}