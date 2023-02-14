using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaJorgeEliezer.Models.Respuesta;
using PruebaJorgeEliezer.Models;

namespace PruebaJorgeEliezer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetLogs()
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                using (PruebaJorgeDibContext db = new PruebaJorgeDibContext())
                {
                    var result = (from l in db.Logs
                                  join p in db.Productos
                                  on l.IdProducto equals p.Id
                                  select new Log
                                  {
                                      Id = l.Id,
                                      Descripcion = l.Descripcion,
                                      Precio = l.Precio,
                                      Existencia = l.Existencia,
                                      IdProducto = l.IdProducto,
                                      Estatus = l.Estatus,
                                  }).ToList();
                    oRespuesta.Exito = 1;
                    oRespuesta.Data = result;
                }
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }
            return Ok(oRespuesta);
        }
    }
}
