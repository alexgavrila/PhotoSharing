using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Photo.Models
{
    public class Image
    {

        [Key]
        public int PhotoId { get; set; }
        public string Description { get; set; }

        public DateTime Date { get; set; }

        //salvare imaginii in db
        [Required]
        public string filePath { get; set; }


        [Required(ErrorMessage = "Alege o categorie")]
        public int CategoryId { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
        public virtual Category Category { get; set; }

        public int AlbumId { get; set; }
        public virtual Album Album { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Comment> Comment { get; set; }


    }
}