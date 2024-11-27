using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace WebApplication3_Final_OrtFlix__Modelo_final_.Models
{
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string NombreCompleto { get; set; }
        public string Password {  get; set; }
        public string Email {  get; set; }
        public Boolean EsAdmin {  get; set; }
        public Boolean Premium { get; set; }
        
    }
}
