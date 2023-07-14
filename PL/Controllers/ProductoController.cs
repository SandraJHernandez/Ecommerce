using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class ProductoController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Producto producto = new ML.Producto();
            producto.Nombre = "";
            producto.Departamento = new ML.Departamento();
            producto.Departamento.IdDepartamento = 0;

            ML.Result result = BL.Producto.GetAll(producto);
           
            if (result.Correct)
            {
                producto.Productos = result.Objects;
                return View(producto);
            }
            else
            {
                ViewBag.Message = result.Message;
                return View(producto);
            }
        }

        [HttpPost]

        public ActionResult GetAll(ML.Producto producto)
        {
            ML.Result result = BL.Producto.GetAll(producto);

            if (result.Correct)
            {
                producto.Productos = result.Objects;
                return View(producto);
            }
            else
            {
                ViewBag.Message=result.Message;
                return View(producto);
            }
        }

       [HttpGet]

       public ActionResult Form(int? idProducto)
       {
            ML.Result resultArea = BL.Area.GetAll();

            ML.Producto producto = new ML.Producto();

            producto.Departamento = new ML.Departamento();
            producto.Departamento.Area = new ML.Area();

            producto.Departamento.Area.Areas = resultArea.Objects;

            if(idProducto == null)
            {
                ViewBag.Titulo = "Agregar";
                return View(producto);
            }
            else
            {
                ML.Result result = BL.Producto.GetById(idProducto.Value);
                if (result.Correct)
                {
                    producto = (ML.Producto)result.Object;
                    producto.Departamento.Area.Areas = resultArea.Objects;

                    ML.Result resultDepartamento = BL.Departamento.GetByIdArea(producto.Departamento.Area.IdArea);

                    ViewBag.Titulo = "Actualizar";
                    return View(producto);
                }
                else
                {
                    ViewBag.Titulo = "ERROR";
                    ViewBag.Message = result.Message;
                    return View("Modal");
                }
            }
       }

        [HttpPost]

        public ActionResult Form(ML.Producto producto)
        {
            if (ModelState.IsValid)
            {
                if(producto.IdProducto == 0)
                {
                    ML.Result result = BL.Producto.Add(producto);

                    if (result.Correct)
                    {
                        ViewBag.Titulo = "Registro Exitoso";
                        ViewBag.Message = "Los datos del producto se agregaron de manera correcta";
                        return View("Modal");
                    }
                    else
                    {
                        ViewBag.Titulo = "ERROR";
                        ViewBag.Message = "Ocurrio un error al intentar agregar los datos del producto";
                        return View("Modal");
                    }
                }
                else
                {
                    ML.Result result = BL.Producto.Update(producto);
                    if (result.Correct)
                    {
                        ViewBag.Titulo = "Modificacion Exitosa";
                        ViewBag.Message = "Los datos del producto se modificacion de manera correcta";
                        return View("Modal");
                    }
                    else
                    {
                        ViewBag.Titulo = "ERROR";
                        ViewBag.Message = "Ocurrio un error al intentar modificar los datos del producto";
                        return View("Modal");
                    }
                }
            }
            else
            {
                ML.Result resultArea = BL.Area.GetAll();
                producto.Departamento = new ML.Departamento();
                producto.Departamento.Area = new ML.Area();
               
                producto.Departamento.Area.Areas = resultArea.Objects;

                return View(producto);
            }
        }

        [HttpGet]

        public ActionResult Delete(int idProducto)
        {
            ML.Producto producto = new ML.Producto();
            producto.IdProducto = Convert.ToInt32(idProducto);
            var result = BL.Producto.Delete(producto);

            if (result.Correct)
            {
                ViewBag.Titulo = "Eliminado";
                ViewBag.Message = "El producto se elimino de forma correcta";
                //return View(usuario);
            }
            else
            {
                ViewBag.Titulo = "ERROR";
                ViewBag.Message = "Ocurrio un error al intentar eliminar el producto";
                //return View("Modal");
            }
            return PartialView("Modal");
        }

        [HttpGet]

        public JsonResult GetDepartamentos(int idArea)
        {
            ML.Result result = BL.Departamento.GetByIdArea(idArea);

            return Json(result.Objects);
        }
    }
}
