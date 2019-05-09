using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RecipesChile.Models;
using RecipesChile.Services;

namespace RecipesChile.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecetaCLController : ControllerBase
    {

        private readonly RecetaServices _recetaService;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:RecipesChile.Controllers.RecetaController"/> class.
        /// </summary>
        /// <param name="recetaService">Receta service.</param>
        public RecetaCLController(RecetaServices recetaService)
        {
            _recetaService = recetaService;
        }

        /// <summary>
        /// Get this instance.
        /// </summary>
        /// <returns>The get.</returns>
        [HttpGet]
        public ActionResult<ServiceResult> Get()
        {
            ServiceResult result = new ServiceResult();
            try
            {
                result.Success = true;
                result.StatusCode = 200;
                result.Message = "OK";
                result.Response = _recetaService.Get();
                result.TokenResponse = null;               
            }
            catch (MongoDB.Driver.MongoConnectionException ex)
            {
                result.Success = false;
                result.StatusCode = 406;
                result.Message = "Mongo Connection Exception";
                result.Response = ex.Message;
                result.TokenResponse = null;

            }
            catch (MongoDB.Driver.MongoExecutionTimeoutException ex)
            {
                result.Success = false;
                result.StatusCode = 408;
                result.Message = "Timeout Exception";
                result.Response = ex.Message;
                result.TokenResponse = null;

            }
            catch (MongoDB.Driver.MongoInternalException ex)
            {
                result.Success = false;
                result.StatusCode = 500;
                result.Message = "Internal mongo error";
                result.Response = ex.Message;
                result.TokenResponse = null;

            }
            catch (MongoDB.Driver.MongoException ex)
            {
                result.Success = false;
                result.StatusCode = -1;
                result.Message = ex.Message;
                result.Response = ex;
                result.TokenResponse = null;

            }
            catch (System.Exception ex)
            {
                result.Success = false;
                result.StatusCode = -100;
                result.Message = ex.Message;
                result.Response = ex;
                result.TokenResponse = null;

            }
            return result;
        }

        /// <summary>
        /// Get the specified id.
        /// </summary>
        /// <returns>The get.</returns>
        /// <param name="id">Identifier.</param>
        [HttpGet("{id:length(24)}", Name = "GetReceta")]
        public ActionResult<Receta> Get(string id)
        {
            var receta = _recetaService.Get(id);
            if (receta == null)
            {
                return NotFound();
            }
            return receta;
        }

        /// <summary>
        /// Create the specified receta.
        /// </summary>
        /// <returns>The create.</returns>
        /// <param name="receta">Receta.</param>
        [HttpPost]
        public ActionResult<ServiceResult> Create(Receta receta)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                if (ModelState.IsValid)
                {
                    if (!string.IsNullOrEmpty(receta.Id)                     
                      && !string.IsNullOrEmpty(receta.Nombre)
                      && !string.IsNullOrEmpty(receta.Pais))
                    {
                        _recetaService.Create(receta);
                        result.Success = true;
                        result.StatusCode = 200;
                        result.Message = "OK";
                        result.Response = receta;
                        result.TokenResponse = null;
                    }
                    else
                    {
                        result.Success = false;
                        result.StatusCode = 800;
                        result.Message = "Business Exception";
                        result.Response = "Business Exception";
                        result.TokenResponse = null;
                    }
                }


            }
            catch (MongoDB.Driver.MongoConnectionException ex)
            {
                result.Success = false;
                result.StatusCode = 406;
                result.Message = "Mongo Connection Exception";
                result.Response = ex.Message;
                result.TokenResponse = null;

            }
            catch (MongoDB.Driver.MongoExecutionTimeoutException ex)
            {
                result.Success = false;
                result.StatusCode = 408;
                result.Message = "Timeout Exception";
                result.Response = ex.Message;
                result.TokenResponse = null;

            }
            catch (MongoDB.Driver.MongoInternalException ex)
            {
                result.Success = false;
                result.StatusCode = 500;
                result.Message = "Internal mongo error";
                result.Response = ex.Message;
                result.TokenResponse = null;

            }
            catch (MongoDB.Driver.MongoException ex)
            {
                result.Success = false;
                result.StatusCode = -1;
                result.Message = ex.Message;
                result.Response = ex;
                result.TokenResponse = null;

            }

            return result;
        }

        /// <summary>
        /// Update the specified id and contIn.
        /// </summary>
        /// <returns>The update.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="recetaIn">Cont in.</param>
        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Receta recetaIn)
        {
            var receta = _recetaService.Get(id);

            if (receta == null)
            {
                return NotFound();
            }
            _recetaService.Update(id, recetaIn);

            return NoContent();
        }

        /// <summary>
        /// Delete the specified id.
        /// </summary>
        /// <returns>The delete.</returns>
        /// <param name="id">Identifier.</param>
        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var receta = _recetaService.Get(id);

            if (receta == null)
            {
                return NotFound();
            }

            _recetaService.Remove(receta.Id);
            return NoContent();
        }
    }
}
