using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiDoctoresPruebaCasa.Models
{
    [Table("Doctor")]
    public class Doctor
    {
        [Column("HOSPITAL_COD")]
        public string Cod_Hospital { get; set; }
        [Key]
        [Column("DOCTOR_NO")]
        public string NumDoctor { get; set; }
        [Column("APELLIDO")]
        public string Apellido { get; set; }
        [Column("ESPECIALIDAD")]
        public string Especialidad { get; set; }
        [Column("SALARIO")]
        public int Salario { get; set; }
        [Column("IMAGEN")]
        public string Imagen { get; set; }
    }
}
