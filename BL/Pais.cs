using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Pais
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using(DL.ShernandezProgramacionNcapasContext context = new DL.ShernandezProgramacionNcapasContext())
                {
                    var query = context.Pais.FromSqlRaw($"PaisGetAll").ToList();

                    if(query.Count > 0)
                    {
                        result.Objects = new List<object>();

                        foreach(var obj in query)
                        {
                            ML.Pais pais = new ML.Pais();

                            pais.IdPais = obj.IdPais;
                            pais.Nombre = obj.Nombre;

                            result.Objects.Add(pais);
                        }
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error al intentar mostrar los datos" + ex.Message;
            
            }
            return (result);
        }
    }
}
