using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario();
            ML.Result result = BL.Usuario.GetAll(usuario);

            var datosUsuario = (usuario.Nombre, usuario.ApellidoPaterno, usuario.ApellidoMaterno == "" ? " ": " ");
            //usuario.Nombre = "";
            //usuario.ApellidoPaterno = "";
            //usuario.ApellidoMaterno = "";
             
            if (result.Correct)
            {
                usuario.Usuarios = result.Objects;
                return View(usuario);
            }
            else
            {
                ViewBag.Message = result.Message;
                return View(usuario);
            }

        }

        [HttpPost]

        public ActionResult GetAll(ML.Usuario usuario)
        {
            ML.Result result = BL.Usuario.GetAll(usuario);
            if (result.Correct)
            {
                usuario.Usuarios = result.Objects;
                return View(usuario);
            }
            else
            {
                ViewBag.Message = result.Message;
                return View(usuario);
            }
        }

        [HttpGet]

        public ActionResult Form(int? idUsuario)
        {
                ML.Result resultRol = BL.Rol.GetAll();
                ML.Result resultPais = BL.Pais.GetAll();

                ML.Usuario usuario = new ML.Usuario();
                usuario.Rol = new ML.Rol();

                usuario.Direccion = new ML.Direccion();
                usuario.Direccion.Colonia = new ML.Colonia();
                usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();

                usuario.Rol.Roles = resultRol.Objects;
                usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPais.Objects;

                if (idUsuario == null)
                {
                    ViewBag.Titulo = "Agregar";

                    return View(usuario);
                }
                else
                {
                    //ML.Result resultRol = BL.Rol.GetAll();
                    ML.Result result = BL.Usuario.GetById(idUsuario.Value);

                    if (result.Correct)
                    {

                        usuario = (ML.Usuario)result.Object;
                        usuario.Rol.Roles = resultRol.Objects;
                        usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPais.Objects;

                        ML.Result resultEstado = BL.Estado.GetByIdPais(usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais);
                        ML.Result resultMunicipio = BL.Municipio.GetByIdEstado(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
                        ML.Result resultColonia = BL.Colonia.GetByIdMunicipio(usuario.Direccion.Colonia.Municipio.IdMunicipio);

                        ViewBag.Titulo = "Actualizar";
                        usuario.Rol.Roles = resultRol.Objects;
                        return View(usuario);
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

        public ActionResult Form(ML.Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                IFormFile image = Request.Form.Files["fileImage"];

                if (image != null)
                {
                    byte[] ImagenBytes = ConvertToBytes(image);

                    usuario.Imagen = Convert.ToBase64String(ImagenBytes);
                }

                if (usuario.IdUsuario == 0)
                {
                    ML.Result result = BL.Usuario.Add(usuario);

                    if (result.Correct)
                    {
                        ViewBag.Titulo = "Registro Exitoso";
                        ViewBag.Message = "Los datos del usuario se agregaron de manera correcta";
                        return View("Modal");

                    }
                    else
                    {
                        ViewBag.Titulo = "ERROR";
                        ViewBag.Message = "Ocurrio un error al intentar agregar los datos del usuario";
                        return View("Modal");
                    }
                }
                else
                {
                    ML.Result result = BL.Usuario.Update(usuario);

                    if (result.Correct)
                    {
                        ViewBag.Titulo = "Modificacion Existosa";
                        ViewBag.Message = "El usuario se modifico de manera correcta";
                        return View("ModaL");
                    }
                    else
                    {
                        ViewBag.Titulo = "ERROR";
                        ViewBag.Message = "Los datos del usuario no se actualizacion";
                        return View("Modal");
                    }
                }
            }
            else
            {
                ML.Result resultRol = BL.Rol.GetAll();
                ML.Result resultPais = BL.Pais.GetAll();

                usuario.Rol = new ML.Rol();

                usuario.Direccion = new ML.Direccion();
                usuario.Direccion.Colonia = new ML.Colonia();
                usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();

                usuario.Rol.Roles = resultRol.Objects;
                usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPais.Objects;

                return View(usuario);
            }
                
            
        }

        [HttpGet]

        public ActionResult Delete(int idUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.IdUsuario = Convert.ToInt32(idUsuario);
            var result = BL.Usuario.Delete(usuario);

            if (result.Correct)
            {
                ViewBag.Titulo = "Eliminado";
                ViewBag.Message = "El usuario se elimino de forma correcta";
                //return View(usuario);
            }
            else
            {
                ViewBag.Titulo = "ERROR";
                ViewBag.Message = "Ocurrio un error al intentar eliminar el registro";
                //return View("Modal");
            }
            return PartialView("Modal");
        }

        [HttpGet]

        public JsonResult GetEstados(int idPais)
        {
            ML.Result result = BL.Estado.GetByIdPais(idPais);
            
            return Json(result.Objects);
        }

        [HttpGet]

        public JsonResult GetMunicipios(int idEstado)
        {
            ML.Result result = BL.Municipio.GetByIdEstado(idEstado);

            return Json(result.Objects);
        }

        [HttpGet]

        public JsonResult GetColonias(int idMunicipio)
        {
            ML.Result result = BL.Colonia.GetByIdMunicipio(idMunicipio);

            return Json(result.Objects);
        }

        public static byte[] ConvertToBytes(IFormFile imagen)
        {

            using var fileStream = imagen.OpenReadStream();

            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, (int)fileStream.Length);

            return bytes;
        }

        [HttpPost]

        public JsonResult CambiarStatus(bool status, int idUsuario)
        {
            ML.Result result = BL.Usuario.UpdateStatus(status, idUsuario);

            return Json(result.Object);
        }
    }
}
