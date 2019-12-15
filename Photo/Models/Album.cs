using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Photo.Models
{
    public class Album
    {
        [Key]
        public int AlbumId { get; set; }

        [Required(ErrorMessage = "Campul title este obligatoriu")]
        [MaxLength(40)]
        [MinLength(5, ErrorMessage = "Titlu trebuie sa fie mai mare de 5 caractere")]
        public string Name { get; set; }

        public DateTime Date { get; set; }


        public virtual ICollection<Image> Photo { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }







    }
}