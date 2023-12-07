using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http.Headers;
using System.Net.Http;
using Practica4Web.Entities;

namespace Practica4Web.Models
{
    public class ProductoModel : IProductoModel
    {



        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private string _urlApi;
        private readonly IHttpContextAccessor _HttpContextAccessor;


        public ProductoModel(HttpClient httpClient, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {

            _httpClient = httpClient;
            _configuration = configuration;
            _urlApi = _configuration.GetSection("Llaves:urlApi").Value;
            _HttpContextAccessor = httpContextAccessor;
        }





        public List<SelectListItem>? ConsultarProductos()
        {
            string url = _urlApi + "api/Producto/ConsultarProductos";
            //string token = _HttpContextAccessor.HttpContext.Session.GetString("TokenUsuario");
            //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var resp = _httpClient.GetAsync(url).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<List<SelectListItem>>().Result;
            else
                return null;
        }




        public ProductoEnt? ObtenerSaldoIdCompra(long Id_Compra)
        {
            string url = _urlApi + "api/Producto/ObtenerSaldoIdCompra?Id_Compra=" + Id_Compra;
            var resp = _httpClient.GetAsync(url).Result;

            if (resp.IsSuccessStatusCode)
            {
                return resp.Content.ReadFromJsonAsync<ProductoEnt>().Result;
            }
            else
            {
                return null;
            }
        }






    }
}
