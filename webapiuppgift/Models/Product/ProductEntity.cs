using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapiuppgift.Models.Product
{
    public class ProductEntity
    {
        [Key]
        public string ArticleNr { get; set; } = null!;
        
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        [Column(TypeName = "money")]
        public decimal Price { get; set; } 
        public string Description { get; set; } = null!;
        public string ImgUrl { get; set; } = null!;

        [Required]
        public int CategoryId { get; set; }
        public virtual ProductCategoryEntity Category { get; set; } = null!;
    }
}
