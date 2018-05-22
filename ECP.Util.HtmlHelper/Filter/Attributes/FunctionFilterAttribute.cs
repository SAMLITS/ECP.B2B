using System;
using System.Collections.Generic;
using System.Text;

namespace ECP.Util.HtmlHelper.Filter.Attributes
{
    /// <summary>
    /// 功能过滤特性
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
    public class FunctionFilterAttribute : Attribute
    {
        public FunctionFilterAttribute(string funCode)
        {
            FunCode = funCode;
        }

        /// <summary>
        /// 功能编号  
        /// 多个使用逗号隔开，一般在Controlls上才会给父类中的方法进行指定，格式为：方法名_功能编号
        /// </summary>
        public  string FunCode { get; set; }

    }
}
