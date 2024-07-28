using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace First_Backend_dotNet.Models
{
    public class Brand
    {
        [Key] // para marcar la propiedad como primary key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // para generar en automatico la propiedad y autoincrementarla
        public int BrandID { get; set; }
        public string Name { get; set; }

    }
}
