using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TransactionService.Models
{
    [Table("Transacciones")]
    public class Transaccion
    {
        [Key]
        public int IdTransaccion { get; set; }

        public DateTime Fecha { get; set; }

        [Required, StringLength(1)]
        public string TipoTransaccion { get; set; } 

        
        public int IdProducto { get; set; }
        [NotMapped]
        [JsonPropertyName("nombreProducto")]
        public string nombreProducto { get; set; } = "";

        public int Cantidad { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal PrecioUnitario { get; set; }

        [Column(TypeName = "decimal(12,2)")]
        public decimal PrecioTotal { get; set; }

        [StringLength(255)]
        public string Detalle { get; set; }
    }
}
