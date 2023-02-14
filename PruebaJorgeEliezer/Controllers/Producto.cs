using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaJorgeEliezer.Models;
using PruebaJorgeEliezer.Models.Respuesta;
using PruebaJorgeEliezer.Models.Solicitudes;

namespace PruebaJorgeEliezer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                using (PruebaJorgeDibContext db = new PruebaJorgeDibContext())
                {
                    var result = (from p in db.Productos
                                  where p.Estado == true
                                  select new Producto
                                  {
                                      Id = p.Id,
                                      Descripcion = p.Descripcion,
                                      Precio = p.Precio,
                                      Existencia = p.Existencia,
                                      Estado = p.Estado
                                  }).ToList();
                    oRespuesta.Exito = 1;
                    oRespuesta.Data = result;
                }

            }catch(Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }
            return Ok(oRespuesta);
        }
        
        
        [HttpPost]
        public IActionResult Add(ProductoSolicitud producto)
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                using (PruebaJorgeDibContext db = new PruebaJorgeDibContext())
                {
                    Producto oProducto = new Producto();
                    oProducto.Descripcion = producto.Descripcion;
                    oProducto.Precio = producto.Precio;
                    oProducto.Existencia = true;
                    oProducto.Estado = true;
                    db.Productos.Add(oProducto);
                    db.SaveChanges();

                    Log oLog = new Log();
                    oLog.Descripcion = oProducto.Descripcion;
                    oLog.IdProducto = oProducto.Id;
                    oLog.Precio = oProducto.Precio;
                    oLog.Fecha = DateTime.Now;
                    oLog.Existencia = oProducto.Existencia;
                    oLog.Estatus ="Registro Creado";
                    db.Logs.Add(oLog);
                    db.SaveChanges();
                    oRespuesta.Exito = 1;
                }
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }
            return Ok(oRespuesta);
        }

        [HttpPut]
        public IActionResult Edit(ProductoSolicitud producto)
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                using (PruebaJorgeDibContext db = new PruebaJorgeDibContext())
                {
                    Producto oProducto = db.Productos.Find(producto.Id);
                    oProducto.Descripcion = producto.Descripcion;
                    oProducto.Precio = producto.Precio;
                    oProducto.Existencia = producto.Existencia;
                    db.Entry(oProducto).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    db.SaveChanges();

                    Log oLog = new Log();
                    oLog.Descripcion = producto.Descripcion;
                    oLog.IdProducto = producto.Id;
                    oLog.Precio = producto.Precio;
                    oLog.Fecha = DateTime.Now;
                    oLog.Existencia = producto.Existencia;
                    oLog.Estatus = "Registro Editado";
                    db.Logs.Add(oLog);
                    db.SaveChanges();
                    oRespuesta.Exito = 1;
                }
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }
            return Ok(oRespuesta);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                using (PruebaJorgeDibContext db = new PruebaJorgeDibContext())
                {
                    var oProducto = (from p in db.Productos
                                 where p.Id == id
                                 select p).FirstOrDefault();
                    oProducto.Estado = false;

                    db.Entry(oProducto).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    db.SaveChanges();

                    Log oLog = new Log();
                    oLog.Fecha = DateTime.Now;
                    oLog.Descripcion = oProducto.Descripcion;
                    oLog.IdProducto = oProducto.Id;
                    oLog.Precio = oProducto.Precio;
                    oLog.Estatus = "Registro Eliminado";
                    db.Logs.Add(oLog);
                    db.SaveChanges();
                    oRespuesta.Exito = 1;       
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
