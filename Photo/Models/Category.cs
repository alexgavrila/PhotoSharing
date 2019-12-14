using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Photo.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        [StringLength(30, ErrorMessage = "Prea lung")]
        public string CategoryName { get; set; }
        public virtual ICollection<Image> Photo { get; set; }
    }
}