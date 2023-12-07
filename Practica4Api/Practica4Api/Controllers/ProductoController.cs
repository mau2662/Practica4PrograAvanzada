using Practica4Api.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Microsoft.AspNetCore.Authorization;

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





        [HttpGet("ConsultarProductos")]
        public IActionResult ConsultarProductos()
        {
            try
            {
                using (var connection = new SqlConnection(_connection))
                {
                    connection.Open();

                    var datos = connection.Query<ProductoEnt>(
                        "ConsultarProductos",
                        commandType: CommandType.StoredProcedure
                    ).ToList();

                    return Ok(datos);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpGet]
        [Route("ConsultarProductosPendientes")]
      
        public IActionResult ConsultarProductosPendientes()
        {
            var resultado = new List<ProductoEnt>();
            var respuesta = new ProductoEntRespuesta();

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    resultado = connection.Query<ProductoEnt>("SPConsultarProductos",
                        new { },
                        commandType: System.Data.CommandType.StoredProcedure).ToList();

                    if (resultado.Count == 0)
                    {
                        respuesta.Codigo = 2;
                        respuesta.Mensaje = "No se encontraron Productos Pendientes";
                        return Ok(respuesta);
                    }

                    respuesta.Codigo = 1;
                    respuesta.Mensaje = "Información consultada correctamente";
                    respuesta.Objetos = resultado;
                    return Ok(respuesta);
                }
            }
            catch (Exception)
            {
                respuesta.Codigo = 3;
                respuesta.Mensaje = "Se presentó un inconveniente.";
                return Ok(respuesta);
            }
        }
                         


        //ObtenerSaldoIdCompra

        [HttpGet]
        [Route("ObtenerSaldoIdCompra")]
        public IActionResult ObtenerSaldoIdCompra(long Id_Compra)
        {
            var resultado = new ProductoEnt();
            var respuesta = new ProductoEntRespuesta();
            //long Id_Compra = Id_Compra;

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    resultado = connection.Query<ProductoEnt>("[dbo].[SPObtenerSaldoIdCompra]",
                        new { Id_Compra },
                        commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();

                    if (resultado == null)
                    {
                        respuesta.Codigo = 2;
                        respuesta.Mensaje = "No se encontró la información de los Productos";
                        return Ok(respuesta);
                    }

                    respuesta.Codigo = 1;
                    respuesta.Mensaje = "Información consultada correctamente";
                    respuesta.Objeto = resultado;
                    return Ok(respuesta);
                }
            }
            catch (Exception)
            {
                respuesta.Codigo = 3;
                respuesta.Mensaje = "Se presentó un incoveninete.";
                respuesta.Objeto = resultado;
                return Ok(respuesta);
            }
        }

        //Registrar Abono

        [HttpPost]    
        [Route("RegistrarAbono")]

        public IActionResult RegistrarAbono(ProductoEnt entidad)
        {
            try
            {


                using (var context = new SqlConnection(_connection))

                {
                    var datos = context.Execute("SPRegistrarAbono", new
                    {
                        entidad.Id_Compra,
                        entidad.Monto,
                        

                    }, commandType: CommandType.StoredProcedure);
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
