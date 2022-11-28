using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserEntity.Dto
{
    public class UsersPagginationDto
    {
        public List<GetAllUsersDto> AllUsers { get; set; }
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
    }
}
