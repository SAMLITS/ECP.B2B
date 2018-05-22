using System;
using System.Collections.Generic;
using System.Text;

namespace ECP.B2b.AttributeModel
{
    /// <summary>
    /// 查询条件 -》 实体    映射标识
    /// </summary>
    public class PageQueryFieldMappingAttribute: Attribute
    {
        /// <summary>
        /// 映射实体字段名称
        /// </summary>
        public string EntityFieldName { get; set; }

        public PageQueryFieldMappingAttribute(string _EntityFieldName)
        {
            this.EntityFieldName = _EntityFieldName;
        }
    }
}
