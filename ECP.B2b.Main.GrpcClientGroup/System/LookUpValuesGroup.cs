using ECP.B2b.ComEntity.Filter.LookUpValues;
using ECP.B2b.ComEntity.Page;
using ECP.B2b.Main.GrpcClient.Interface.System;
using ECP.B2b.Main.GrpcClientGroup.Interface.System;
using ECP.B2b.ModelDto.System.LookUpValues;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace ECP.B2b.Main.GrpcClientGroup.System
{
    public class LookUpValuesGroup: ILookUpValuesGroup
    {
        public ILookUpValuesClient _lookUpValuesClient;

        public LookUpValuesGroup(ILookUpValuesClient lookUpValuesClient)
        {
            _lookUpValuesClient = lookUpValuesClient;
        }

        /// <summary>
        /// 组合数据
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        public async Task<PageResult<PageResultReplyDto>> ListByPageGroup(PageQueryParams queryParams)
        {
            var lookUpValuesList = _lookUpValuesClient.GetListByPage(queryParams);
            var lookUpList= _lookUpValuesClient.GetLookUpValuesByType(new List<LookUpValuesByTypeParams>
            {
                new LookUpValuesByTypeParams{IsBetweenOt=true, LookUpName ="YES_NO" }
            });
            await lookUpValuesList;
            await lookUpList;

            var result = from v in lookUpValuesList.Result.Data
                         join p in lookUpList.Result.Where(l => l.LOOKUP_TYPE == "YES_NO")
                         on v.ENABLED_FLAG equals p.LOOKUP_CODE into temp
                         from tt in temp.DefaultIfEmpty()
                         select new PageResultReplyDto
                         {
                             ID = v.ID,
                             LOOKUP_VALUES_ALL_ID = v.LOOKUP_VALUES_ALL_ID,
                             LOOKUP_TYPE = v.LOOKUP_TYPE,
                             LOOKUP_CODE = v.LOOKUP_CODE,
                             LOOKUP_MEANING = v.LOOKUP_MEANING,
                             LOOKUP_DESCRIPTION = v.LOOKUP_DESCRIPTION,
                             ENABLED_FLAG = v.ENABLED_FLAG,
                             ENABLED_FLAG_NAME= tt == null ? v.ENABLED_FLAG : tt.LOOKUP_MEANING,
                             TAG = v.TAG,
                             START_DATE_ACTIVE = v.START_DATE_ACTIVE,
                             END_DATE_ACTIVE = v.END_DATE_ACTIVE,
                             ATTIBUTE1 = v.ATTIBUTE1,
                             ATTIBUTE2 = v.ATTIBUTE2,
                             ATTIBUTE3 = v.ATTIBUTE3,
                             ATTIBUTE4 = v.ATTIBUTE4,
                             ATTIBUTE5 = v.ATTIBUTE5,
                             CREATION_DATE = v.CREATION_DATE,
                             CREATOR = v.CREATOR,
                             LAST_UPDATE_DATE = v.LAST_UPDATE_DATE,
                             EDITOR = v.EDITOR
                         };
            lookUpValuesList.Result.Data = result.ToList();
            return lookUpValuesList.Result;
        }
    }
}
