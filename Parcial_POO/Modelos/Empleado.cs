using System.ComponentModel.DataAnnotations;

namespace Parcial_POO.Modelos
{
    public class Empleado
    {
        [Key]   // indica que es llave primaria
        public int IdEmpleado {  get; set; }
        public string NombreEmpleado { get; set; }
        public string Cargo { get; set; }
        public string Correo {  get; set; }
        public DateTime FechaCertificacion { get; set; }
            
    }
}
