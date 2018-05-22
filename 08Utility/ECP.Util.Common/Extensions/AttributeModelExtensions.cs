using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ECP.B2b.AttributeModel;

namespace ECP.Util.Common.Extensions
{
    public static class AttributeModelExtensions
    {
        /// <summary>
        /// 将T对象中的具有 [ModelModifyFlag] 特性的字段值由 _Source 中更新到 _Dest 对象中
        /// </summary>
        /// <typeparam name="T">DB实体对象</typeparam>  
        /// <param name="_Source">传入部分值对象</param>
        /// <param name="_Dest">查询出的目标对象 被赋值更新</param>
        public static void SourceDestModifyConvert<T>(this T _Source, T _Dest)
        {
            Type type = typeof(T);
            type.GetProperties().Where(p => p.GetCustomAttributes(typeof(ModelModifyFlagAttribute), false).Length > 0).ToList().ForEach(p =>
            {
                p.SetValue(_Dest, p.GetValue(_Source));
            });
        }
    }
}
