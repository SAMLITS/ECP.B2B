using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace ECP.B2b.DbModel.Sys
{
    /// <summary>
    /// 货位
    /// </summary>
    [Serializable]
    public class B2B_INV_LOCATION: BaseModel
    {
        /// <summary>
        /// 货位ID
        /// </summary>
        [Key, Required]
        public int LOCATION_ID { get; set; }

        /// <summary>
        /// 库存组织ID
        /// </summary>
        [Required]
        public int INV_ID { get; set; }

        /// <summary>
        /// 货位代码
        /// </summary>
        [Required,MaxLength(100)]
        public string LOCATION_CODE { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string REMARK { get; set; }
    }
}
