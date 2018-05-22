using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ECP_B2B_API_SDK.Entity.Dto
{
    public class PageResult
    {
        /// <summary>
        /// 页码
        /// </summary> 
        public int PageIndex { get; set; }

        /// <summary>
        /// 每页条数
        /// </summary> 
        public int PageSize { get; set; }

        /// <summary>
        /// 总条数
        /// </summary> 
        public int Total { get; set; }

        /// <summary>
        /// 分页后得到的数据
        /// </summary> 
        public DataTable Data { get; set; } = new DataTable();
    }
}
