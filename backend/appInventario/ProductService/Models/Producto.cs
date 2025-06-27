using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductService.Models
{
    [Table("Productos")]             
    public class Producto
    {
        [Key]
        public int IdProducto { get; set; }

        [Required, StringLength(100)]
        public string Nombre { get; set; }

        [StringLength(255)]
        public string Descripcion { get; set; }

        [StringLength(50)]
        public string Categoria { get; set; }

        [StringLength(255)]
        public string ImagenUrl { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Precio { get; set; }

        public int Stock { get; set; }
    }
}
