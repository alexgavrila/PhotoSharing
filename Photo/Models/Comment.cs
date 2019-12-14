using System;
using System.ComponentModel.DataAnnotations;

namespace Photo.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }

        [Required(ErrorMessage = "Camp obligaoriu")]
        public string Description { get; set; }

        public DateTime Date { get; set; }

        [Required]
        public int PhotoId { get; set; }
        public virtual Image Photo { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}