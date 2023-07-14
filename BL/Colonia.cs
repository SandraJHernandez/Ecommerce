using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Colonia
    {
        public static ML.Result GetByIdMunicipio(int idMunicipio)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.ShernandezProgramacionNcapasContext context = new DL.ShernandezProgramacionNcapasContext())
                {
                    var query = context.Colonia.FromSqlRaw($"ColoniaGetByIdMunicipio {idMunicipio}").ToList();
                    
                    if(query.Count > 0)
                    {
                        result.Objects = new List<object>();

                        foreach(var obj in query)
                        {
                            ML.Colonia colonia = new ML.Colonia();

                            colonia.IdColonia = obj.IdColonia;
                            colonia.Nombre = obj.Nombre;

                            colonia.Municipio = new ML.Municipio();
                            colonia.Municipio.IdMunicipio = obj.IdMunicipio.Value;

                            result.Objects.Add(colonia);
                        }
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error al intetar mostrar los datos" + ex;   
            }
            return result;
        }
    }
}
