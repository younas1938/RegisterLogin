using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserEntity.Models
{
    public class ServerResponse
    {
   
        public Object Payload { get; set; }
        public int Status { get; set; } = 1;

        public string Message { get; set; } = "";

    }



}
