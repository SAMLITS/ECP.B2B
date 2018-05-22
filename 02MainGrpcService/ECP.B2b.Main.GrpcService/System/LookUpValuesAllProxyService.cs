using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Grpc.Core;
using ECP.B2b.DbModel.Sys;
using ECP.B2b.Service.Interface;
using ECP.B2b.Service;
using ECP.B2b.ComEntity.Page;
using System.Linq;
using System.Linq.Expressions;
using ECP.Util.Common;
using ECP.B2b.Main.GrpcProxy.System;
using ECP.B2b.ModelDto.System.LookUpValuesAll;
using ECP.B2b.ComEntity.Filter.LookUpValuesAll;
using ECP.B2b.ComEntity;

namespace ECP.B2b.Main.GrpcService
{
    public class LookUpValuesAllProxyService : EntityProxyBase<PageResultReplyDto, PageQueryParams, B2B_LOOKUP_VALUES_ALL>, LookUpValuesAllProxy.ILookUpValuesAllProxyBase
    {
        public LookUpValuesAllProxyService(Func<IBaseService<B2B_LOOKUP_VALUES_ALL>> _baseService) : base(_baseService)
        {
            base._ListByPageSelector = m => new B2B_LOOKUP_VALUES_ALL
            {
                ID = m.ID,
                LOOKUP_TYPE = m.LOOKUP_TYPE,
                LOOKUP_TYPE_NAME = m.LOOKUP_TYPE_NAME,
                LOOKUP_DESCRIPTION = m.LOOKUP_DESCRIPTION,

                CREATION_DATE = m.CREATION_DATE,
                CREATOR = m.CREATOR,
                LAST_UPDATE_DATE = m.LAST_UPDATE_DATE,
                EDITOR = m.EDITOR,
            };
        }

        /// <summary>
        /// 码表新增编辑校验
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public override Task<string> ValidAdd_Modify(B2B_LOOKUP_VALUES_ALL request, ServerCallContext context)
        {
            //校验码表代码唯一性
            bool isLookUpTypeExist = base.baseService().Count(x => x.ID != request.ID && x.LOOKUP_TYPE == request.LOOKUP_TYPE).Result > 0;
            if (isLookUpTypeExist)
            {
                return Task.Run(() => "3019");
            }
            //校验码表名称唯一性
            bool isLookUpNameExist = base.baseService().Count(x => x.ID != request.ID && x.LOOKUP_TYPE_NAME == request.LOOKUP_TYPE_NAME).Result > 0;
            if (isLookUpNameExist)
            {
                return Task.Run(() => "3020");
            }
            return Task.Run(() => "1");
        }


    }
}
