using System;
using System.Collections.Generic;
using System.Text;

namespace ECP.B2b.AttributeModel
{
    /// <summary>
    /// 设置查询条件字段的查询类型属性，用于自动生成 Lambda 查询条件
    /// </summary>
    public class QueryParamsBuildOptionsAttribute : Attribute
    {
        /// <summary>
        /// 标识字段查询方式
        /// </summary>
        public ExpressionOptions _Options;

        public QueryParamsBuildOptionsAttribute(ExpressionOptions _options)
        {
            this._Options = _options;
        }
    }

    public enum ExpressionOptions
    {
        /// <summary>
        /// 用于特性标识，包含Equals与Contains   具体使用哪一个通过IS_LIKE字段来判断
        /// </summary>
        Normal,

        /// <summary>
        /// 等于
        /// </summary>
        Equals,
        /// <summary>
        /// 不等于
        /// </summary>
        NotEquals,
        /// <summary>
        /// 大于
        /// </summary>
        GreaterThan,
        /// <summary>
        /// 小于
        /// </summary>
        LessThan,
        /// <summary>
        /// 大于等于
        /// </summary>
        GreaterThanOrEqual,
        /// <summary>
        /// 小于等于
        /// </summary>
        LessThanOrEqual,
        /// <summary>
        /// 包含 针对字符串
        /// </summary>
        Contains,
        /// <summary>
        /// 开始包含
        /// </summary>
        StartsWith,
        /// <summary>
        /// 结束包含
        /// </summary>
        EndsWith,

        /// <summary>
        /// 集合对象中包含  in 
        /// </summary>
        ContainsList,

        /// <summary>
        /// 集合对象中不包含 not in
        /// </summary>
        NotContainsList
    }

    /// <summary>
    /// 在查询条件实体中指定属性只用于传输，在生成 Lambda 查询条件时会将其过滤掉，也就是该字段不会生成 Lambda 查询条件
    /// </summary>
    public class NotQueryParamsBuildOptionsAttribute : Attribute
    { }
}
