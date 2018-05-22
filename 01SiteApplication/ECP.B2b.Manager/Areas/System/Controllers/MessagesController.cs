using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ECP.B2b.ComEntity;
using ECP.B2b.ComEntity.Page; 
using ECP.B2b.Manager.Models;
using ECP.Util.Common;
using ECP.B2b.Main.GrpcClient.Interface.System; 
using Microsoft.AspNetCore.Http; 
using static ECP.B2b.ComEntity.Filter.Messages.PageQueryParams;
using ECP.B2b.DbModel.Sys;
using ECP.B2b.Main.GrpcClientGroup.Interface.System;
using ECP.B2b.ComEntity.Filter.Messages;
using ECP.B2b.ModelDto.System.Messages;

namespace ECP.B2b.Manager.Areas.System.Controllers
{
    public class MessagesController : BaseController<ModelDto.System.Messages.PageResultReplyDto, ComEntity.Filter.Messages.PageQueryParams, B2B_MESSAGES>
    {
        public IMessagesClient _messagesClient;
        public IMessagesGroup _messagesGroup;

        public MessagesController(IMessagesClient messagesClient, IMessagesGroup messagesGroup) : base(messagesClient)
        {
            _messagesClient = messagesClient;
            _messagesGroup = messagesGroup;
        }

        /// <summary>
        /// 重写列表数据加载，处理码值
        /// </summary>
        /// <param name="pageQueryParams"></param>
        /// <returns></returns>
        public override async Task<JsonResult> List(PageQueryParams pageQueryParams)
        {
            var resultReply = await _messagesGroup.ListByPageGroup(pageQueryParams);
            return Json(resultReply);
        }

	/// <summary>
        /// 重写消息新增，获取最大编号
        /// </summary>
        /// <returns></returns>
        /// </summary>
        /// <returns></returns>
        public override ActionResult Add()
        {
            int.TryParse(_messagesClient.GetMaxMessagesNumber().Result,out int maxNumber);
            ViewBag.MaxNumber= maxNumber + 1;
            return base.Add();
        }
    }
}