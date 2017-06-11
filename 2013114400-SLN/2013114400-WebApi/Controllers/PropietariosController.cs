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
    public class PropietariosController : ApiController
    {
        //  private EnsambladoraDbContext db = new EnsambladoraDbContext();
        private readonly IUnityOfWork _UnityOfWork;

        public PropietariosController(IUnityOfWork unityOfWork)
        {
            _UnityOfWork = unityOfWork;

        }

        /*   // GET: api/Propietarios
           public IQueryable<Propietario> GetPropietarios()
           {
               return db.Propietarios;
           }*/

        [HttpGet]
        public IHttpActionResult Get()
        {
       
            var propietario = _UnityOfWork.Propietarios.GetAll();

            if (propietario == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return Ok(propietario);
        }

        // GET: api/Propietarios/5
        [ResponseType(typeof(Propietario))]
        public IHttpActionResult GetPropietario(int id)
        {
            Propietario propietario = _UnityOfWork.Propietarios.Get(id);
            if (propietario == null)
            {
                return NotFound();
            }

            return Ok(propietario);
        }

        // PUT: api/Propietarios/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPropietario(int id, Propietario propietario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != propietario.PropietarioId)
            {
                return BadRequest();
            }

            _UnityOfWork.StateModified(propietario);

            try
            {
                _UnityOfWork.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PropietarioExists(id))
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
        public IHttpActionResult Create(Propietario propietario)
        {
            if (!ModelState.IsValid)
                return BadRequest();



            _UnityOfWork.Propietarios.Add(propietario);

            try
            {
                _UnityOfWork.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PropietarioExists(propietario.PropietarioId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = propietario.PropietarioId }, propietario);
        }


        // DELETE: api/Propietarios/5
        [ResponseType(typeof(Propietario))]
        public IHttpActionResult DeletePropietario(int id)
        {
            Propietario propietario = _UnityOfWork.Propietarios.Get(id);
            if (propietario == null)
            {
                return NotFound();
            }

            _UnityOfWork.Propietarios.Remove(propietario);
            _UnityOfWork.SaveChanges();

            return Ok(propietario);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _UnityOfWork.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PropietarioExists(int id)
        {
            return _UnityOfWork.Propietarios.GetEntity().Count(e => e.PropietarioId == id) > 0;
        }
    }
}