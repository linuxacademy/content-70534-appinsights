using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace la70534ai.Models
{
    public class StoreProduct
    {
        public int Id { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(255)]
        public string Name { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(3000)]
        public string Description { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        [DisplayName("Age Restricted")]
        public bool AgeRestricted { get; set; }
    }
}
