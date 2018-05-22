using System;
using System.Collections.Generic;
using System.Text;

namespace ECP.B2b.AttributeModel
{
    /// <summary>
    /// 事务处理字段关联 
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class TransFieldBindAttribute : Attribute
    {
        public TransFieldBindAttribute(Type bindType, string bindProperName)
        {
            _bindType = bindType;
            _bindProperName = bindProperName;
        }

        /// <summary>
        /// 绑定关联类型
        /// </summary>
        public Type _bindType { get; set; }
        /// <summary>
        /// 绑定关联字段
        /// </summary>
        public string _bindProperName { get; set; }
    }
}
