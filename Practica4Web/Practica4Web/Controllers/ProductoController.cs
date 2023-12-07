using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Practica4Web.Entities;
using Practica4Web.Models;

namespace Practica4Web.Controllers
{
    public class ProductoController : Controller


    {


        private readonly IProductoModel _productoModel;
        public ProductoController(IProductoModel productoModel)
        {
            _productoModel = productoModel;
        }




        [HttpGet]
        public IActionResult ConsultarProducto()
        {
            return View();
        }


        [HttpGet]
        public IActionResult RegistrarProducto()
        {


            ViewBag.Productos = _productoModel.ConsultarProductos();


            return View();


        }



        [HttpGet]
        public IActionResult ObtenerSaldoIdCompra()
        {

            var entidad = new ProductoEnt();       
            var saldo = _productoModel.ObtenerSaldoIdCompra(entidad.Id_Compra);
            ViewBag.Productos = _productoModel.ConsultarProductos();

            return Json(saldo);

         

        }









    }
}
