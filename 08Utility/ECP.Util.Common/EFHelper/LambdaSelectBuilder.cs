using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Linq;
using ECP.B2b.AttributeModel;
using System.Reflection;

namespace ECP.Util.Common.EFHelper
{
    /// <summary>
    /// Select 表达式树动态构建
    /// </summary>
    public class LambdaSelectBuilder
    {
        public static Expression<Func<M,D>> BuildSelect<M,D>(Dictionary<string, string> proMapping = null)
        {
            ParameterExpression left = Expression.Parameter(typeof(M), "c");
            return Expression.Lambda<Func<M, D>>(CreateMemberInitExpression<M, D>(left, proMapping),new ParameterExpression[] { left });
        }

        private static MemberInitExpression CreateMemberInitExpression<M, D>(ParameterExpression left, Dictionary<string, string> proMapping = null)
        {
            Type dType = typeof(D);
            NewExpression newAnimal = Expression.New(dType);
            List<MemberBinding> memberBindingList = new List<MemberBinding>();

            List<PropertyInfo> properList = dType.GetProperties().Where(p => p.GetCustomAttribute(typeof(NotAutoBuilderLambdaSelectAttribute)) == null).ToList();

            MemberInfo memberInfo;
            MemberExpression memberExpression;

            foreach (var p in properList)
            {
                string pName = p.Name;
                memberInfo = dType.GetMember(pName)[0];
                if (proMapping != null && proMapping.ContainsKey(pName))
                {
                    if (string.IsNullOrEmpty(proMapping[pName]))
                        continue;
                    pName = proMapping[pName];
                }

                memberExpression = Expression.Property(left, pName);
                Expression handledMember = memberExpression;
                Type propertyType = ((PropertyInfo)memberExpression.Member).PropertyType;
                //类型不一致的话统一转化为字符串
                if (p.PropertyType == typeof(string) && propertyType != typeof(string))
                {
                    handledMember = Expression.Call(memberExpression, propertyType.GetMethod("ToString", System.Type.EmptyTypes));
                }


                memberBindingList.Add(Expression.Bind(memberInfo, handledMember));
            }

            MemberInitExpression memberInitExpression = Expression.MemberInit(newAnimal, memberBindingList);

            return memberInitExpression;
        }
    }
}
