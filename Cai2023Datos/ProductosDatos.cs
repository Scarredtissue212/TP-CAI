using System;
using System.Collections.Specialized;
using Newtonsoft.Json;
using Cai2023Entidades;
using Cai2023Datos.Utilidades;

namespace Cai2023Datos
{
    public class ProductoDatos
    {
        public void InsertarProducto(int Categoria, Guid idproveedor,string Nombre, double precio, int stock)
        {
            string idusuario = "D347CE99-DB8D-4542-AA97-FC9F3CCE6969"; //hardcode de id usuario admin
            Dictionary<String, String> map = new Dictionary<String, String>();
            map.Add("Categoria", Categoria.ToString());
            map.Add("Idproveedor", idproveedor.ToString());
            map.Add("Idusuario", idusuario);
            map.Add("nombre", Nombre);
            map.Add("precio", precio.ToString());
            map.Add("stock", stock.ToString());

            var jsonRequest = JsonConvert.SerializeObject(map);

            HttpResponseMessage response = WebHelper.Post("Producto/AgregarProducto", jsonRequest);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Verifique los datos ingresados");
            }
        }

        public List<Producto> TraerTodos()
        {
            HttpResponseMessage response = WebHelper.Get("Producto/TraerProductos");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Verifique los datos ingresados");
            }
            else
            {
                var contentStream = response.Content.ReadAsStringAsync().Result;
                List<Producto> listadoproductos = JsonConvert.DeserializeObject<List<Producto>>(contentStream);
                return listadoproductos;
            }
        }

        public List<Producto> TraerporCategoria(int Idcategoria)
        {
            HttpResponseMessage response = WebHelper.Get("Producto/TraerProductosPorCategoria?catnum=" + Idcategoria);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Verifique los datos ingresados");
            }
            else
            {
                var contentStream = response.Content.ReadAsStringAsync().Result;
                List<Producto> listadoproductosporcategoria = JsonConvert.DeserializeObject<List<Producto>>(contentStream);
                return listadoproductosporcategoria;
            }
        }

        public void EliminarProducto(Guid idProducto)
        {
            string idusuario = "D347CE99-DB8D-4542-AA97-FC9F3CCE6969";
            Dictionary<String, String> map = new Dictionary<String, String>();
            map.Add("id", idProducto.ToString());
            map.Add("idUsuario", idusuario);

            var jsonRequest = JsonConvert.SerializeObject(map);

            HttpResponseMessage response = WebHelper.DeleteConBody("Producto/BajaProducto", jsonRequest);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Verifique los datos ingresados");
            }
        }

        public void ReactivarProducto(Guid idProducto)
        {
            string idusuario = "D347CE99-DB8D-4542-AA97-FC9F3CCE6969";

            Dictionary<String, String> map = new Dictionary<String, String>();
            map.Add("id", idProducto.ToString());
            map.Add("idUsuario", idusuario);

            var jsonRequest = JsonConvert.SerializeObject(map);

            HttpResponseMessage response = WebHelper.Patch("Producto/ReactivarProducto", jsonRequest);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Verifique los datos ingresados");
            }

        }
        public void ActualizarProducto(Guid idproducto, double precio, int stock)
        {
            string idusuario = "D347CE99-DB8D-4542-AA97-FC9F3CCE6969";

            Dictionary<String, String> map = new Dictionary<String, String>();
            map.Add("id", idproducto.ToString());
            map.Add("idUsuario", idusuario);
            map.Add("precio", precio.ToString());
            map.Add("stock", stock.ToString());

            var jsonRequest = JsonConvert.SerializeObject(map);

            HttpResponseMessage response = WebHelper.Patch("Producto/ModificarProducto", jsonRequest);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Verifique los datos ingresados");
            }
        }

        public static string Actualizar(Guid idProducto, double precio, int stock)
        {
            string idusuario = "D347CE99-DB8D-4542-AA97-FC9F3CCE6969";
            Dictionary<String, String> map = new Dictionary<String, String>();
            map.Add("id", idProducto.ToString());
            map.Add("IdUsuario", idusuario);
            map.Add("precio", precio.ToString());
            map.Add("stock", stock.ToString());

            var jsonRequest = JsonConvert.SerializeObject(map);

            HttpResponseMessage response = WebHelper.Patch("Producto/ModificarProducto", jsonRequest);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Verifique los datos ingresados");
            }

            var reader = new StreamReader(response.Content.ReadAsStream());

            String respuesta = reader.ReadToEnd();

            return respuesta;
        }
    }
}

