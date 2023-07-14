using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Departamento
    {
        public static ML.Result GetByIdArea(int idArea)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.ShernandezProgramacionNcapasContext context = new DL.ShernandezProgramacionNcapasContext())
                {
                    var query = context.Departamentos.FromSqlRaw($"DepartamentoGetByIdArea {idArea}").ToList();

                    result.Objects = new List<object>();

                    if (query.Count >0)
                    {
                        result.Objects = new List<object>();

                        foreach (var obj in query)
                        {
                            ML.Departamento departamento = new ML.Departamento();

                            departamento.IdDepartamento = obj.IdDepartamento;
                            departamento.Nombre = obj.Nombre;

                            departamento.Area = new ML.Area();
                            departamento.Area.IdArea = obj.IdArea.Value;
                            
                            result.Objects.Add(departamento);
                        }
                        result.Correct = true;
                    }

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error al intentar mostrar los datos";
            }
            return result;
        }
    }
}
