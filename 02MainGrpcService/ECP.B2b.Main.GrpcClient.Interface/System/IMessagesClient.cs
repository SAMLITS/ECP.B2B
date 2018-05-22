using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions; 
using ECP.B2b.ComEntity.Page;
using ECP.B2b.ModelDto.System.Messages; 
using ECP.B2b.ComEntity;
using ECP.B2b.DbModel.Sys;
using ECP.B2b.ComEntity.Filter.Messages;

namespace ECP.B2b.Main.GrpcClient.Interface.System
{
    public interface IMessagesClient: IBaseGrpcClient<PageResultReplyDto, PageQueryParams, B2B_MESSAGES>
    {
    	/// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
	Task<string> GetMaxMessagesNumber();
	
	/// <summary>
        /// 根据消息编码返回消息信息  用于开发弹出
        /// </summary>
        /// <param name="msgNumber"></param>
        /// <returns></returns>
        Task<MessageAlertDto> FindMessageByAlert(string msgNumber);
    }
}
