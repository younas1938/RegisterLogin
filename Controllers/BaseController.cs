using Microsoft.AspNetCore.Mvc;
using RegisterLogin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegisterLogin.Controllers
{
    // we have BaseController from ControllerBase because in this API we dont need view Support, if Needed View then derieved from Controller

    //we use baseController for common features in our application, which will be use next in anywhere we want just by call it's properties and should be inhertied by child property
    public class BaseController : ControllerBase
    {

        // we use static Users list to return the list to the user
        public static List<User> user = new List<User>()
        {  
            new User {
                Id=1,
                FirstName="qwer",
                LastName="eq",
                UserName="qwer12",
                Email="qwer12@gmail.com",
                Password="qwer1212"
            },
             new User {
                Id=2,
                FirstName="zxc",
                LastName="xz",
                UserName="zxc12",
                Email="zxc12@gmail.com",
                Password="zxc1212"
            },
               new User {
                Id=3,
                FirstName="dfgd",
                LastName="dfg",
                UserName="fgg",
                Email="fgg12@gmail.com",
                Password="fgg1212"
            }
        };
    }
}
