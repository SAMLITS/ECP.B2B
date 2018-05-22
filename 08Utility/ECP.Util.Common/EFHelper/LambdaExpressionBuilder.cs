using ECP.B2b.AttributeModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;


namespace ECP.Util.Common.EFHelper
{
    public class FilterCollection : Collection<IList<Filter>>
    {
        public FilterCollection()
            : base()
        { }
    }

    public class Filter
    {
        public string PropertyName { get; set; }
        public ExpressionOptions Operation { get; set; }
        public object Value { get; set; }
    }

    

    public static class LambdaExpressionBuilder
    {  

        private static MethodInfo containsMethod = typeof(string).GetMethod("Contains");
        private static MethodInfo startsWithMethod =
                                typeof(string).GetMethod("StartsWith", new Type[] { typeof(string) });
        private static MethodInfo endsWithMethod =
                                typeof(string).GetMethod("EndsWith", new Type[] { typeof(string) });
        private static Expression GetExpression(ParameterExpression param, Filter filter)
        {
            MemberExpression member = Expression.Property(param, filter.PropertyName);
            Expression handledMember = member;
            ConstantExpression constant = Expression.Constant(filter.Value);

            if (member.Member.MemberType == MemberTypes.Property)
            {
                Type propertyType = ((PropertyInfo)member.Member).PropertyType;
                if (propertyType == typeof(string))
                {
                    //handledMember = Expression.Call(member, typeof(string).GetMethod("ToLower", System.Type.EmptyTypes));
                }
                if (propertyType == typeof(DateTime?) && filter.Value != null)
                {
                    handledMember = Expression.Property(member, typeof(DateTime?).GetProperty("Value"));
                    //是要是查询条件的截止日期需要包含截止当天数据
                    if(filter.Operation== ExpressionOptions.LessThanOrEqual)
                    {
                        DateTime dt =  Convert.ToDateTime(filter.Value);
                        if (dt.Hour+dt.Minute+dt.Millisecond==0)
                        { 
                            filter.Value = dt.Add(new TimeSpan(23, 59, 59));
                            constant = Expression.Constant(filter.Value);
                        }
                    }
                }
                if (propertyType == typeof(int?)&& filter.Value!=null)
                {
                    handledMember = Expression.Property(member, typeof(int?).GetProperty("Value"));
                }
                if (propertyType == typeof(double?) && filter.Value != null)
                {
                    handledMember = Expression.Property(member, typeof(double?).GetProperty("Value"));
                }
                if (propertyType == typeof(decimal?) && filter.Value != null)
                {
                    handledMember = Expression.Property(member, typeof(decimal?).GetProperty("Value"));
                }
            }

            switch (filter.Operation)
            {
                case ExpressionOptions.Equals:
                    return Expression.Equal(handledMember, constant);
                case ExpressionOptions.NotEquals:
                    return Expression.NotEqual(handledMember, constant);
                case ExpressionOptions.GreaterThan:
                    return Expression.GreaterThan(handledMember, constant);
                case ExpressionOptions.GreaterThanOrEqual:
                    return Expression.GreaterThanOrEqual(handledMember, constant);
                case ExpressionOptions.LessThan:
                    return Expression.LessThan(handledMember, constant);
                case ExpressionOptions.LessThanOrEqual:
                    return Expression.LessThanOrEqual(handledMember, constant);
                case ExpressionOptions.Contains:
                    return Expression.Call(handledMember, containsMethod, constant);
                case ExpressionOptions.StartsWith:
                    return Expression.Call(handledMember, startsWithMethod, constant);
                case ExpressionOptions.EndsWith:
                    return Expression.Call(handledMember, endsWithMethod, constant);
                //如不是字符型，那么不允许为空，比如：List<int>   不可为：List<int?>
                case ExpressionOptions.ContainsList:
                    return Expression.Call(constant, constant.Value.GetType().GetMethod("Contains"), handledMember);
                //如不是字符型，那么不允许为空，比如：List<int>   不可为：List<int?>
                case ExpressionOptions.NotContainsList:
                    return Expression.Not(Expression.Call(constant, constant.Value.GetType().GetMethod("Contains"), handledMember));
            }
            
            return null;
        }
        private static BinaryExpression GetORExpression(ParameterExpression param, Filter filter1, Filter filter2)
        {
            Expression bin1 = GetExpression(param, filter1);
            Expression bin2 = GetExpression(param, filter2);

            return Expression.Or(bin1, bin2);
        }

        private static Expression GetExpression(ParameterExpression param, IList<Filter> orFilters)
        {
            if (orFilters.Count == 0)
                return null;

            Expression exp = null;

            if (orFilters.Count == 1)
            {
                exp = GetExpression(param, orFilters[0]);
            }
            else if (orFilters.Count == 2)
            {
                exp = GetORExpression(param, orFilters[0], orFilters[1]);
            }
            else
            {
                while (orFilters.Count > 0)
                {
                    var f1 = orFilters[0];
                    var f2 = orFilters[1];

                    if (exp == null)
                    {
                        exp = GetORExpression(param, orFilters[0], orFilters[1]);
                    }
                    else
                    {
                        exp = Expression.Or(exp, GetORExpression(param, orFilters[0], orFilters[1]));
                    }
                    orFilters.Remove(f1);
                    orFilters.Remove(f2);

                    if (orFilters.Count == 1)
                    {
                        exp = Expression.Or(exp, GetExpression(param, orFilters[0]));
                        orFilters.RemoveAt(0);
                    }
                }
            }

            return exp;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filters">一个IList<Filter>整体就是一个And查询语句，List其中的Filter选项将采用Or进行拼接查询</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> GetExpression<T>(FilterCollection filters)
        {
            if (filters == null || filters.Count == 0)
                return null;

            ParameterExpression param = Expression.Parameter(typeof(T), "t");
            Expression exp = null;

            if (filters.Count == 1)
            {
                exp = GetExpression(param, filters[0]);
            }
            else if (filters.Count == 2)
            {
                exp = Expression.AndAlso(GetExpression(param, filters[0]), GetExpression(param, filters[1]));
            }

            else
            {
                while (filters.Count > 0)
                {
                    var f1 = filters[0];
                    var f2 = filters[1];
                    var f1Andf2 = Expression.AndAlso(GetExpression(param, filters[0]), GetExpression(param, filters[1]));
                    if (exp == null)
                    {
                        exp = f1Andf2;
                    }
                    else
                    {
                        exp = Expression.AndAlso(exp, f1Andf2);
                    }

                    filters.Remove(f1);
                    filters.Remove(f2);

                    if (filters.Count == 1)
                    {
                        exp = Expression.AndAlso(exp, GetExpression(param, filters[0]));
                        filters.RemoveAt(0);
                    }
                }
            }

            return Expression.Lambda<Func<T, bool>>(exp, param);
        }
    }
}
