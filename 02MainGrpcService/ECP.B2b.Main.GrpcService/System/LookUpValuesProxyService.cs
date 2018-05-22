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
using ECP.B2b.ModelDto.System.LookUpValues;
using ECP.B2b.ComEntity.Filter.LookUpValues;
using ECP.B2b.ComEntity;
using ECP.B2b.Service.Interface.System;

namespace ECP.B2b.Main.GrpcService
{
    public class LookUpValuesProxyService : EntityProxyBase<PageResultReplyDto, PageQueryParams, B2B_LOOKUP_VALUES>, LookUpValuesProxy.ILookUpValuesProxyBase
    {
        private Func<IB2B_LOOKUP_VALUES_Service> _lookupValuesService;
        public LookUpValuesProxyService(Func<IB2B_LOOKUP_VALUES_Service> lookupValuesService) : base(lookupValuesService)
        {
            this._lookupValuesService = lookupValuesService;
            base._ListByPageSelector = m => new B2B_LOOKUP_VALUES
            {
                ID = m.ID,
                LOOKUP_VALUES_ALL_ID = m.LOOKUP_VALUES_ALL_ID,
                LOOKUP_TYPE = m.LOOKUP_TYPE,
                LOOKUP_CODE = m.LOOKUP_CODE,
                LOOKUP_MEANING = m.LOOKUP_MEANING,
                LOOKUP_DESCRIPTION = m.LOOKUP_DESCRIPTION,
                ENABLED_FLAG = m.ENABLED_FLAG,
                TAG = m.TAG,
                START_DATE_ACTIVE = m.START_DATE_ACTIVE,
                END_DATE_ACTIVE = m.END_DATE_ACTIVE,
                ATTIBUTE1 = m.ATTIBUTE1,
                ATTIBUTE2 = m.ATTIBUTE2,
                ATTIBUTE3 = m.ATTIBUTE3,
                ATTIBUTE4 = m.ATTIBUTE4,
                ATTIBUTE5 = m.ATTIBUTE5,

                CREATION_DATE = m.CREATION_DATE,
                CREATOR = m.CREATOR,
                LAST_UPDATE_DATE = m.LAST_UPDATE_DATE,
                EDITOR = m.EDITOR,
            };
        }

        /// <summary>
        /// 码表明细新增编辑校验
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public override Task<string> ValidAdd_Modify(B2B_LOOKUP_VALUES request, ServerCallContext context)
        {
            //同一“码表代码”之下，“值代码"必需唯一
            bool isLookUpCodeExist = base.baseService().Count(x => x.ID != request.ID && x.LOOKUP_VALUES_ALL_ID == request.LOOKUP_VALUES_ALL_ID && x.LOOKUP_CODE == request.LOOKUP_CODE).Result > 0;
            if (isLookUpCodeExist)
            {
                return Task.Run(() => "3021");
            }
            ///同一“码表代码”之下，"值名称”必需唯一
            bool isLookUpMeaningExist = base.baseService().Count(x => x.ID != request.ID && x.LOOKUP_VALUES_ALL_ID == request.LOOKUP_VALUES_ALL_ID && x.LOOKUP_MEANING == request.LOOKUP_MEANING).Result > 0;
            if (isLookUpMeaningExist)
            {
                return Task.Run(() => "3022");
            }
            return base.ValidAdd_Modify(request, context);
        }

        /// <summary>
        /// 获取码表值
        /// </summary>
        /// <param name="lookupTypeName"></param>
        /// <returns></returns>
        public Task<List<LookUpValuesByTypeDto>> GetLookUpValuesByType(List<LookUpValuesByTypeParams> requests, ServerCallContext context)
        {
            return Task.Run(() =>
            { 
                var lookupTypes = requests.Select(r => r.LookUpName).ToList();
                List<B2B_LOOKUP_VALUES> values = base.baseService().FindAll(q => lookupTypes.Contains(q.LOOKUP_TYPE)).Result;

                List<B2B_LOOKUP_VALUES> lookupValues;
                List<B2B_LOOKUP_VALUES> removeList;

                void RemoveLookup(Func<B2B_LOOKUP_VALUES, bool> predicate)
                {
                    removeList = lookupValues.Where(predicate).ToList();
                    removeList.ForEach(d =>
                    {
                        lookupValues.Remove(d);
                        values.Remove(d);
                    });
                }

                foreach (var item in requests)
                {
                    lookupValues = values.Where(v =>
                           v.LOOKUP_TYPE == item.LookUpName).ToList();

                   
                    //不包含失效数据   进行排除
                    if (!item.IsBetweenOt)
                    { 
                        RemoveLookup(v => (v.ENABLED_FLAG != "Y"
                           || (v.END_DATE_ACTIVE != null && DateTime.Now > Convert.ToDateTime(v.END_DATE_ACTIVE))
                           || v.START_DATE_ACTIVE > DateTime.Now));
                    }

                    //只保留指定Code
                    if (item.LOOKUP_CODE_List != null && item.LOOKUP_CODE_List.Count > 0)
                    { 
                        RemoveLookup(v => !item.LOOKUP_CODE_List.Contains(v.LOOKUP_CODE));
                    }

                    //只保留满足条件的
                    if (!string.IsNullOrEmpty(item.ATTIBUTE1))
                    { 
                        RemoveLookup(v => v.ATTIBUTE1 != item.ATTIBUTE1);
                    }
                    if (!string.IsNullOrEmpty(item.ATTIBUTE2))
                    {
                        RemoveLookup(v => v.ATTIBUTE2 != item.ATTIBUTE2);
                    }
                    if (!string.IsNullOrEmpty(item.ATTIBUTE3))
                    {
                        RemoveLookup(v => v.ATTIBUTE3 != item.ATTIBUTE3);
                    }
                    if (!string.IsNullOrEmpty(item.ATTIBUTE4))
                    {
                        RemoveLookup(v => v.ATTIBUTE4 != item.ATTIBUTE4);
                    }
                    if (!string.IsNullOrEmpty(item.ATTIBUTE5))
                    {
                        RemoveLookup(v => v.ATTIBUTE5 != item.ATTIBUTE5);
                    }
                    if (!string.IsNullOrEmpty(item.TAG))
                    {
                        RemoveLookup(v => v.TAG != item.TAG);
                    }
                    
                }

                return EntityAutoMapper.ConvertMappingList<LookUpValuesByTypeDto, B2B_LOOKUP_VALUES>(values);
            });
        }
    }
}
