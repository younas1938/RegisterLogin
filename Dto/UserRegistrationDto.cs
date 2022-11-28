using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserEntity.Dto
{
    public class UserRegistrationDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        [Required]
        public string UserName { get; set; }
       
        public string Password { get; set; }
        //public List<UsersPagginationDto> Pages { get; set; }


    }
}
