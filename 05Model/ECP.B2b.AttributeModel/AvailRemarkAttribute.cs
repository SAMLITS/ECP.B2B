using System;
using System.Collections.Generic;
using System.Text;

namespace ECP.B2b.AttributeModel
{
    /// <summary>
    /// 指定实体的生效数据规则 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class AvailRemarkAttribute : Attribute
    {
        public AvailRemarkAttribute(AvaliType avaliType, string bindProperName, object bindProperValue=null, bool isContainsNull = false)
        {
            _avaliType = avaliType;
            _bindProperName = bindProperName;
            _bindProperValue = bindProperValue;
            _isContainsNull = isContainsNull;

            if (_avaliType == AvaliType.StartDate || _avaliType == AvaliType.EndDate)
                _bindProperValue = DateTime.Now.ToString("yyyy-MM-dd");
            if(_avaliType == AvaliType.EndDate)
            {
                //设置结束日期是包含为null的
                _isContainsNull = true;
            }
        }

        /// <summary>
        /// 设置字段与值的 有效枚举类型
        /// </summary>
        public AvaliType _avaliType { get; set; }
        /// <summary>
        /// 有效判定字段 
        /// </summary>
        public string _bindProperName { get; set; }
        /// <summary>
        /// 有效判定值 如果是ContainsList类型则需要传入new string[] 数组
        /// </summary>
        public object _bindProperValue { get; set; }

        /// <summary>
        /// 有效判定值是否可以为null  
        /// 设置为true结果为：_bindProperName=_bindProperValue OR _bindProperName is null
        /// </summary>
        public bool _isContainsNull { get; set; } = false;
    }

    public enum AvaliType
    {
        /// <summary>
        /// 等于  用于单个固定值
        /// </summary>
        Equals,
        /// <summary>
        /// 包含  用于多个固定值  应该传入new string[] 数组
        /// </summary>
        ContainsList,
        /// <summary>
        /// 生效日期
        /// </summary>
        StartDate,
        /// <summary>
        /// 失效日期
        /// </summary>
        EndDate
    }
}
