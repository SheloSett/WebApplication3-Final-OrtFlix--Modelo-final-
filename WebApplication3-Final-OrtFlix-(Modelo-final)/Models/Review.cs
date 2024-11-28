using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication3_Final_OrtFlix__Modelo_final_.Models
{
    public class Review
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Titulo { get; set; }

        public string Descripcion { get; set; }

        [EnumDataType(typeof(Valoracion))]
        public Valoracion Estrellas { get; set; }
    }
}