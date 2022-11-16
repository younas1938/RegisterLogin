using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RegisterLogin.Models
{
    // created a User Entity/Modelel as per Assigment
    public class User
    {
        // id is for the userId which will be hide during whole CRUD of the API
        [Key]
        public int Id { get; set; } 
        // display name shows the name of the field/column in our view as First Name
        [Display(Name ="First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        // required is used for that should be fill otherwise it will show an validation Error
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; } 
      

    }
}
