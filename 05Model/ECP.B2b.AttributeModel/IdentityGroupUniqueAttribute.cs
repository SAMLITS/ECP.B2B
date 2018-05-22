using System;
using System.Collections.Generic;
using System.Text;

namespace ECP.B2b.AttributeModel
{
    /// <summary>
    /// 多列组合唯一特性
    /// </summary>
    public class IdentityGroupUniqueAttribute : Attribute
    {
        public IdentityGroupUniqueAttribute(string _groupUniqueArray, string  _dateRangeArray = null)
        {
            if (_groupUniqueArray.Length == 0)
                throw new Exception("至少需要有一个组合！");

            if (_dateRangeArray != null && _dateRangeArray.Length != 2)
                throw new Exception("日期区间分别是开始日期与结束日期字段名，长度为2，开始日期在前，结束日期在后！");

            this.groupUniqueArray = _groupUniqueArray;
            this.dateRangeArray = _dateRangeArray;
        }

        /// <summary>
        /// 一个个的组合循环执行  每一组使用"|"进行分割，同组使用“,”进行分割   每一个组合的第一个项为返回的消息编码值    
        /// 将默认加上ID!=ID
        /// [NULL] 不等于null才校验某个字段标识 
        /// 
        /// 参考 菜单DbModel
        /// </summary>
        public string  groupUniqueArray { get; set; }
        /// <summary>
        /// Exists专用唯一校验时间区间  开始日期0   结束日期1
        /// </summary>
        public string  dateRangeArray { get; set; }
    }
}