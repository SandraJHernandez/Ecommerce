using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Usuario
    {
        public int IdUsuario { get; set; }

        [Required]
        [MinLength(5)]
        //[RegularExpression("/ ^[A - Z a - z 0 - 9_ -]{5, 20}$/")] // Permite letras mayusculas y minusculas, numeros y guiones (entre 5 y 20 elementos)
        public string UserName { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(50)]
        [RegularExpression("^[A-ZÑa-zñáéíóúÁÉÍÓÚ'° ]+$")] //Permite letras mayusculas y minusculas, espacios, tildes y apostrofes
        public string Nombre { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(50)]
        [RegularExpression("^[A-ZÑa-zñáéíóúÁÉÍÓÚ'° ]+$")] //Permite letras mayusculas y minusculas, espacios, tildes y apostrofes
        public string ApellidoPaterno { get; set; }
        [Required]
        [MinLength(1)]
        [MaxLength(50)]
        [RegularExpression("^[A-ZÑa-zñáéíóúÁÉÍÓÚ'° ]+$")] //Permite letras mayusculas y minusculas, espacios, tildes y apostrofes
        public string ApellidoMaterno { get; set; }

        [Required]
        [MaxLength(50)]
        [EmailAddress]
        //[RegularExpression("^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:.[a-zA-Z0-9-]+)*$")] //Permite letras minusculas y mayusculas, numeros del 0 al 9 y caracteres especiales
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        [MaxLength(8)]
        //[RegularExpression("^(?=w*d)(?=w*[A - Z])(?=w*[a - z])S{6,8}$")] //Permite letras mayusculas y minusculas
        public string Password { get; set; }

        [Required]
        //[DataType(DataType.Date)]
        //[RegularExpression("^([0-2][0-9]|3[0-1])(/|-)(0[1-9]|1[0-2])2(d{4})$")]
        public string FechaNacimiento { get; set; }

        [Required]
        [MaxLength(1)]
        public string Sexo { get; set; }

        [MaxLength(10)]
        //[RegularExpression("^[0 - 9] +$")] //Permite numeros del 0 al 9
        public string Telefono { get; set; }

        [MaxLength(10)]
        //[RegularExpression("^[0 - 9] +$")] //Permite numeros del 0 al 9
        public string Celular { get; set; }

        [Required]
        //Las posiciones donde se esperan letras, vocales y consonantes de los nombres y apellidos.
        //Fecha válida(aunque para simplificarlo, no se están validando meses con menos de 31 días).
        //Listado válido de entidades federativas.
        //Y genera referencias para separar los primeros 17 dígitos(grupo 1) del último dígito(grupo 2).

        //[RegularExpression("/^([A-Z][AEIOUX][A-Z]{2} d{2}(?:0[1-9]|1[0-2])(?:0[1-9]|[12] d|3[01])[HM](?:AS|B[CS]|C[CLMSH]|D[FG]|G[TR]|HG|JC|M[CNS]|N[ETL]|OC|PL|Q[TR]|S[PLR]|T[CSL]|VZ|YN|ZS)[B-DF-HJ-NP-TV-Z]{3}[A-Z d])(d)$/")]
        public string CURP { get; set; }
        public string Imagen { get; set; }
        public bool Status { get; set; }

        public ML.Rol Rol { get; set; }
        public ML.Direccion Direccion { get; set; }
        public List<Object> Usuarios { get; set; }
    }
}
