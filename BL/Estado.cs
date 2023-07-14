using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Estado
    {
        public static ML.Result GetByIdPais(int idPais)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ShernandezProgramacionNcapasContext context = new DL.ShernandezProgramacionNcapasContext())
                {
                    var query = context.Estados.FromSqlRaw($"EstadoGetByIdPais {idPais}").ToList();

                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();

                        foreach(var obj in query)
                        {
                            ML.Estado estado = new ML.Estado();

                            estado.IdEstado = obj.IdEstado;
                            estado.Nombre = obj.Nombre;

                            estado.Pais = new ML.Pais();
                            estado.Pais.IdPais = obj.IdPais.Value;

                            result.Objects.Add(estado);
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
    }
}
