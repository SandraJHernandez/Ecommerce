using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Producto
    {
        public Producto()
        {
        }

        public Producto(int idProducto, string nombre, decimal precioUnitario, int stock, string descripcion, string imagen)
        {
            ML.Producto producto = new ML.Producto();

            IdProducto = idProducto;
            Nombre = nombre;
            PrecioUnitario = precioUnitario;
            Stock = stock;
            Descripcion = descripcion;
            Imagen = imagen;

        }

        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public decimal PrecioUnitario { get; set; }
        public int Stock { get; set; }
        public ML.Proveedor Proveedor { get; set; } //Propiedad de navegacion
        public ML.Departamento Departamento { get; set; } //Propiedad de navegacion
        public string Descripcion { get; set; }
        public string Imagen { get; set; }

        public List<Object> Productos { get; set; } //se utiliza para el uso de lista en el GetAll en MVC
    }
}
