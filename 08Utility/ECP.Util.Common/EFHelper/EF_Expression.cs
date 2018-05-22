using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ECP.Util.Common
{
    /// <summary>
    /// 动态生成 条件表达式树
    /// </summary>
    public static class EF_Where_Expression
    {

        /// <summary>
        /// 自定义Contains方法(允许value为null)，value为null 的时候，该查询条件 不会生成
        /// </summary>
        /// <typeparam name="T">实体数据类型</typeparam>
        /// <param name="columnNames">以逗号分割的列名称</param>
        /// <param name="values">这些列对应的值</param>
        /// <returns>返回Lambda表达式，eg:where(Lambda)</returns>
        public static IQueryable<T> WhereContains<T>(this IQueryable<T> source, string columnNames, params object[] values)
        {
            return source.Where(Custom_Expression_Common<T>((Expression left, Expression right) =>
            {
                return Expression.Call(left, typeof(string).GetMethod("Contains"), right);
            }, columnNames, values));
        }

        /// <summary>
        /// 自定义Equal方法(允许value为null)，value为null 的时候，该查询条件 不会生成
        /// </summary>
        /// <typeparam name="T">实体数据类型</typeparam>
        /// <param name="columnNames">以逗号分割的列名称</param>
        /// <param name="values">这些列对应的值</param>
        /// <returns>返回Lambda表达式，eg:where(Lambda)</returns>
        public static IQueryable<T> WhereEqual<T>(this IQueryable<T> source, string columnNames, params object[] values)
        {
            return source.Where(Custom_Expression_Common<T>((Expression left, Expression right) =>
            {
                return Expression.Equal(left, right);
            }, columnNames, values));
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns>Lambda表达式树</returns>
        public delegate Expression ExpressionEventHandler(Expression left, Expression right);


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Handler">可以是  Equal、Contains</param>
        /// <param name="columnNames"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        static Expression<Func<T, bool>> Custom_Expression_Common<T>(ExpressionEventHandler handler, string columnNames, params object[] values)
        {
            BinaryExpression filter = Expression.Equal(Expression.Constant(1), Expression.Constant(1));

            var columns = columnNames.Split(',');
            var param = Expression.Parameter(typeof(T));
            for (int i = 0; i < columns.Length; i++)
            {
                if (values[i] == null) continue;
                string columnName = columns[i].ToString();
                var value = values[i];
                Expression left = Expression.Property(param, typeof(T).GetProperty(columnName));
                Expression right = Expression.Constant(value, value.GetType());
                Expression result = handler(left, right);
                filter = Expression.And(filter, result);// where 条件 拼接
            }
            return Expression.Lambda<Func<T, bool>>(filter, param);
        }
    }
     

    /// <summary>
    /// 动态生成 排序表达式树
    /// </summary>
    public static class OrderExpression
    {
        public static IOrderedQueryable<T> OrderByProp<T>(this IQueryable<T> source, string property)
        {
            return ApplyOrder<T>(source, property, "OrderBy");
        }
        public static IOrderedQueryable<T> OrderByDescendingProp<T>(this IQueryable<T> source, string property)
        {
            return ApplyOrder<T>(source, property, "OrderByDescending");
        }
        public static IOrderedQueryable<T> ThenByProp<T>(this IOrderedQueryable<T> source, string property)
        {
            return ApplyOrder<T>(source, property, "ThenBy");
        }
        public static IOrderedQueryable<T> ThenByDescendingProp<T>(this IOrderedQueryable<T> source, string property)
        {
            return ApplyOrder<T>(source, property, "ThenByDescending");
        }
        static IOrderedQueryable<T> ApplyOrder<T>(IQueryable<T> source, string property, string methodName)
        {
            string[] props = property.Split('.');
            Type type = typeof(T);
            ParameterExpression arg = Expression.Parameter(type, "x");
            Expression expr = arg;
            foreach (string prop in props)
            {
                // use reflection (not ComponentModel) to mirror LINQ  
                PropertyInfo pi = type.GetProperty(prop);
                expr = Expression.Property(expr, pi);
                type = pi.PropertyType;
            }
            Type delegateType = typeof(Func<,>).MakeGenericType(typeof(T), type);
            LambdaExpression lambda = Expression.Lambda(delegateType, expr, arg);

            object result = typeof(Queryable).GetMethods().Single(
                    method => method.Name == methodName
                            && method.IsGenericMethodDefinition
                            && method.GetGenericArguments().Length == 2
                            && method.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(T), type)
                    .Invoke(null, new object[] { source, lambda });
            return (IOrderedQueryable<T>)result;
        }
    }

}
