using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace PL.Controllers
{
    public class UsuarioCargaMasivaController : Controller
    {
        private IHostingEnvironment environment;
        private IConfiguration configuration;
        public UsuarioCargaMasivaController(IHostingEnvironment _environment, IConfiguration _configuration)
        {
            environment = _environment;
            configuration = _configuration;
        }
        public ActionResult GetCargaMasiva()
        {
            ML.Result result = new ML.Result();
            return View(result);
        }

        [HttpPost]

        public ActionResult PostCargaMasiva(IFormFile file)
        {
            //Si el arvhico viene vacio o simplemente no tiene un tamaño, se redirecciona a la vista
            if (file == null || file.Length <= 0)
            {
                return RedirectToAction("GetCargaMasiva");
            }

            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                string line = reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    line = reader.ReadLine();
                    var values = line.Split('|');

                    ML.Usuario usuario = new ML.Usuario();

                    usuario.UserName = values[0];
                    usuario.Nombre = values[1];
                    usuario.ApellidoPaterno = values[2];
                    usuario.ApellidoMaterno = values[3];
                    usuario.Email = values[4];
                    usuario.Password = values[5];
                    usuario.FechaNacimiento = values[6];
                    usuario.Sexo = values[7];
                    usuario.Telefono = values[8];
                    usuario.Celular = values[9];
                    usuario.CURP = values[10];

                    usuario.Rol = new ML.Rol();
                    usuario.Rol.IdRol = int.Parse(values[11]);

                    usuario.Direccion = new ML.Direccion();
                    usuario.Direccion.Calle = values[12];
                    usuario.Direccion.NumeroInterior = values[13];
                    usuario.Direccion.NumeroExterior = values[14];

                    usuario.Direccion.Colonia = new ML.Colonia();
                    usuario.Direccion.Colonia.IdColonia = int.Parse(values[15]);

                    ML.Result result = BL.Usuario.Add(usuario);

                    if (result.Correct)
                    {
                        string rutaCompleta = @"C:\Users\digis\Documents\Sandra Joceline Hernandez Hernandez\UsuariosCargaMasivaResultado.txt";
                        var texto = "El registro se agrego correctamente";

                        using (StreamWriter files = new StreamWriter(rutaCompleta, true))
                        {
                            files.WriteLine(texto); //se agrega información al documento

                            files.Close();
                        }
                    }
                    else
                    {
                        string rutaCompleta = @"C:\Users\digis\Documents\Sandra Joceline Hernandez Hernandez\UsuariosCargaMasivaResultado.txt";
                        var texto = "ERROR " + result.Ex;

                        using (StreamWriter files = new StreamWriter(rutaCompleta, true))
                        {
                            files.WriteLine(texto); //se agrega información al documento

                            files.Close();
                        }
                    }
                }
            }

            return RedirectToAction("GetAll", "Usuario");
        }

        [HttpPost]

        public ActionResult GetCargaMasiva(int? valor)
        {
            IFormFile file = Request.Form.Files["file"];

            if (HttpContext.Session.GetString("PathArchivo") == null)
            {
                if (file != null)
                {
                    string fileName = Path.GetFileName(file.FileName); //GetFileName recupera el nombre del archivo, para poder obtener la extension
                    string extensionArchivo = Path.GetExtension(file.FileName).ToLower(); //GetExtension recupera la extencion del nombre del documento que se recupero anteriormente || El ToLower convierte todas las letras en minusculas
                    string extensionAceptada = configuration["TipoExcel"]; //Se crea esta variable para indicar el tipo de extension que vamos a aceptar, en este caso, de excel.
                    //Se coloca la ruta del ordenador debido a que estamos trabajando de manera local, cuando es con un servidor, esta deja de funcionar y se debe modificar
                    string folderPath = configuration["PathFolder:ruta"];  //Se coloca la direccion de donde se guardaran las copias que se realizaran de todos los documentos que ingrese el usuario, debido a que asi podremos modificar la copia y no alterar el documento original
                    if (extensionArchivo == extensionAceptada) //se comparan las extension, si ambas coinciden entra al bloque if, si no, entra al else
                    {
                        string filePath = Path.Combine(environment.ContentRootPath, folderPath, Path.GetFileNameWithoutExtension(fileName)) + '-' + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx"; //crea la ruta y se concatena con la fecha y hora en que se realiza, para evitar que se creen archivos con el mismo nombre y se sobreescriban

                        if (!System.IO.File.Exists(filePath))
                        {
                            using (FileStream stream = new FileStream(filePath, FileMode.Create)) //crea un objeto temporal con la instruccion crear
                            {
                                file.CopyTo(stream); //crea la copia de la informacion del archivo
                            }
                            string connString = configuration["ExcelConString:value"] + filePath; //obtiene la cadena de conexion de OLE DB y la ruta del archivo

                            ML.Result resultExcelDt = BL.Usuario.ConvertExcelToDataTable(connString);

                            if (resultExcelDt.Correct)
                            {
                                ML.Result resultValidacion = BL.Usuario.ValidarExcel(resultExcelDt.Objects);

                                if (resultValidacion.Objects.Count == 0)
                                {
                                    resultValidacion.Correct = true;
                                    HttpContext.Session.SetString("PathArchivo", filePath);
                                }
                                return View(resultValidacion);
                            }
                        }
                        else
                        {
                            ViewBag.Message = "El archivo que se intenta procesar no es un excel";
                        }
                    }
                    else
                    {
                        ViewBag.Message = "No se ha insertado un archivo";
                    }
                    return View();
                }
             return View();

            }
            else
            {
                string rutaArchivoExcel = HttpContext.Session.GetString("PathArchivo");
                string connectionString = configuration["ExcelConString:value"] + rutaArchivoExcel;

                ML.Result resultData = BL.Usuario.ConvertExcelToDataTable(connectionString);
                if (resultData.Correct)
                {
                    ML.Result resultErrores = new ML.Result();
                    resultErrores.Objects = new List<object>();

                    foreach (ML.Usuario usuarioItem in resultData.Objects)
                    {
                        ML.Result resultAdd = BL.Usuario.Add(usuarioItem);
                        if (!resultAdd.Correct)
                        {
                            resultErrores.Objects.Add("No se inserto el Usuario con UserName: " + usuarioItem.UserName + "Error: " + resultAdd.Message);
                        }
                    }
                    if (resultErrores.Objects.Count > 0)
                    {
                        string fileError = Path.Combine(environment.WebRootPath, @"logErrores.txt");
                        using (StreamWriter writer = new StreamWriter(fileError))
                        {
                            foreach (string ln in resultErrores.Objects)
                            {
                                writer.WriteLine(ln);
                            }
                        }
                        ViewBag.Message = "Los Usuarios no han sido agregados correctamente";
                    }
                    else
                    {
                        ViewBag.Message = "Los Usuarios han sido agregados correctamente";
                    }
                }
            }
            return View();
        }
    }
}
