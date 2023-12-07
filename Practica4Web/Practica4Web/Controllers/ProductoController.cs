using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Practica4Web.Entities;
using Practica4Web.Models;
using System.Reflection;

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
            var datos = _productoModel.Consultar();
            return View(datos);
        }


        [HttpGet]
        public IActionResult RegistrarProducto()
        {
            var ProductosPendientes = _productoModel.ConsultarProductosPendientes();

            var ProductosPendientesDropDown = new List<SelectListItem>
    {
        new SelectListItem { Value = "", Text = "Seleccione un producto", Selected = true }
    };

            foreach (var item in ProductosPendientes.Objetos)
            {
                ProductosPendientesDropDown.Add(new SelectListItem { Value = item.Id_Compra.ToString(), Text = item.Descripcion });
            }

            ViewBag.ProductosPendientesDropDown = ProductosPendientesDropDown;

            return View();
        }



        [HttpGet]
        public IActionResult ObtenerSaldoIdCompra(long Id_Compra)
        {
           
            var respuesta = _productoModel.ObtenerSaldoIdCompra(Id_Compra);

            if (respuesta.Objeto != null)
            {
               
                return Json(new { Saldo = respuesta.Objeto.Saldo });
            }
            else
            {
               
                return Json(new { Saldo = 0 }); 
            }
        }


        [HttpPost]
        public IActionResult RegistrarAbono(ProductoEnt entidad)
        {
            var resp = _productoModel.RegistrarAbono(entidad);
            if (resp > 1)
            {
                return RedirectToAction("RegistrarProducto", "Producto");
            }
            else
            {
                ViewBag.MensajePantalla = "No se logro registrar el abono";
                return View();

            }



        }







    }
}
