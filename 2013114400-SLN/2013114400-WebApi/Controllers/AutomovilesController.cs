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
    public class AutomovilesController : ApiController
    {
        // private EnsambladoraDbContext db = new EnsambladoraDbContext();
        private readonly IUnityOfWork _UnityOfWork;

        public AutomovilesController(IUnityOfWork unityOfWork)
        {
            _UnityOfWork = unityOfWork;

        }



        /*   // GET: api/Automoviles
           public IQueryable<Automovil> GetCarros()
           {
               return db.Carros;
           }
           */
        [HttpGet]
        public IHttpActionResult Get()
        {
            //La capa de persistencia no debe ser modificada, porque es única para todo canal de atencion de la aplicacion
            //por lo tanto, a nivel de controlador se debe de hacer las modificaciones.
            var automoviles = _UnityOfWork.Automoviles.GetAll();

            if (automoviles == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            //    var FloreriaDTO = new List<FloreriaDto>();

            //            foreach (var floreria in Floreria)
            //              FloreriaDTO.Add(Mapper.Map<Florerias, FloreriaDto>(floreria));

            return Ok(automoviles);
        }


        // GET: api/Automoviles/5
        [ResponseType(typeof(Automovil))]
        public IHttpActionResult GetAutomovil(int id)
        {
            Automovil automovil = _UnityOfWork.Automoviles.Get(id);
            if (automovil == null)
            {
                return NotFound();
            }

            return Ok(automovil);
        }
        

        // PUT: api/Automoviles/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAutomovil(int id, Automovil automovil)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != automovil.CarroId)
            {
                return BadRequest();
            }

            _UnityOfWork.StateModified(automovil);

            try
            {
                _UnityOfWork.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AutomovilExists(id))
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
                // POST: api/Automoviles
                [ResponseType(typeof(Automovil))]
                public IHttpActionResult PostAutomovil(Automovil automovil)
                {
                    if (!ModelState.IsValid)
                    {
                        return BadRequest(ModelState);
                    }

                    db.Carros.Add(automovil);

                    try
                    {
                        db.SaveChanges();
                    }
                    catch (DbUpdateException)
                    {
                        if (AutomovilExists(automovil.CarroId))
                        {
                            return Conflict();
                        }
                        else
                        {
                            throw;
                        }
                    }

                    return CreatedAtRoute("DefaultApi", new { id = automovil.CarroId }, automovil);
                }

            */

        [HttpPost]
        public IHttpActionResult Create(Automovil automovil)
        {
            if (!ModelState.IsValid)
                return BadRequest();

           
            _UnityOfWork.Automoviles.Add(automovil);

            try
            {
                _UnityOfWork.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (AutomovilExists(automovil.AutomovilId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = automovil.AutomovilId }, automovil);
        }

        // DELETE: api/Automoviles/5
        [ResponseType(typeof(Automovil))]
        public IHttpActionResult DeleteAutomovil(int id)
        {
            Automovil automovil = _UnityOfWork.Automoviles.Get(id);
            if (automovil == null)
            {
                return NotFound();
            }

            _UnityOfWork.Carros.Remove(automovil);
            _UnityOfWork.SaveChanges();

            return Ok(automovil);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _UnityOfWork.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AutomovilExists(int id)
        {
            return _UnityOfWork.Carros.GetEntity().Count(e => e.CarroId == id) > 0;
        }
    }
}