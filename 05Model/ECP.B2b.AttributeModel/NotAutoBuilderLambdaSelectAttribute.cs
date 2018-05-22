using System;
using System.Collections.Generic;
using System.Text;

namespace ECP.B2b.AttributeModel
{
    /// <summary>
    /// 仅用于标识该字段是否“不”需要自动映射生成到 select 表达树 主要用于返回DTO实体
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class NotAutoBuilderLambdaSelectAttribute : Attribute
    {
    }
}
