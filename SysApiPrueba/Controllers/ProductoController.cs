using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using SysApiPrueba.Models;

using Microsoft.AspNetCore.Cors;

namespace SysApiPrueba.Controllers
{

    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {

        public readonly DbapiContext _dbcontext;

        public ProductoController(DbapiContext _context) {
            _dbcontext = _context;

        }

        [HttpGet]
        [Route("Lista")]
        public IActionResult Lista()
        {
            List<Producto> lista = new List<Producto>();

            try
            {
                lista = _dbcontext.Producto.Include(c => c.objCategoria).ToList();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = lista });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = lista });
            }
        }


        [HttpGet]
        [Route("Obtener/{IdProducto}")]
        public IActionResult Obtener(int IdProducto)
        {
            Producto objProducto = _dbcontext.Producto.Find(IdProducto);

            if (objProducto == null)
            {
                return BadRequest("Producto no encontrado");
            }

            try
            {
                objProducto = _dbcontext.Producto.Include(c => c.objCategoria).Where(p => p.IdProducto == IdProducto).FirstOrDefault();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = objProducto });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = objProducto });
            }
        }


        [HttpPost]
        [Route("Guardar")]
        public IActionResult Guardar([FromBody] Producto objeto)
        {
            try
            {
                _dbcontext.Producto.Add(objeto);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }


        [HttpPut]
        [Route("Editar")]
        public IActionResult Editar([FromBody] Producto objeto)
        {

            Producto objProducto = _dbcontext.Producto.Find(objeto.IdProducto);

            if (objProducto == null)
            {
                return BadRequest("Producto no encontrado");
            }

            try
            {
                objProducto.CodigoBarra = objeto.CodigoBarra is null ? objProducto.CodigoBarra : objeto.CodigoBarra;
                objProducto.Descripcion = objeto.Descripcion is null ? objProducto.Descripcion : objeto.Descripcion;
                objProducto.Marca = objeto.Marca is null ? objProducto.Marca : objeto.Marca;
                objProducto.IdCategoria = objeto.IdCategoria is null ? objProducto.IdCategoria : objeto.IdCategoria;
                objProducto.Precio = objeto.Precio is null ? objProducto.Precio : objeto.Precio;

                _dbcontext.Producto.Update(objProducto);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }



        [HttpDelete]
        [Route("Eliminar/{IdProducto}")]
        public IActionResult Eliminar(int IdProducto)
        {



            Producto objProducto = _dbcontext.Producto.Find(IdProducto);

            if (objProducto == null)
            {
                return BadRequest("Producto no encontrado");
            }

            try
            {
         

                _dbcontext.Producto.Remove(objProducto);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }

        }









    }
}
