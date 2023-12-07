using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http.Headers;
using System.Net.Http;
using Practica4Web.Entities;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;

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


        public ProductoEntRespuesta? ConsultarProductosPendientes()
        {
            string url = "api/Producto/ConsultarProductosPendientes";
            var response = _httpClient.GetAsync(_urlApi + url).Result;
            return response.Content.ReadFromJsonAsync<ProductoEntRespuesta>().Result;
        }



        public ProductoEntRespuesta? ObtenerSaldoIdCompra(long Id_Compra)
        {
            string url = _urlApi + "api/Producto/ObtenerSaldoIdCompra?Id_Compra=" + Id_Compra;

            try
            {
                var resp = _httpClient.GetAsync(url).Result;

                if (resp.IsSuccessStatusCode)
                {
                    var productoEnt = resp.Content.ReadFromJsonAsync<ProductoEntRespuesta>().Result;
                    Console.WriteLine($"Respuesta de la API: {JsonConvert.SerializeObject(productoEnt)}");
                    return productoEnt;
                }
                else
                {
                    // Agrega un log para identificar posibles errores
                    Console.WriteLine($"Error en la solicitud: {resp.StatusCode}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                // Agrega un log para identificar posibles excepciones
                Console.WriteLine($"Excepción al realizar la solicitud: {ex.Message}");
                return null;
            }
        }


        public List<ProductoEnt> Consultar()
        {
            using (var http = new HttpClient())
            {
                var url = "https://localhost:7103/api/Producto/ConsultarProductos";
                var resp = http.GetAsync(url).Result;

                if (resp.IsSuccessStatusCode)
                    return resp.Content.ReadFromJsonAsync<List<ProductoEnt>>().Result;
                else
                    return new List<ProductoEnt>();
            }
        }


        public int RegistrarAbono(ProductoEnt entidad)
        {
            string url = _urlApi + "api/Producto/RegistrarAbono";
            JsonContent obj = JsonContent.Create(entidad);
            var resp = _httpClient.PostAsync(url, obj).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<int>().Result;
            else
                return 0;

        }



     

    }
}
