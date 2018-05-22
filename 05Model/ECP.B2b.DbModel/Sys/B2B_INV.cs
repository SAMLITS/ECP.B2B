using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace ECP.B2b.DbModel.Sys
{
    /// <summary>
    /// 库存组织
    /// </summary>
    [Serializable]
    public class B2B_INV : BaseModel
    {
        /// <summary>
        /// 库存组织ID
        /// </summary>
        [Key, Required]
        public int INV_ID { get; set; }

        /// <summary>
        /// 库存组织代码
        /// </summary>
        [Required,MaxLength(100)]
        public string INV_CODE { get; set; }

        /// <summary>
        /// 库存组织名称
        /// </summary>
        [Required,MaxLength(100)]
        public string INV_NAME { get; set; }

        /// <summary>
        /// 交易方ID
        /// </summary>
        [Required]
        public int PARTY_ID { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string REMARK { get; set; }

        /// <summary>
        /// 生效日期
        /// </summary>
        [Required]
        public DateTime START_DATE { get; set; }

        /// <summary>
        /// 失效日期
        /// </summary>
        public DateTime? END_DATE { get; set; }

        /// <summary>
        /// 是否外包
        /// </summary>
        public string IS_OUTER { get; set; }

        /// <summary>
        /// EBS平台库存ID
        /// </summary>
        [Required]
        public int EBS_INV_ID { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        [MaxLength(100)]
        public string CMAN { get; set; }

        /// <summary>
        /// 联系手机
        /// </summary>
        [MaxLength(20)]
        public string CMAN_MOBILE { get; set; }

        /// <summary>
        /// 联系邮箱
        /// </summary>
        [MaxLength(100)]
        public string CMAN_MAIL { get; set; }

        /// <summary>
        /// 国家Id
        /// </summary>
        public int? COUNTRY_ID { get; set; }

        /// <summary>
        /// 国家
        /// </summary>
        [MaxLength(50)]
        public string COUNTRY { get; set; }

        /// <summary>
        /// 省Id
        /// </summary>
        public int? PROVINCE_ID { get; set; }

        /// <summary>
        /// 省
        /// </summary>
        [MaxLength(50)]
        public string PROVINCE { get; set; }

        /// <summary>
        /// 市Id
        /// </summary>
        public int? CITY_ID { get; set; }

        /// <summary>
        /// 市
        /// </summary>
        [MaxLength(50)]
        public string CITY { get; set; }

        /// <summary>
        /// 区Id
        /// </summary>
        public int? DISTRICT_ID { get; set; }

        /// <summary>
        /// 区
        /// </summary>
        [MaxLength(50)]
        public string DISTRICT { get; set; }

        /// <summary>
        /// 街道
        /// </summary>
        [MaxLength(50)]
        public string STREET { get; set; }

        /// <summary>
        /// 详细地址
        /// </summary>
        [MaxLength(500)]
        public string ADDRESS { get; set; }
    }
}
