using Practica4Api.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace Practica4Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {


        private readonly IConfiguration _configuration;
        private string _connection;

        public ProductoController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = _configuration.GetConnectionString("DefaultConnection");
        }




        [HttpGet]
        [Route("ConsultarProductos")]
        public IActionResult ConsultarProductos()
        {
            try
            {
                using (var context = new SqlConnection(_connection))
                {
                    var datos = context.Query<SelectListItem>("SPConsultarProductos",
                        new { },
                        commandType: CommandType.StoredProcedure).ToList();

                    return Ok(datos);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




        [HttpGet]
        [Route("ObtenerSaldoIdCompra")]
        public IActionResult ObtenerSaldoIdCompra(long Id_Compra) 
        {
            try
            {
                using (var context = new SqlConnection(_connection))
                {
                    var datos = context.Query<ProductoEnt>("SPObtenerSaldoIdCompra",
                        new { Id_Compra },
                        commandType: CommandType.StoredProcedure).FirstOrDefault();

                    return Ok(datos);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }







    }
}
