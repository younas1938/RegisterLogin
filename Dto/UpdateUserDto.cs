using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserEntity.Dto
{
    public class UpdateUserDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
    }
}
