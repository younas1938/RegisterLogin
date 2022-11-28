using Microsoft.AspNetCore.Mvc;
using UserEntity.Models;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UserEntity.Enum;

namespace UserEntity.Controllers
{
    // we have BaseController from ControllerBase because in this API we dont need view Support, if Needed View then derieved from Controller
    //we use baseController for common features in our application, which will be use next in anywhere we want just by call it's properties and 
    public class BaseController : Controller
    {

        public override OkObjectResult Ok(object value)
        {
            
            // make a serve response object and assign values to our response to show in the JSON form
            return base.Ok(new ServerResponse()
            {
                // the RequestStatus.Status.Success is our ENUM type  so if OK override then this will work
                Status = (int)RequestStatus.Status.Success,
                Payload = value
            });
        }

        protected OkObjectResult Ok(string message)
        {
            // this is for the message we want to return in the server response
            return base.Ok(new ServerResponse()
            {
                Status = (int)RequestStatus.Status.Success,
                Message = message
            });
        }


        protected OkObjectResult Ok(object value, string message)
        {
            // this case will work if we want to show message and data 
            return base.Ok(new ServerResponse()
            {
                Status = (int)RequestStatus.Status.Success,
                Message = message,
                Payload = value
            });
        }

        public override BadRequestObjectResult BadRequest(object value)
        {
            // if we passes the non string type like system generated exception it will go through if condition
            if (value.GetType() != typeof(string))
            {
                Dictionary<string, object> errorDictionary = value.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
                    .ToDictionary(prop => prop.Name, prop => prop.GetValue(value, null));
                // assigning the exception message into string for our serverresponse.Message
                return base.BadRequest(new ServerResponse()
                {
                    Status = (int)RequestStatus.Status.Error,
                    Message = errorDictionary["Message"].ToString(),
                });
            }
            // otherwise if string this will works, like User is Added or Deleted custome generated messages
            return base.BadRequest(new ServerResponse()
            {
                Status = (int)RequestStatus.Status.Error,
                Message = value.ToString(),
            });
        }
    }
}
