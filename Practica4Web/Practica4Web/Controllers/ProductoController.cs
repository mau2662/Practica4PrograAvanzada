using Microsoft.AspNetCore.Mvc;

namespace Practica4Web.Controllers
{
    public class ProductoController : Controller


    {
        [HttpGet]
        public IActionResult ConsultarProducto()
        {
            return View();
        }


        [HttpGet]
        public IActionResult RegistrarProducto()
        {
            return View();
        }
    }
}
