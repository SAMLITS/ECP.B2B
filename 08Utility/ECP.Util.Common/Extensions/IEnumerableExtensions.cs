
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ECP.B2b.ModelDto;
using ECP.B2b.ModelDto.System.LookUpValues;

namespace ECP.Util.Common.Extensions
{
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// 获取 NameByIdDto 结果中的id集合
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static List<int> GetIDS(this List<NameByIdDto> list)  
        {
            return list.Select(s => s.ID).ToList();
        }


        public static string GetLookupText(this List<LookUpValuesByTypeDto> lookupList, string lookupType,string lookupCode)
        {
            List<LookUpValuesByTypeDto> textList= lookupList.Where(l => l.LOOKUP_TYPE == lookupType && l.LOOKUP_CODE == lookupCode).ToList();
            if (textList.Count > 0)
                return textList[0].LOOKUP_MEANING;
            else
                return lookupCode.ToDbJoinNull();
        }
    }
}
