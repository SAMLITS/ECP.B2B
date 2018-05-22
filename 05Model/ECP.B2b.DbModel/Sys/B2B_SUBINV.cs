using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace ECP.B2b.DbModel.Sys
{
    /// <summary>
    /// 子库
    /// </summary>
    [Serializable]
    public class B2B_SUBINV: BaseModel
    {
        /// <summary>
        /// 子库ID
        /// </summary>
        [Key, Required]
        public int SUBINV_ID { get; set; }

        /// <summary>
        /// 库存组织ID
        /// </summary>
        [Required]
        public int INV_ID { get; set; }

        /// <summary>
        /// 子库名称
        /// </summary>
        [Required,MaxLength(100)]
        public string SUBINV_NAME { get; set; }

        /// <summary>
        /// 子库类型
        /// </summary>
        [Required,MaxLength(50)]
        public string SUBINV_TYPE { get; set; }

        /// <summary>
        /// EBS云平台子库ID
        /// </summary>
        [Required]
        public int EBS_SUBINV_ID { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string REMARK { get; set; }
    }
}
