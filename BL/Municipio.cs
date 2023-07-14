﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Municipio
    {
        public static ML.Result GetByIdEstado(int idEstado)
        {
            ML.Result result = new ML.Result();

            try
            {
                using(DL.ShernandezProgramacionNcapasContext context = new DL.ShernandezProgramacionNcapasContext())
                {
                    var query = context.Municipios.FromSqlRaw($"MunicipioGetByIdEstado {idEstado}").ToList();

                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();

                        foreach(var obj in query)
                        {
                            ML.Municipio municipio = new ML.Municipio();

                            municipio.IdMunicipio = obj.IdMunicipio;
                            municipio.Nombre = obj.Nombre;

                            municipio.Estado = new ML.Estado();
                            municipio.Estado.IdEstado = obj.IdEstado.Value;

                            result.Objects.Add(municipio);
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
