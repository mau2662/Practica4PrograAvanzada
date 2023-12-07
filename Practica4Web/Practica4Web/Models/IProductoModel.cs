using Microsoft.AspNetCore.Mvc.Rendering;
using Practica4Web.Entities;

namespace Practica4Web.Models
{
    public interface IProductoModel
    {
        public ProductoEntRespuesta? ConsultarProductosPendientes();


        public ProductoEntRespuesta? ObtenerSaldoIdCompra(long Id_Compra);

        public List<ProductoEnt> Consultar();

        public int RegistrarAbono(ProductoEnt entidad);


    }
}
