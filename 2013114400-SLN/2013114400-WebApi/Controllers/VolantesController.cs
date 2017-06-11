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
    public class VolantesController : ApiController
    {
        //  private EnsambladoraDbContext db = new EnsambladoraDbContext();
        private readonly IUnityOfWork _UnityOfWork;

        public VolantesController(IUnityOfWork unityOfWork)
        {
            _UnityOfWork = unityOfWork;

        }


        /*   // GET: api/Volantes
           public IQueryable<Volante> GetVolantes()
           {
               return db.Volantes;
           }*/
        [HttpGet]
        public IHttpActionResult Get()
        {

            var volantes = _UnityOfWork.Volantes.GetAll();

            if (volantes == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return Ok(volantes);
        }

        // GET: api/Volantes/5
        [ResponseType(typeof(Volante))]
        public IHttpActionResult GetVolante(int id)
        {
            Volante volante = _UnityOfWork.Volantes.Get(id);
            if (volante == null)
            {
                return NotFound();
            }

            return Ok(volante);
        }

        // PUT: api/Volantes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVolante(int id, Volante volante)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != volante.VolanteId)
            {
                return BadRequest();
            }

            _UnityOfWork.StateModified(volante);

            try
            {
                _UnityOfWork.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VolanteExists(id))
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

        /*  // POST: api/Volantes
          [ResponseType(typeof(Volante))]
          public IHttpActionResult PostVolante(Volante volante)
          {
              if (!ModelState.IsValid)
              {
                  return BadRequest(ModelState);
              }

              _UnityOfWork.Volantes.Add(volante);

              try
              {
                  _UnityOfWork.SaveChanges();
              }
              catch (DbUpdateException)
              {
                  if (VolanteExists(volante.VolanteId))
                  {
                      return Conflict();
                  }
                  else
                  {
                      throw;
                  }
              }

              return CreatedAtRoute("DefaultApi", new { id = volante.VolanteId }, volante);
          }*/

        [HttpPost]
        public IHttpActionResult Create(Volante volantes)
        {
            if (!ModelState.IsValid)
                return BadRequest();



            _UnityOfWork.Volantes.Add(volantes);

            try
            {
                _UnityOfWork.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (VolanteExists(volantes.VolanteId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = volantes.VolanteId }, volantes);
        }

        // DELETE: api/Volantes/5
        [ResponseType(typeof(Volante))]
        public IHttpActionResult DeleteVolante(int id)
        {
            Volante volante = _UnityOfWork.Volantes.Get(id);
            if (volante == null)
            {
                return NotFound();
            }

            _UnityOfWork.Volantes.Remove(volante);
            _UnityOfWork.SaveChanges();

            return Ok(volante);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _UnityOfWork.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VolanteExists(int id)
        {
            return _UnityOfWork.Volantes.GetEntity().Count(e => e.VolanteId == id) > 0;
        }
    }
}