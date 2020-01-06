using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace Photo.Models
{
    public class UserProfile
    {

        [Key]
        public int ProfilId { get; set; }

        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime BirthDate { get; set; }
    
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        
        public string TextProfil { get; set; }





    }
}