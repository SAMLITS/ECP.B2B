using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ECP.Util.Common.Extensions
{
    public static class ObjectExtensions
    {
        public static string ToDbJoinNull(this object obj)
        {
            return obj == null || obj.ToString().Trim() == "" ? "" : obj + "-未找到关联";
        }

        public static int ToIntByIntNull(this int? i)
        {
            return Convert.ToInt32(i);
        }

        public static object ChangeTypeExtend(this object val, PropertyInfo property)
        {
            if (val.GetType() != property.PropertyType)
            {
                if (!property.PropertyType.IsGenericType)
                {
                    //非泛型
                    return Convert.ChangeType(val, property.PropertyType);
                }
                else
                {
                    //泛型Nullable<>
                    Type genericTypeDefinition = property.PropertyType.GetGenericTypeDefinition();
                    if (genericTypeDefinition == typeof(Nullable<>))
                    {
                        return Convert.ChangeType(val, Nullable.GetUnderlyingType(property.PropertyType));
                    }
                } 
            }
            return val;
        }

        /// <summary>
        /// 四舍五入保留指定为小数 
        /// </summary>
        /// <param name="val"></param>
        /// <param name="digits"></param>
        /// <returns></returns>
        public static double? Round(this double? val, int digits = 2)
        {
            if (val != null)
            {
                return Math.Round(Convert.ToDouble(val), digits);
            }
            else
                return val;
        }
    }
}
