using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BACKEND.Models
{
    public class Beer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BeerId { get; set; }

        public string Name { get; set; }

        public int BrandId { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Al { get; set; }

        [ForeignKey("BrandId")]
        public Brand Brand { get; set; }
    }
}
