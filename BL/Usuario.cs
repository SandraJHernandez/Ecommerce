using Microsoft.EntityFrameworkCore;
using ML;
using System.Data;
using System.Data.OleDb;

namespace BL
{
    public class Usuario
    {
       public static ML.Result GetAll(ML.Usuario usuarios)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ShernandezProgramacionNcapasContext context = new DL.ShernandezProgramacionNcapasContext())
                {
                    var query = context.Usuarios.FromSqlRaw($"UsuarioGetAll '{usuarios.Nombre}', '{usuarios.ApellidoPaterno}', '{usuarios.ApellidoMaterno}'").ToList();

                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Usuario usuario = new ML.Usuario();

                            usuario.IdUsuario = obj.IdUsuario;
                            usuario.UserName = obj.UserName;
                            usuario.Nombre = obj.Nombre;
                            usuario.ApellidoPaterno = obj.ApellidoPaterno;
                            usuario.ApellidoMaterno = obj.ApellidoMaterno;
                            usuario.Email = obj.Email;
                            usuario.Password = obj.Password;
                            usuario.FechaNacimiento = obj.FechaNacimiento.ToString("dd/MM/yyyy");
                            usuario.Sexo = obj.Sexo;
                            usuario.Telefono = obj.Telefono;
                            usuario.Celular = obj.Celular;
                            usuario.CURP = obj.Curp;
                            usuario.Imagen = obj.Imagen;

                            usuario.Rol = new ML.Rol();
                            usuario.Rol.IdRol = obj.IdRol.Value;
                            usuario.Rol.Nombre = obj.NombreRol;

                            usuario.Direccion = new ML.Direccion();
                            usuario.Direccion.IdDireccion = obj.IdDireccion;
                            usuario.Direccion.Calle = obj.Calle;
                            usuario.Direccion.NumeroInterior = obj.NumeroInterior;
                            usuario.Direccion.NumeroExterior = obj.NumeroExterior;

                            usuario.Direccion.Colonia = new ML.Colonia();
                            usuario.Direccion.Colonia.IdColonia = obj.IdColonia;
                            usuario.Direccion.Colonia.Nombre = obj.NombreColonia;
                            usuario.Direccion.Colonia.CodigoPostal = obj.CodigoPostal;

                            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                            usuario.Direccion.Colonia.Municipio.IdMunicipio = obj.IdMunicipio;
                            usuario.Direccion.Colonia.Municipio.Nombre = obj.NombreMunicipio;

                            usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                            usuario.Direccion.Colonia.Municipio.Estado.IdEstado = obj.IdEstado;
                            usuario.Direccion.Colonia.Municipio.Estado.Nombre = obj.NombreEstado;

                            usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
                            usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais = obj.IdPais;
                            usuario.Direccion.Colonia.Municipio.Estado.Pais.Nombre = obj.NombrePais;

                            usuario.Status = obj.Status;

                            result.Objects.Add(usuario);
                        }

                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct=false;
                result.Ex = ex;
                result.Message = "Ocurrio un error al intentar mostrar los registros" + ex.Message;
            }
            return result;
        }

        public static ML.Result GetById(int idUsuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.ShernandezProgramacionNcapasContext context = new DL.ShernandezProgramacionNcapasContext())
                {
                    var query = context.Usuarios.FromSqlRaw($"UsuarioGetById {idUsuario}").AsEnumerable().FirstOrDefault();

                    result.Objects = new List<object>();

                    if(query != null)
                    {
                        ML.Usuario usuario = new ML.Usuario();

                        usuario.IdUsuario = query.IdUsuario;
                        usuario.UserName = query.UserName;
                        usuario.Nombre = query.Nombre;
                        usuario.ApellidoPaterno = query.ApellidoPaterno;
                        usuario.ApellidoMaterno = query.ApellidoMaterno;
                        usuario.Email = query.Email;
                        usuario.Password = query.Password;
                        usuario.FechaNacimiento = query.FechaNacimiento.ToString("dd/MM/yyyy");
                        usuario.Sexo = query.Sexo;
                        usuario.Telefono = query.Telefono;
                        usuario.Celular = query.Celular;
                        usuario.CURP = query.Curp;
                        usuario.Imagen = query.Imagen;
                        usuario.Rol = new ML.Rol();
                        usuario.Rol.IdRol = query.IdRol.Value;
                        usuario.Rol.Nombre = query.NombreRol;

                        usuario.Direccion = new ML.Direccion();
                        usuario.Direccion.IdDireccion = query.IdDireccion;
                        usuario.Direccion.Calle = query.Calle;
                        usuario.Direccion.NumeroInterior = query.NumeroInterior;
                        usuario.Direccion.NumeroExterior = query.NumeroExterior;

                        usuario.Direccion.Colonia = new ML.Colonia();
                        usuario.Direccion.Colonia.IdColonia = query.IdColonia;
                        usuario.Direccion.Colonia.Nombre = query.NombreColonia;
                        usuario.Direccion.Colonia.CodigoPostal = query.CodigoPostal;

                        usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                        usuario.Direccion.Colonia.Municipio.IdMunicipio = query.IdMunicipio;
                        usuario.Direccion.Colonia.Municipio.Nombre = query.NombreMunicipio;

                        usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                        usuario.Direccion.Colonia.Municipio.Estado.IdEstado = query.IdEstado;
                        usuario.Direccion.Colonia.Municipio.Estado.Nombre = query.NombreEstado;

                        usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
                        usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais = query.IdPais;
                        usuario.Direccion.Colonia.Municipio.Estado.Pais.Nombre = query.NombrePais;

                        usuario.Status = query.Status;

                        result.Object = usuario;

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

        public static ML.Result Add(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using(DL.ShernandezProgramacionNcapasContext context = new DL.ShernandezProgramacionNcapasContext())
                {
                    bool status = true;
                    usuario.Status = status;
                    var query = context.Database.ExecuteSqlRaw($"UsuarioAdd '{usuario.UserName}', '{usuario.Nombre}', '{usuario.ApellidoPaterno}','{usuario.ApellidoMaterno}', '{usuario.Email}', '{usuario.Password}', '{usuario.FechaNacimiento}', '{usuario.Sexo}', '{usuario.Telefono}', '{usuario.Celular}', '{usuario.CURP}', '{usuario.Imagen}', {usuario.Rol.IdRol}, '{usuario.Direccion.Calle}', '{usuario.Direccion.NumeroInterior}', '{usuario.Direccion.NumeroExterior}', {usuario.Direccion.Colonia.IdColonia}, {usuario.Status}");

                    if(query > 0)
                    {
                        result.Correct = true;
                        result.Message = "El registro se guardo correctamente";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct=false;
                result.Ex=ex;
                result.Message = "Ocurrio un error al intentar agregar los datos";
            }
            return result;
        }

        public static ML.Result Update(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ShernandezProgramacionNcapasContext context = new DL.ShernandezProgramacionNcapasContext()) //conexion a la base de datos por medio de la cadena del EF
                {
                    var queryResult = context.Database.ExecuteSqlRaw($"UsuarioUpdate {usuario.IdUsuario}, '{usuario.UserName}', '{usuario.Nombre}', '{usuario.ApellidoPaterno}', '{usuario.ApellidoMaterno}', '{usuario.Email}', '{usuario.Password}', '{usuario.FechaNacimiento}', '{usuario.Sexo}', '{usuario.Telefono}', '{usuario.Celular}', '{usuario.CURP}', '{usuario.Imagen}', {usuario.Rol.IdRol}, '{usuario.Direccion.Calle}', '{usuario.Direccion.NumeroInterior}', '{usuario.Direccion.NumeroExterior}', {usuario.Direccion.Colonia.IdColonia}");

                    if (queryResult > 1)
                    {
                        result.Correct = true;
                    }
                    result.Message = "El registro se guardo correctamente";
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error al intentar actualizar los datos";
            }
            return result;
        }

        public static ML.Result Delete(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ShernandezProgramacionNcapasContext context = new DL.ShernandezProgramacionNcapasContext()) //conexion a la base de datos por medio de la cadena del EF
                {
                    var queryResult = context.Database.ExecuteSqlRaw($"UsuarioDelete {usuario.IdUsuario}");

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

        public static ML.Result UpdateStatus(bool status, int idUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ShernandezProgramacionNcapasContext context = new DL.ShernandezProgramacionNcapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"UsuariostatusUpdate {status}, {idUsuario}");

                    if(query > 1)
                    {
                        result.Correct=true;
                    }
                    result.Message = "El registro se actualizo de manera correcta";
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error al intentar actualizar el status " + ex.Message;
            }
            return result;
        }

        public static ML.Result ConvertExcelToDataTable(string connString)
        {
            ML.Result result = new ML.Result();

            try
            {
                using(OleDbConnection context = new OleDbConnection(connString))
                {
                    string query = "SELECT * FROM [Sheet1$]";

                    using(OleDbCommand cmd = new OleDbCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Connection = context;

                        OleDbDataAdapter da = new OleDbDataAdapter();
                        da.SelectCommand = cmd;

                        DataTable tablaUsuario = new DataTable();

                        da.Fill(tablaUsuario);

                        if(tablaUsuario.Rows.Count > 0)
                        {
                            result.Objects = new List<object>();

                            foreach(DataRow row in tablaUsuario.Rows)
                            {
                                ML.Usuario usuario = new ML.Usuario();

                                usuario.UserName = row[0].ToString();
                                usuario.Nombre = row[1].ToString();
                                usuario.ApellidoPaterno = row[2].ToString();
                                usuario.ApellidoMaterno = row[3].ToString();
                                usuario.Email = row[4].ToString();
                                usuario.Password = row[5].ToString();
                                usuario.FechaNacimiento = row[6].ToString();
                                usuario.Sexo = row[7].ToString();
                                usuario.Telefono = row[8].ToString();
                                usuario.Celular = row[9].ToString();
                                usuario.CURP = row[10].ToString();

                                usuario.Rol = new ML.Rol();
                                usuario.Rol.IdRol = int.Parse(row[11].ToString());

                                usuario.Direccion = new ML.Direccion();
                                usuario.Direccion.Calle = row[12].ToString();
                                usuario.Direccion.NumeroInterior = row[13].ToString();
                                usuario.Direccion.NumeroExterior = row[14].ToString();

                                usuario.Direccion.Colonia = new ML.Colonia();
                                usuario.Direccion.Colonia.IdColonia = int.Parse(row[15].ToString());

                                result.Objects.Add(usuario);
                            }
                            result.Correct = true;
                        }
                        result.Object = tablaUsuario;

                        if(tablaUsuario.Rows.Count > 1)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.Message = "No existen registros en el excel";
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = ex.Message;
            }
            return result;
        }

        public static ML.Result ValidarExcel(List<Object> Objects)
        {
            ML.Result result = new ML.Result();
            try
            {
                result.Objects = new List<object>();

                int i = 1; //variable que sirve para que se le agregue un numero a cada registro.
                foreach (ML.Usuario usuario in Objects)
                {
                    ML.ErrorExcel error = new ML.ErrorExcel();
                    error.IdRegistro = i++;

                    usuario.UserName = (usuario.UserName == "") ? error.Mensaje += "Ingrese el UserName " : usuario.UserName;
                    usuario.Nombre = (usuario.Nombre == "") ? error.Mensaje += "Ingrese el Nombre " : usuario.Nombre;
                    usuario.ApellidoPaterno = (usuario.ApellidoPaterno == "") ? error.Mensaje += "Ingrese el Apellido Paterno " : usuario.ApellidoPaterno;
                    usuario.ApellidoMaterno = (usuario.ApellidoMaterno == "") ? error.Mensaje += "Ingrese el Apellido Materno " : usuario.ApellidoMaterno;
                    usuario.Email = (usuario.Email == "") ? error.Mensaje += "Ingrese el Email " : usuario.Email;
                    usuario.Password = (usuario.Password == "") ? error.Mensaje += "Ingrese el Password " : usuario.Password;
                    usuario.FechaNacimiento = (usuario.FechaNacimiento == "") ? error.Mensaje += "Ingrese la Fecha de Nacimiento " : usuario.FechaNacimiento;
                    usuario.Sexo = (usuario.Sexo == "") ? error.Mensaje += "Ingrese el Sexo " : usuario.Sexo;
                    usuario.Telefono = (usuario.Telefono == "") ? error.Mensaje += "Ingrese el numero de Telefono" : usuario.Telefono;
                    usuario.Celular = (usuario.Celular == "") ? error.Mensaje += "Ingrese el numero de Celular" : usuario.Celular;
                    usuario.CURP = (usuario.CURP == "") ? error.Mensaje += "Ingrese el CURP" : usuario.CURP;
                    if(usuario.Rol.IdRol <= 0)
                    {
                        error.Mensaje += "Ingrese el Id del Rol ";
                    }
                    else
                    {
                        usuario.Rol.IdRol = usuario.Rol.IdRol;
                    }
                    usuario.Direccion.Calle = (usuario.Direccion.Calle == "") ? error.Mensaje += "Ingrese el nombre de la calle " : usuario.Direccion.Calle;
                    usuario.Direccion.NumeroInterior = (usuario.Direccion.NumeroInterior == "") ? error.Mensaje += "Ingrese el numero interior " : usuario.Direccion.NumeroInterior;
                    usuario.Direccion.NumeroExterior = (usuario.Direccion.NumeroExterior == "") ? error.Mensaje += "Ingrese el numero exterior " : usuario.Direccion.NumeroExterior;
                    if(usuario.Direccion.Colonia.IdColonia <= 0)
                    {
                        error.Mensaje += "Ingrese el Id de la Colonia";
                    }
                    else
                    {
                        usuario.Direccion.Colonia.IdColonia = usuario.Direccion.Colonia.IdColonia;
                    }

                    if (error.Mensaje != null)
                    {
                        result.Objects.Add(error);
                    }
                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error al validar el Excel" + ex.Message;
            }
            return result;
        }
    }
}
