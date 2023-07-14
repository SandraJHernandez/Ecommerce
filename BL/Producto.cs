using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Producto
    {
        public static ML.Result GetAll(ML.Producto productos)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ShernandezProgramacionNcapasContext context = new DL.ShernandezProgramacionNcapasContext())
                {
                    
                    var query = context.Productos.FromSqlRaw($"ProductoGetAll '{productos.Nombre}', {productos.Departamento.IdDepartamento}").ToList();
                    
                    result.Objects = new List<object>();

                    if(query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Producto producto = new ML.Producto();
                   
                            producto = new ML.Producto(obj.IdProducto, obj.Nombre, obj.PrecioUnitario, obj.Stock, obj.Descripcion, obj.Imagen);
                            producto.Proveedor = new ML.Proveedor();
                            producto.Proveedor.IdProveedor = obj.IdProveedor.Value;
                            producto.Proveedor.Telefono = obj.Telefono;
                            producto.Departamento = new ML.Departamento();
                            producto.Departamento.IdDepartamento = obj.IdDepartamento.Value;
                            producto.Departamento.Nombre = obj.NombreDepartamento;
                            producto.Departamento.Area = new ML.Area();
                            producto.Departamento.Area.IdArea = obj.IdArea;
                            producto.Departamento.Area.Nombre = obj.NombreArea;


                            result.Objects.Add(producto);
                        }
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error al intentar mostrar los datos" + ex;
            }
            return result;
        }

        public static ML.Result GetById(int idProducto)
        {
            ML.Result result = new ML.Result();

            try
            {
                using(DL.ShernandezProgramacionNcapasContext context = new DL.ShernandezProgramacionNcapasContext())
                {
                    var query = context.Productos.FromSqlRaw($"ProductoGetById {idProducto}").AsEnumerable().FirstOrDefault();
                    result.Objects = new List<object>();

                    if(query != null)
                    {

                        ML.Producto producto = new ML.Producto(query.IdProducto, query.Nombre, query.PrecioUnitario, query.Stock, query.Descripcion, query.Imagen);
                        
                        producto.Proveedor = new ML.Proveedor();
                        producto.Proveedor.IdProveedor = query.IdProveedor.Value;
                        producto.Proveedor.Telefono = query.Telefono;
                        producto.Departamento = new ML.Departamento();
                        producto.Departamento.IdDepartamento = query.IdDepartamento.Value;
                        producto.Departamento.Nombre = query.NombreDepartamento;
                        producto.Departamento.Area = new ML.Area();
                        producto.Departamento.Area.IdArea = query.IdArea;
                        producto.Departamento.Area.Nombre = query.NombreArea;

                        result.Object = producto;
                    }
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error al intentar mostrar los datos" + ex;
            }
            return result;
        }

        public static ML.Result Add(ML.Producto producto)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.ShernandezProgramacionNcapasContext context = new DL.ShernandezProgramacionNcapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"ProductoAdd '{producto.Nombre}', {producto.PrecioUnitario}, {producto.Stock}, {producto.Proveedor.IdProveedor}, {producto.Departamento.IdDepartamento}, '{producto.Descripcion}', '{producto.Imagen}'");

                    if(query > 0)
                    {
                        result.Correct = true;
                    }
                    result.Message = "Los datos se agregaron de manera correcta";
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error al intentar agregar los datos" + ex;
            }
            return result;
        }

        public static ML.Result Update(ML.Producto producto)
        {
            ML.Result result = new ML.Result();

            try
            {
                using(DL.ShernandezProgramacionNcapasContext context = new DL.ShernandezProgramacionNcapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"ProductoUpdate {producto.IdProducto}, '{producto.Nombre}', {producto.PrecioUnitario}, {producto.Stock}, {producto.Proveedor.IdProveedor}, {producto.Departamento.IdDepartamento}, '{producto.Descripcion}', '{producto.Imagen}'");

                    if(query > 1)
                    {
                        result.Correct=true;
                    }
                    result.Message = "Los datos se actualizaron de manera correcta";
                }
            }
            catch (Exception ex)
            { 
                result.Correct= false;
                result.Ex = ex;
                result.Message = "Ocurrio un error al intentar actualizar los datos" + ex;
            }
            return result;
        }

        public static ML.Result Delete(ML.Producto producto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ShernandezProgramacionNcapasContext context = new DL.ShernandezProgramacionNcapasContext()) //conexion a la base de datos por medio de la cadena del EF
                {
                    var queryResult = context.Database.ExecuteSqlRaw($"ProductoDelete {producto.IdProducto}");

                    if (queryResult >= 1)
                    {
                        result.Correct = true;
                    }
                    result.Message = "El registro se elimino correctamente";
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error al intentar eliminar los datos" + ex.Message;
            }
            return result;
        }
    }
}
