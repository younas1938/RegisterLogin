using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserEntity.Data.Services
{
    // T is is Actuall type of data, we want to send back
    public class ServerResponse
    {
        // store the data and show into the OK response
        public Object Payload { get; set; }
        // we can tell the front end if everything went right , inthe form of 0 or 1
        public int Status { get; set; } = 1;

        // the messgae property can be used to send a nice explanpotry msg i.e error
        public string Message { get; set; } = "";

    }



}
