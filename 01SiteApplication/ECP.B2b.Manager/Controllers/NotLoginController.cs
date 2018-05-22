using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ECP.B2b.Main.GrpcClient.Interface.System;
using ECP.B2b.ModelDto.System.Messages;
using ECP.B2b.ComEntity;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ECP.B2b.Client.Controllers
{
    /// <summary>
    /// 不需要登录即可访问的放在此处 
    /// </summary>
    [Route("/common")]
    public class NotLoginController : Controller
    {
        public IMessagesClient _messagesClient; 

        public NotLoginController(IMessagesClient messagesClient )
        { 
            _messagesClient = messagesClient; 
        }


        // GET: /<controller>/
        [Route("alertmsg")]
        [HttpPost]
        public JsonResult AlertMsg(string msgNumber)
        {
            MessageAlertDto message = _messagesClient.FindMessageByAlert(msgNumber).Result;
            return Json(message);
        }
         
    }
}
