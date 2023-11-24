using System;
using System.Collections.Specialized;
using Cai2023Entidades;
using Cai2023Datos.Utilidades;
using Newtonsoft.Json;

namespace Cai2023Datos
{
    public class VentaDatos
    {
        public void AgregarVenta(Guid idCliente,Guid idProducto,Guid IdVendedor,int Cantidad)
        {
            Dictionary<String, String> map = new Dictionary<String, String>();
            map.Add("idCliente", idCliente.ToString());
            map.Add("idVendedor", IdVendedor.ToString());
            map.Add("idProducto", idProducto.ToString());
            map.Add("Cantidad", Cantidad.ToString());

            var jsonRequest = JsonConvert.SerializeObject(map);

            HttpResponseMessage response = WebHelper.Patch("Venta/AgregarVenta", jsonRequest);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Verifique los datos ingresados");
            }
        }

        public void DevolverVenta(Guid IdVenta,Guid IdVendedor)
        {
            Dictionary<String, String> map = new Dictionary<String, String>();
            map.Add("id", IdVenta.ToString());
            map.Add("idUsuario", IdVendedor.ToString());

            var jsonRequest = JsonConvert.SerializeObject(map);

            HttpResponseMessage response = WebHelper.Patch("Venta/DevolverVenta", jsonRequest);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Verifique los datos ingresados");
            }
        }

        public List<Venta> TraerVenta(Guid idVenta)
        {
            HttpResponseMessage response = WebHelper.Get("Venta/GetVenta?id" + idVenta);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Verifique los datos ingresados");
            }
            else
            {
                var contentStream = response.Content.ReadAsStringAsync().Result;
                List<Venta> listadoventas = JsonConvert.DeserializeObject<List<Venta>>(contentStream);
                return listadoventas;
            }
        }

        public List<Venta> TraerVentaporCliente(Guid idCliente)
        {
            HttpResponseMessage response = WebHelper.Get("Venta/GetVenta?id" + idCliente);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Verifique los datos ingresados");
            }
            else
            {
                var contentStream = response.Content.ReadAsStringAsync().Result;
                List<Venta> listadoventas = JsonConvert.DeserializeObject<List<Venta>>(contentStream);
                return listadoventas;
            }
        }
    }
}

