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
    public class AsientosController : ApiController
    {
        //   private EnsambladoraDbContext db = new EnsambladoraDbContext();
        private readonly IUnityOfWork _UnityOfWork;

        public AsientosController(IUnityOfWork unityOfWork)
        {
            _UnityOfWork = unityOfWork;

        }
        /*      // GET: api/Asientos
              public IQueryable<Asiento> GetAsientos()
          {
              return db.Asientos;
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

            //    var FloreriaDTO = new List<FloreriaDto>();

            //            foreach (var floreria in Floreria)
            //              FloreriaDTO.Add(Mapper.Map<Florerias, FloreriaDto>(floreria));

            return Ok(asientos);
        }


        // GET: api/Asientos/5
        [ResponseType(typeof(Asiento))]
        public IHttpActionResult GetAsiento(int id)
        {
            Asiento asiento = _UnityOfWork.Asientos.Get(id);
            if (asiento == null)
            {
                return NotFound();
            }

            return Ok(asiento);
        }

        // PUT: api/Asientos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAsiento(int id, Asiento asiento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != asiento.AsientoId)
            {
                return BadRequest();
            }

            _UnityOfWork.StateModified(asiento);

            try
            {
                _UnityOfWork.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AsientoExists(id))
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
        /*
                // POST: api/Asientos
                [ResponseType(typeof(Asiento))]
                public IHttpActionResult PostAsiento(Asiento asiento)
                {
                    if (!ModelState.IsValid)
                    {
                        return BadRequest(ModelState);
                    }

                    _UnityOfWork.Asientos.Add(asiento);
                    _UnityOfWork.SaveChanges();

                    return CreatedAtRoute("DefaultApi", new { id = asiento.AsientoId }, asiento);
                }
            */

        [HttpPost]
        public IHttpActionResult Create(Asiento asientos)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            //var floreria = Mapper.Map<FloreriaDto, Florerias>(floreriaDTO);

            _UnityOfWork.Asientos.Add(asientos);

            try
            {
                _UnityOfWork.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (AsientoExists(asientos.AsientoId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = asientos.AsientoId }, asientos);
        }

        // DELETE: api/Asientos/5
        [ResponseType(typeof(Asiento))]
        public IHttpActionResult DeleteAsiento(int id)
        {
            Asiento asiento = _UnityOfWork.Asientos.Get(id);
            if (asiento == null)
            {
                return NotFound();
            }

            _UnityOfWork.Asientos.Remove(asiento);
            _UnityOfWork.SaveChanges();

            return Ok(asiento);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _UnityOfWork.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AsientoExists(int id)
        {
            return _UnityOfWork.Asientos.GetEntity().Count(e => e.AsientoId == id) > 0;
        }
    }
}