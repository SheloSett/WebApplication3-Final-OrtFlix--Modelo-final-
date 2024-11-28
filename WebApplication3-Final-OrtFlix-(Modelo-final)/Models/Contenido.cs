using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections;
namespace WebApplication3_Final_OrtFlix__Modelo_final_.Models
{
    public class Contenido
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public Boolean Premium { get; set; }

        [EnumDataType(typeof(TipoContenido))]
        public TipoContenido TipoContenido { get; set; }

        [EnumDataType(typeof(Valoracion))]
        public Valoracion Valoracion { get; set; }
        public int Duracion { get; set; }
        
        [EnumDataType(typeof(Clasificacion))]
        public Clasificacion Clasificacion { get; set; }

        public List<Review>? Reviews { get; set; }
    }
}
