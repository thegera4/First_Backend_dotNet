using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace First_Backend_dotNet.Models
{
    public class Beer
    {
        [Key] // para marcar la propiedad como primary key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // para generar en automatico la propiedad y autoincrementarla
        public int BeerId { get; set; }
        public string Name { get; set; }
        public int BrandId { get; set; } // esta es la propiedad que se va a relacionar a la propiedad con el mismo nombre de "Brand"

        [Column(TypeName = "decimal(18,2)")] // para definir el tipo de dato en la base de datos (decimal con 18 digitos y 2 decimales)
        public decimal Alcohol { get; set; }

        [ForeignKey("BrandId")] //el string es el nombre de la propiedad de arriba (la que vamos a relacionar)
        public virtual Brand Brand { get; set; } // se declara una propiedad virtual para poder relacionar las propiedades

    }
}
