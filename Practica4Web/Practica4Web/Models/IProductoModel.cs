using Microsoft.AspNetCore.Mvc.Rendering;
using Practica4Web.Entities;

namespace Practica4Web.Models
{
    public interface IProductoModel
    {
        public List<SelectListItem>? ConsultarProductos();



        public ProductoEnt? ObtenerSaldoIdCompra(long Id_Compra);



    }
}
